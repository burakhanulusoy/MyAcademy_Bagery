using Bagery.WebUI.Entities;
using Bagery.WebUI.Enums;
using Bagery.WebUI.MediatorPattern.Commands.PaymentCommands;
using Bagery.WebUI.Repositories.OrderRepositories;
using Bagery.WebUI.Services.EmailServices;
using Bagery.WebUI.Services.PayTRServices;
using Bagery.WebUI.Services.RealtimeServices;
using Bagery.WebUI.UOW;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Bagery.WebUI.MediatorPattern.Handlers.PaymentHandlers
{
    public class ProcessPaymentCallbackCommandHandler(IPayTRService _payTRService,
                                                      IOrderRepository _orderRepository,
                                                      IUnitOfWork _unitOfWork,
                                                      IEmailService _emailService,
                                                      IWebHostEnvironment _environment,   // logo dosyasının yolu için
                                                      IConfiguration _configuration,      // fatura linki (baseUrl)
                                                      UserManager<AppUser> _userManager,  // fatura linki için sipariş sahibinin rolü
                                                      IDeliveryNotifier _notifier,        // canlı bildirim (SignalR)
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

            // 3) Daha önce işlendiyse tekrar işleme (e-posta ve bildirim ikinci kez gitmez)
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

            // 5) Sadece başarılı ödemede: önce garson panosuna haber, sonra fatura e-postası
            if (order.Status == OrderStatus.Paid)
            {
                // YENİ: pano saniyesinde bilsin (e-posta yavaş olsa bile mutfak beklemesin)
                await _notifier.NewPaidOrderAsync(order.OrderNo, order.FullName);
                await SendPaidEmailAsync(order.OrderNo);
            }

            return Ok;
        }

        // E-posta gitmese bile ödeme kaydedildi; hata PayTR'ye "OK" dönmeyi engellememeli.
        private async Task SendPaidEmailAsync(string orderNo)
        {
            try
            {
                // Ürünler ve sipariş sahibiyle birlikte oku
                var order = await _orderRepository.GetOrderDetailForAdminAsync(orderNo);
                if (order is null) return;

                // Fatura linki sipariş sahibinin kendi paneline gider
                var baseUrl = _configuration["PayTR:baseUrl"]?.TrimEnd('/');
                string? invoiceUrl = null;
                if (!string.IsNullOrWhiteSpace(baseUrl))
                {
                    var area = await PanelAreaOfAsync(order.AppUser);
                    invoiceUrl = $"{baseUrl}/{area}/MyOrders/Invoice?orderNo={order.OrderNo}";
                }

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

        // Sipariş sahibinin Siparişlerim sayfası hangi panelde
        private async Task<string> PanelAreaOfAsync(AppUser? user)
        {
            if (user is null) return "User";

            var roles = await _userManager.GetRolesAsync(user);
            return roles.Contains("Admin") ? "Admin"
                 : roles.Contains("Writer") ? "Writer"
                 : "User";
        }
    }
}