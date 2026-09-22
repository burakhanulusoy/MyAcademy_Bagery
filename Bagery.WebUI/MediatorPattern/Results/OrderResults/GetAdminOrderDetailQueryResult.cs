using Bagery.WebUI.Enums;

namespace Bagery.WebUI.MediatorPattern.Results.OrderResults
{
    public class GetAdminOrderDetailQueryResult
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
        public string? CardBrand { get; set; }

        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string? Note { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? PaidAt { get; set; }

        // Mapster: AppUser.FullName -> AppUserFullName
        public string AppUserFullName { get; set; } = string.Empty;
        public string? AppUserEmail { get; set; }

        // YENİ: teslimat (Mapster isimden eşleştirir)
        public DeliveryStatus DeliveryStatus { get; set; }
        public DateTime? DispatchedAt { get; set; }
        public string? DispatchedBy { get; set; }
        public DateTime? DeliveredAt { get; set; }
        public string? DeliveredBy { get; set; }
        public List<GetAdminDeliveryLogResult> DeliveryLogs { get; set; } = [];

        public List<GetAdminOrderItemResult> OrderItems { get; set; } = [];
    }
}