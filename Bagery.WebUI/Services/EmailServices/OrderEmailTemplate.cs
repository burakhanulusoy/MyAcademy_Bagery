using Bagery.WebUI.Entities;
using System.Globalization;
using System.Net;
using System.Text;

namespace Bagery.WebUI.Services.EmailServices
{
    public static class OrderEmailTemplate
    {
        // EmailService logoyu bu kimlikle ekler, HTML "cid:bagery-logo" ile çağırır
        public const string LogoContentId = "bagery-logo";

        private static readonly CultureInfo Tr = new("tr-TR");

        public static string BuildPaidOrderEmail(Order order, string? invoiceUrl)
        {
            // Ürün satırları
            var itemRows = new StringBuilder();
            foreach (var item in order.OrderItems)
            {
                var variant = item.VariantName is null ? "" : $" ({Encode(item.VariantName)})";
                itemRows.Append($"""
                    <tr>
                      <td style="padding:12px 0;border-bottom:1px solid #eeeeee;color:#222222;">{Encode(item.ProductName)}<span style="color:#888888;">{variant}</span></td>
                      <td style="padding:12px 0;border-bottom:1px solid #eeeeee;text-align:center;color:#222222;">{item.Quantity}</td>
                      <td style="padding:12px 0;border-bottom:1px solid #eeeeee;text-align:right;color:#222222;">{Money(item.UnitPrice * item.Quantity)}</td>
                    </tr>
                    """);
            }

            // Toplamlar
            var installmentFee = order.PaidPrice - (order.TotalPrice + order.ShippingPrice - order.CouponPrice);
            var totals = new StringBuilder();
            totals.Append(TotalRow("Ara toplam", Money(order.TotalPrice)));
            if (order.CouponPrice > 0)
                totals.Append(TotalRow($"İndirim ({Encode(order.CouponCode)})", "-" + Money(order.CouponPrice)));
            totals.Append(TotalRow("Kargo", order.ShippingPrice == 0 ? "Ücretsiz" : Money(order.ShippingPrice)));
            if (installmentFee > 0)
                totals.Append(TotalRow($"Vade farkı ({order.InstallmentCount} taksit)", Money(installmentFee)));
            totals.Append(TotalRow("Ödenen tutar", Money(order.PaidPrice), isGrandTotal: true));

            var invoiceNo = "BGR-" + order.OrderNo[..8].ToUpperInvariant();
            var paidAt = (order.PaidAt ?? order.CreatedAt).ToLocalTime().ToString("dd.MM.yyyy HH:mm", Tr);
            var paymentText = order.InstallmentCount == 0 ? "Tek çekim" : $"{order.InstallmentCount} taksit";

            // baseUrl yoksa buton hiç konmaz
            var invoiceButton = string.IsNullOrWhiteSpace(invoiceUrl)
                ? ""
                : $"""<a href="{invoiceUrl}" style="display:inline-block;background:#e3a087;color:#ffffff;text-decoration:none;font-weight:bold;padding:14px 32px;border-radius:4px;">Faturayı görüntüle</a>""";

            return $"""
                <div style="background:#f6f3f1;padding:30px 0;font-family:Arial,Helvetica,sans-serif;">
                  <table role="presentation" width="600" cellpadding="0" cellspacing="0" align="center" style="max-width:600px;width:100%;background:#ffffff;border-radius:8px;overflow:hidden;">
                    <tr>
                      <td style="padding:28px 40px;border-bottom:4px solid #e3a087;">
                        <img src="cid:{LogoContentId}" alt="Bagery" width="170" style="display:block;border:0;">
                      </td>
                    </tr>
                    <tr>
                      <td style="padding:32px 40px 8px 40px;">
                        <h1 style="margin:0 0 10px 0;font-size:22px;color:#2b3c6b;">Siparişiniz alındı</h1>
                        <p style="margin:0;font-size:15px;line-height:24px;color:#555555;">Merhaba {Encode(order.FullName)}, ödemeniz onaylandı ve siparişiniz hazırlanmaya başladı. Fatura bilgileriniz aşağıda.</p>
                      </td>
                    </tr>
                    <tr>
                      <td style="padding:20px 40px;">
                        <table role="presentation" width="100%" cellpadding="0" cellspacing="0" style="background:#fcf5f3;border-radius:6px;">
                          <tr>
                            <td style="padding:14px 18px;font-size:12px;color:#888888;">Belge no<br><strong style="font-size:14px;color:#222222;">{invoiceNo}</strong></td>
                            <td style="padding:14px 18px;font-size:12px;color:#888888;">Tarih<br><strong style="font-size:14px;color:#222222;">{paidAt}</strong></td>
                            <td style="padding:14px 18px;font-size:12px;color:#888888;">Ödeme<br><strong style="font-size:14px;color:#222222;">{paymentText}</strong></td>
                          </tr>
                        </table>
                      </td>
                    </tr>
                    <tr>
                      <td style="padding:10px 40px;">
                        <table role="presentation" width="100%" cellpadding="0" cellspacing="0" style="font-size:14px;">
                          <tr>
                            <th align="left" style="padding-bottom:8px;border-bottom:2px solid #2b3c6b;color:#2b3c6b;">Ürün</th>
                            <th style="padding-bottom:8px;border-bottom:2px solid #2b3c6b;color:#2b3c6b;">Adet</th>
                            <th align="right" style="padding-bottom:8px;border-bottom:2px solid #2b3c6b;color:#2b3c6b;">Tutar</th>
                          </tr>
                          {itemRows}
                        </table>
                      </td>
                    </tr>
                    <tr>
                      <td style="padding:12px 40px 20px 40px;">
                        <table role="presentation" width="100%" cellpadding="0" cellspacing="0" style="font-size:14px;">
                          {totals}
                        </table>
                      </td>
                    </tr>
                    <tr>
                      <td style="padding:8px 40px;">
                        <p style="margin:0 0 4px 0;font-size:12px;color:#888888;">Teslimat adresi</p>
                        <p style="margin:0;font-size:14px;line-height:22px;color:#222222;">{Encode(order.FullName)}<br>{Encode(order.Address)}<br>{Encode(order.PhoneNumber)}</p>
                      </td>
                    </tr>
                    <tr>
                      <td style="padding:28px 40px;text-align:center;">{invoiceButton}</td>
                    </tr>
                    <tr>
                      <td style="padding:18px 40px;background:#2b3c6b;color:#c9d0e0;font-size:12px;line-height:18px;text-align:center;">
                        Bu e-posta Bagery siparişiniz için otomatik gönderildi.<br>Fiyatlara KDV dahildir. Bu belge e-Arşiv fatura yerine geçmez.
                      </td>
                    </tr>
                  </table>
                </div>
                """;
        }

        // Toplamlar tablosunun tek satırı; son satır (genel toplam) kalın ve lacivert
        private static string TotalRow(string label, string value, bool isGrandTotal = false)
        {
            var style = isGrandTotal
                ? "padding:12px 0 0 0;border-top:2px solid #2b3c6b;font-size:16px;font-weight:bold;color:#2b3c6b;"
                : "padding:6px 0;color:#555555;";
            return $"""<tr><td style="{style}">{label}</td><td align="right" style="{style}">{value}</td></tr>""";
        }

        private static string Money(decimal value) => value.ToString("N2", Tr) + " ₺";

        // Müşterinin yazdığı metin (ad, adres) HTML'e girmeden önce etkisizleştirilir
        private static string Encode(string? text) => WebUtility.HtmlEncode(text ?? string.Empty);
    }
}