using Bagery.WebUI.Enums;
using Bagery.WebUI.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Bagery.WebUI.Services.RealtimeServices
{
    public class DeliveryNotifier(IHubContext<OrderHub> _hub,
                                  ILogger<DeliveryNotifier> _logger) : IDeliveryNotifier
    {
        public async Task NewPaidOrderAsync(string orderNo, string customerName)
        {
            try
            {
                // Personel ekranlarına: "yeni sipariş geldi"
                await _hub.Clients.Group(OrderHub.StaffGroup)
                          .SendAsync("orderReceived", new { orderNo, customerName });
            }
            catch (Exception ex)
            {
                // Bildirim gitmese bile sipariş kaydedildi: hata akışı durdurmasın
                _logger.LogError(ex, "Yeni sipariş bildirimi gönderilemedi. OrderNo: {OrderNo}", orderNo);
            }
        }

        public async Task DeliveryChangedAsync(string orderNo, DeliveryStatus status, DateTime? dispatchedAt, DateTime? deliveredAt)
        {
            var payload = new
            {
                orderNo,
                status = status.ToString(),
                statusText = status.ToTurkish(),
                dispatchedAt,
                deliveredAt
            };

            try
            {
                // Panolar: veriyi tazelesinler
                await _hub.Clients.Group(OrderHub.StaffGroup).SendAsync("deliveryChanged", payload);

                // Siparişi izleyen müşteri: adımlar ilerlesin
                await _hub.Clients.Group(OrderHub.OrderGroup(orderNo)).SendAsync("deliveryChanged", payload);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Teslimat bildirimi gönderilemedi. OrderNo: {OrderNo}", orderNo);
            }
        }
    }
}