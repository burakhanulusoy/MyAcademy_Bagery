namespace Bagery.WebUI.Enums
{
    public static class OrderStatusExtensions
    {
        // Kullanım: order.Status.ToTurkish()  ->  "Ödendi"
        public static string ToTurkish(this OrderStatus status) => status switch
        {
            OrderStatus.Paid => "Ödendi",
            OrderStatus.Failed => "Başarısız",
            _ => "Bekliyor" // Pending
        };
    }
}