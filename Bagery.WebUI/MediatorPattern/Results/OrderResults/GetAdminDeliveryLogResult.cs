namespace Bagery.WebUI.MediatorPattern.Results.OrderResults
{
    // Kanıt çizelgesindeki tek satır (OrderDeliveryLog'dan Mapster ile dolar)
    public class GetAdminDeliveryLogResult
    {
        public string Action { get; set; } = string.Empty;
        public string PerformedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}