using Bagery.WebUI.Enums;

namespace Bagery.WebUI.MediatorPattern.Results.OrderResults
{
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
        public string? CardBrand { get; set; }                     // YENİ (fatura)
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;          // YENİ (fatura)
        public string PhoneNumber { get; set; } = string.Empty;    // YENİ (fatura)
        public string Address { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? PaidAt { get; set; }                      // YENİ (fatura tarihi)
        public List<GetMyOrderItemQueryResult> OrderItems { get; set; } = [];
    }
}