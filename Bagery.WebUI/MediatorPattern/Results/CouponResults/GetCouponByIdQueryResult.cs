namespace Bagery.WebUI.MediatorPattern.Results.CouponResults
{
    // Düzenleme formunu doldurmak için
    public class GetCouponByIdQueryResult
    {
        public Guid Id { get; set; }
        public string CouponCode { get; set; } = string.Empty;
        public decimal CouponPrice { get; set; }
        public decimal MinPrice { get; set; }
        public bool IsActive { get; set; }
    }
}