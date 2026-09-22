namespace Bagery.WebUI.Enums
{
    public static class DeliveryStatusExtensions
    {
        // Kullanım: order.DeliveryStatus.ToTurkish() -> "Yolda"
        public static string ToTurkish(this DeliveryStatus status) => status switch
        {
            DeliveryStatus.OnTheWay => "Yolda",
            DeliveryStatus.Delivered => "Teslim edildi",
            _ => "Bekliyor"
        };
    }
}