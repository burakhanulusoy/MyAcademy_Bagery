namespace Bagery.WebUI.MediatorPattern.Results.CouponResults
{
    // Sepet ve ödeme sayfasında müşteriye gösterilen alanlar (Id gerekmiyor)
    public class GetActiveCouponsQueryResult
    {
        public string CouponCode { get; set; } = string.Empty;
        public decimal CouponPrice { get; set; }
        public decimal MinPrice { get; set; }
    }
}