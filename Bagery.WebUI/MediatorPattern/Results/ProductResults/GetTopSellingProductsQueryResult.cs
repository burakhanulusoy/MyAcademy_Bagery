namespace Bagery.WebUI.MediatorPattern.Results.ProductResults
{
    public class GetTopSellingProductsQueryResult
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }                        // ürünün GÜNCEL fiyatı
        public string MainImageUrl { get; set; } = string.Empty;
        public int TotalSold { get; set; }                        // ödenmiş siparişlerde toplam satılan adet
    }
}