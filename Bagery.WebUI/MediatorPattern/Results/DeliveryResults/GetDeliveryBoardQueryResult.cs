namespace Bagery.WebUI.MediatorPattern.Results.DeliveryResults
{
    public class GetDeliveryBoardQueryResult
    {
        public DateTime ServerTimeUtc { get; set; }                  // sayaçlar bu saate göre işler
        public List<DeliveryCard> Waiting { get; set; } = [];        // en eski üstte
        public List<DeliveryCard> OnTheWay { get; set; } = [];       // önce çıkan üstte
        public List<DeliveryCard> DeliveredToday { get; set; } = []; // en yeni üstte
    }

    // Panodaki tek kart
    public record DeliveryCard(
        string OrderNo,
        string FullName,
        string PhoneNumber,
        string Address,
        string? Note,
        decimal PaidPrice,
        DateTime PaidAt,
        DateTime? DispatchedAt,
        string? DispatchedBy,
        DateTime? DeliveredAt,
        string? DeliveredBy,
        List<DeliveryCardItem> Items);

    public record DeliveryCardItem(string Name, string? Variant, int Quantity);
}