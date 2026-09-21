using Bagery.WebUI.Enums;
using Bagery.WebUI.MediatorPattern.Commands.PaymentCommands;
using Bagery.WebUI.Repositories.OrderRepositories;
using Bagery.WebUI.Services.EmailServices;
using Bagery.WebUI.Services.PayTRServices;
using Bagery.WebUI.UOW;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.PaymentHandlers
{
    public class ProcessPaymentCallbackCommandHandler(IPayTRService _payTRService,
                                                      IOrderRepository _orderRepository,
                                                      IUnitOfWork _unitOfWork,
                                                      IEmailService _emailService,        // YENİ
                                                      IWebHostEnvironment _environment,   // YENİ: logo dosyasının yolu için
                                                      IConfiguration _configuration,      // YENİ: fatura linki (baseUrl)
                                                      ILogger<ProcessPaymentCallbackCommandHandler> _logger) : IRequestHandler<ProcessPaymentCallbackCommand, string>
    {
        private const string Ok = "OK";

        public async Task<string> Handle(ProcessPaymentCallbackCommand request, CancellationToken cancellationToken)
        {
            // 1) Önce hash
            if (!_payTRService.IsCallbackHashValid(request.MerchantOid, request.Status, request.TotalAmount, request.Hash))
            {
                _logger.LogWarning("PayTR callback hash uyuşmadı. OrderNo: {OrderNo}", request.MerchantOid);
                return "PAYTR notification failed: bad hash";
            }

            // 2) Siparişi bul
            var order = await _orderRepository.GetByOrderNoAsync(request.MerchantOid);
            if (order is null)
            {
                _logger.LogWarning("PayTR callback: sipariş bulunamadı. OrderNo: {OrderNo}", request.MerchantOid);
                return Ok;
            }

            // 3) Daha önce işlendiyse tekrar işleme (e-posta da ikinci kez gitmez)
            if (order.Status != OrderStatus.Pending)
                return Ok;

            // 4) Sonucu yaz
            if (request.Status == "success")
            {
                order.Status = OrderStatus.Paid;
                order.StatusMessage = "Ödeme başarılı.";
                order.PaidAt = DateTime.UtcNow;
            }
            else
            {
                order.Status = OrderStatus.Failed;
                order.StatusMessage = string.IsNullOrWhiteSpace(request.FailedReasonMsg)
                    ? "Ödeme başarısız."
                    : $"{request.FailedReasonMsg} (Kod: {request.FailedReasonCode})";
            }

            _orderRepository.Update(order);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("PayTR callback işlendi. OrderNo: {OrderNo}, Durum: {Status}", order.OrderNo, order.Status);

            // 5) YENİ: sadece başarılı ödemede fatura e-postası (başarısızda gönderilmiyor)
            if (order.Status == OrderStatus.Paid)
                await SendPaidEmailAsync(order.OrderNo);

            return Ok;
        }

        // E-posta gitmese bile ödeme kaydedildi; hata PayTR'ye "OK" dönmeyi engellememeli.
        // Bu yüzden her şey try-catch içinde, hata sadece loglanıyor.
        private async Task SendPaidEmailAsync(string orderNo)
        {
            try
            {
                // Ürünlerle birlikte tekrar oku (Adım 12'deki metot)
                var order = await _orderRepository.GetOrderDetailForAdminAsync(orderNo);
                if (order is null) return;

                // Fatura butonu: baseUrl (ngrok / canlı alan adı) varsa eklenir
                var baseUrl = _configuration["PayTR:baseUrl"]?.TrimEnd('/');
                var invoiceUrl = string.IsNullOrWhiteSpace(baseUrl)
                    ? null
                                       : $"{baseUrl}/Admin/MyOrders/Invoice?orderNo={order.OrderNo}"; // DEĞİŞTİ: panel adresi

                // wwwroot'taki logo dosyasının diskteki tam yolu
                var logoPath = Path.Combine(_environment.WebRootPath, "Bagery Pack", "Bagery", "assets", "images", "logo-4.png");

                await _emailService.SendOrderConfirmationAsync(
                    order.Email,
                    order.FullName,
                    $"Bagery - Siparişiniz alındı (BGR-{order.OrderNo[..8].ToUpperInvariant()})",
                    OrderEmailTemplate.BuildPaidOrderEmail(order, invoiceUrl),
                    logoPath);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sipariş e-postası gönderilemedi. OrderNo: {OrderNo}", orderNo);
            }
        }
    }
}