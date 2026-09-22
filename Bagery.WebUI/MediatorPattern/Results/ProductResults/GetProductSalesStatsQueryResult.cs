namespace Bagery.WebUI.MediatorPattern.Results.ProductResults
{
    public class GetProductSalesStatsQueryResult
    {
        public int SoldQuantity { get; set; }       // toplam satılan adet
        public decimal Revenue { get; set; }        // bu üründen gelen ciro
        public int OrderCount { get; set; }         // kaç siparişte geçti
        public DateTime? LastSoldAt { get; set; }
        public List<ProductMonthSale> Months { get; set; } = []; // son 6 ay
    }

    public record ProductMonthSale(string Label, int Quantity);
}