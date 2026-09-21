namespace Bagery.WebUI.MediatorPattern.Results.OrderResults
{
    // Detay sayfasındaki ürün satırı
    public class GetAdminOrderItemResult
    {
        public string ProductName { get; set; } = string.Empty;
        public string? VariantName { get; set; }
        public string? ImageUrl { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal LineTotal => UnitPrice * Quantity;
    }
}