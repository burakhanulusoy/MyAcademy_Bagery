using Bagery.WebUI.Enums;

namespace Bagery.WebUI.MediatorPattern.Results.OrderResults
{
    // Order entity'sinin sayfada gösterilecek kısmı (Mapster isimleri eşleştirerek doldurur)
    public class GetMyOrderQueryResult
    {
        public string OrderNo { get; set; } = string.Empty;
        public OrderStatus Status { get; set; }
        public string? StatusMessage { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal ShippingPrice { get; set; }
        public string? CouponCode { get; set; }
        public decimal CouponPrice { get; set; }
        public decimal PaidPrice { get; set; }
        public int InstallmentCount { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }   // BaseEntity'den geliyor
        public List<GetMyOrderItemQueryResult> OrderItems { get; set; } = [];
    }
}