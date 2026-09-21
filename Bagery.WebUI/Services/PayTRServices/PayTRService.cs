using Bagery.WebUI.Exceptions;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Bagery.WebUI.Services.PayTRServices
{
    public class PayTRService(HttpClient _httpClient,
                              IConfiguration _configuration, // DEĞİŞTİ: PAY_TR'deki gibi ayarlar doğrudan buradan
                              ILogger<PayTRService> _logger) : IPayTRService
    {
        private const string PaymentUrl = "https://www.paytr.com/odeme";
        private const string BinDetailUrl = "https://www.paytr.com/odeme/api/bin-detail";
        private const string InstallmentRatesUrl = "https://www.paytr.com/odeme/taksit-oranlari";


        // ---------------- BIN SORGUSU (PAY_TR: PaymentService.GetCardBrand) ----------------
        public async Task<PayTRCardInfo> GetCardInfoAsync(string binNumber, CancellationToken cancellationToken = default)
        {
            var merchantId = _configuration["PayTR:merchantId"];
            var merchantKey = _configuration["PayTR:merchantKey"];
            var merchantSalt = _configuration["PayTR:merchantSalt"];

            // Hash sırası PAY_TR ile aynı: bin + merchantId + salt
            var token = CalculateToken(string.Concat(binNumber, merchantId, merchantSalt), merchantKey);

            var body = await PostFormAsync(BinDetailUrl, new Dictionary<string, string>
            {
                ["merchant_id"] = merchantId,
                ["bin_number"] = binNumber,
                ["paytr_token"] = token
            }, cancellationToken);

            BinDetailResponse? response;
            try
            {
                response = JsonSerializer.Deserialize<BinDetailResponse>(body);
            }
            catch (JsonException)
            {
                _logger.LogWarning("PayTR BIN yanıtı okunamadı: {Body}", body);
                throw new BusinessException("Kart bilgisi okunamadı. Lütfen tekrar deneyin.");
            }

            if (response?.Status == "success")
            {
                // PAY_TR'deki gibi: cardType "credit" ise taksit yapılabilir
                return new PayTRCardInfo(response.Brand ?? string.Empty, response.CardType == "credit");
            }

            throw new BusinessException(response?.ErrMsg ?? "Kart bilgisi alınamadı.");
        }


        // ---------------- TAKSİT ORANLARI (PAY_TR: InstallmentController.InstallmentUpdate'in PayTR kısmı) ----------------
        public async Task<List<PayTRInstallmentRate>> GetInstallmentRatesAsync(CancellationToken cancellationToken = default)
        {
            var merchantId = _configuration["PayTR:merchantId"];
            var merchantKey = _configuration["PayTR:merchantKey"];
            var merchantSalt = _configuration["PayTR:merchantSalt"];
            var requestId = Guid.NewGuid().ToString("N");

            // Hash sırası PAY_TR ile aynı: merchantId + request_id + salt
            var token = CalculateToken(string.Concat(merchantId, requestId, merchantSalt), merchantKey);

            var body = await PostFormAsync(InstallmentRatesUrl, new Dictionary<string, string>
            {
                ["merchant_id"] = merchantId,
                ["request_id"] = requestId,
                ["paytr_token"] = token
            }, cancellationToken);

            JsonNode? root;
            try
            {
                root = JsonNode.Parse(body);
            }
            catch (JsonException)
            {
                _logger.LogWarning("PayTR taksit yanıtı okunamadı: {Body}", body);
                throw new BusinessException("PayTR taksit yanıtı okunamadı.");
            }

            // "oranlar" yoksa PayTR hata dönmüştür
            if (root?["oranlar"] is not JsonObject brands)
            {
                var error = root?["err_msg"]?.ToString() ?? "PayTR taksit oranlarını döndürmedi.";
                throw new BusinessException(error);
            }

            var rates = new List<PayTRInstallmentRate>();

            // { "oranlar": { "world": { "taksit_2": 2.45, ... }, "bonus": {...} } }
            foreach (var brand in brands)
            {
                if (brand.Value is not JsonObject installments) continue;

                foreach (var installment in installments)
                {
                    var countText = installment.Key.Replace("taksit_", string.Empty); // "taksit_3" -> "3"
                    if (!int.TryParse(countText, out var count)) continue;

                    // Oran sayı da metin de gelse noktalı ondalığı doğru okur
                    if (!decimal.TryParse(installment.Value?.ToString(), NumberStyles.Number, CultureInfo.InvariantCulture, out var rate)) continue;

                    rates.Add(new PayTRInstallmentRate(brand.Key, count, rate));
                }
            }

            return rates;
        }


        // ---------------- ÖDEME (PAY_TR: PaymentController.PaymentCreate'in PayTR kısmı) ----------------
        public async Task<string> CreatePaymentAsync(PayTRPaymentRequest request, CancellationToken cancellationToken = default)
        {
            var merchantId = _configuration["PayTR:merchantId"];
            var merchantKey = _configuration["PayTR:merchantKey"];
            var merchantSalt = _configuration["PayTR:merchantSalt"];

            // Hash'e giren ve forma giden değer AYNI değişken (PAY_TR'de forma "0" sabiti gidiyordu)
            string testMode = _configuration["PayTR:testMode"] ?? "0"; // secrets'ta yoksa "0"
            string debugOn = _configuration["PayTR:debugOn"] ?? "1";

            string paymentAmount = request.PaymentAmount.ToString("0.00", CultureInfo.InvariantCulture); // 150.50 biçimi
            const string paymentType = "card";
            const string currency = "TL";
            const string non3d = "0";
            string installmentCount = request.InstallmentCount.ToString(CultureInfo.InvariantCulture);

            // Hash sırası PAY_TR ile birebir aynı
            string hashStr = string.Concat(
                merchantId,
                request.UserIp,
                request.OrderNo,
                request.Email,
                paymentAmount,
                paymentType,
                installmentCount,
                currency,
                testMode,
                non3d,
                merchantSalt);

            string paytrToken = CalculateToken(hashStr, merchantKey);

            // Sepet: [["Ürün adı","150.00","2"], ...] -> JSON -> Base64 (PAY_TR ile aynı)
            object[][] basket = request.Basket
                .Select(x => new object[]
                {
                    x.Name,
                    x.UnitPrice.ToString("0.00", CultureInfo.InvariantCulture),
                    x.Quantity.ToString(CultureInfo.InvariantCulture)
                })
                .ToArray();

            string userBasket = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(basket)));

            // Alan isimleri PAY_TR'deki postData ile aynı
            var form = new Dictionary<string, string>
            {
                ["merchant_id"] = merchantId,
                ["paytr_token"] = paytrToken,
                ["user_ip"] = request.UserIp,
                ["merchant_oid"] = request.OrderNo,
                ["email"] = request.Email,
                ["payment_type"] = paymentType,
                ["payment_amount"] = paymentAmount,
                ["installment_count"] = installmentCount,
                ["card_type"] = request.CardType,
                ["currency"] = currency,
                ["client_lang"] = "tr",
                ["test_mode"] = testMode,
                ["non_3d"] = non3d,
                ["non3d_test_failed"] = "0",
                ["cc_owner"] = request.CardOwner,
                ["card_number"] = request.CardNumber,
                ["expiry_month"] = request.ExpiryMonth,
                ["expiry_year"] = request.ExpiryYear,
                ["cvv"] = request.Cvv,
                ["merchant_ok_url"] = request.OkUrl,
                ["merchant_fail_url"] = request.FailUrl,
                ["user_name"] = request.UserName,
                ["user_address"] = request.UserAddress,
                ["user_phone"] = request.UserPhone,
                ["user_basket"] = userBasket,
                ["debug_on"] = debugOn,
                ["sync_mode"] = "0",
                ["no_installment"] = "0",
                ["max_installment"] = "0",
                ["lang"] = "tr"
            };

            var response = await PostFormAsync(PaymentUrl, form, cancellationToken);

            // 1) HTML içine gömülü hata: ..."status":"failed","reason":"..."
            if (response.Contains("\"status\":\"failed\"", StringComparison.OrdinalIgnoreCase))
            {
                var match = Regex.Match(response, @"""reason""\s*:\s*""([^""]*)""", RegexOptions.IgnoreCase);
                var reason = match.Success
                    ? JsonSerializer.Deserialize<string>($"\"{match.Groups[1].Value}\"") // \u00fc gibi kaçışları çözer
                    : null;

                throw new BusinessException(reason ?? "Ödeme başlatılamadı.");
            }

            // 2) HTML değil düz metin döndüyse o metin hata mesajıdır
            if (!response.TrimStart().StartsWith('<'))
            {
                throw new BusinessException(response.Trim());
            }

            // 3) Başarılı: 3D Secure sayfasına yönlendiren HTML
            return response;
        }


        // ---------------- CALLBACK HASH (PAY_TR: CallBackController içindeki kontrol) ----------------
        public bool IsCallbackHashValid(string merchantOid, string status, string totalAmount, string hash)
        {
            var merchantKey = _configuration["PayTR:merchantKey"];
            var merchantSalt = _configuration["PayTR:merchantSalt"];

            // Hash sırası PAY_TR ile aynı: merchant_oid + salt + status + total_amount
            var expected = CalculateToken(string.Concat(merchantOid, merchantSalt, status, totalAmount), merchantKey);

            // == yerine sabit süreli karşılaştırma (Adım 6'da açıklamıştık)
            return CryptographicOperations.FixedTimeEquals(
                Encoding.UTF8.GetBytes(expected),
                Encoding.UTF8.GetBytes(hash ?? string.Empty));
        }


        // ---------------- yardımcılar ----------------

        // PAY_TR'deki CryptoHelper.CalculateToken'ın aynısı
        private static string CalculateToken(string data, string merchantKey)
        {
            var hash = HMACSHA256.HashData(Encoding.UTF8.GetBytes(merchantKey), Encoding.UTF8.GetBytes(data));
            return Convert.ToBase64String(hash);
        }

        private async Task<string> PostFormAsync(string url, Dictionary<string, string> data, CancellationToken cancellationToken)
        {
            using var content = new FormUrlEncodedContent(data); // Content-Type'ı kendisi ekler

            HttpResponseMessage response;
            try
            {
                response = await _httpClient.PostAsync(url, content, cancellationToken);
            }
            catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException) // internet yok / zaman aşımı
            {
                _logger.LogError(ex, "PayTR'ye ulaşılamadı: {Url}", url);
                throw new BusinessException("Ödeme sağlayıcısına ulaşılamadı. Lütfen biraz sonra tekrar deneyin.");
            }

            using (response)
            {
                var body = await response.Content.ReadAsStringAsync(cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("PayTR {Url} -> {StatusCode}: {Body}", url, (int)response.StatusCode, body);
                    throw new BusinessException($"PayTR hata döndürdü ({(int)response.StatusCode}).");
                }

                return body;
            }
        }

        // PAY_TR'deki CardBinResponse
        private sealed class BinDetailResponse
        {
            [JsonPropertyName("status")] public string? Status { get; set; }
            [JsonPropertyName("brand")] public string? Brand { get; set; }
            [JsonPropertyName("cardType")] public string? CardType { get; set; }
            [JsonPropertyName("err_msg")] public string? ErrMsg { get; set; }
        }
    }
}