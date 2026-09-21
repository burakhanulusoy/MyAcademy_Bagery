namespace Bagery.WebUI.MediatorPattern.Results.CouponResults
{
    // Admin listesi
    public class GetCouponsQueryResult
    {
        public Guid Id { get; set; }
        public string CouponCode { get; set; } = string.Empty;
        public decimal CouponPrice { get; set; }
        public decimal MinPrice { get; set; }
        public bool IsActive { get; set; }
        public int UsageCount { get; set; }   // kaç ÖDENMİŞ siparişte kullanıldı
    }
}