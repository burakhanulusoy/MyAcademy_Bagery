using Bagery.WebUI.Enums;

namespace Bagery.WebUI.MediatorPattern.Results.OrderResults
{
    // Tablodaki tek satır: sadece listede gereken alanlar
    public class GetAdminOrderListItemResult
    {
        public string OrderNo { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int ItemCount { get; set; }                 // toplam ürün adedi
        public decimal PaidPrice { get; set; }
        public int InstallmentCount { get; set; }
        public OrderStatus Status { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; } // YENİ
        public DateTime? DispatchedAt { get; set; } // YENİ
        public DateTime? DeliveredAt { get; set; }  // YENİ
    }
}