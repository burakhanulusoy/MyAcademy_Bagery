namespace Bagery.WebUI.MediatorPattern.Results.OrderResults
{
    public class GetMyOrderItemQueryResult
    {
        public string ProductName { get; set; } = string.Empty;
        public string? VariantName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal LineTotal => UnitPrice * Quantity; // hesaplanan alan, Mapster buna dokunmaz
    }
}