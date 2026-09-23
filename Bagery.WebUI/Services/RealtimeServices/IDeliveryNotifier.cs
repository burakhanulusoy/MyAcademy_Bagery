using Bagery.WebUI.Enums;

namespace Bagery.WebUI.Services.RealtimeServices
{
    // Handler'lar bunu çağırır; SignalR ayrıntısı arkada kalır
    public interface IDeliveryNotifier
    {
        // Ödeme onaylandı: garson panosuna yeni sipariş düşsün (ses + vurgu)
        Task NewPaidOrderAsync(string orderNo, string customerName);

        // Teslimat durumu değişti: panolar ve müşterinin ekranı güncellensin
        Task DeliveryChangedAsync(string orderNo, DeliveryStatus status, DateTime? dispatchedAt, DateTime? deliveredAt);
    }
}