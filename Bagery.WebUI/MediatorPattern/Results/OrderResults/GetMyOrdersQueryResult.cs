using Bagery.WebUI.Enums;

namespace Bagery.WebUI.MediatorPattern.Results.OrderResults
{
    // Siparişlerim listesindeki tek satır
    public class GetMyOrdersQueryResult
    {
        public string OrderNo { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public OrderStatus Status { get; set; }
        public decimal PaidPrice { get; set; }
        public string FirstProductName { get; set; } = string.Empty; // "Caffè Latte ve 2 ürün daha" yazmak için
        public int ProductLineCount { get; set; }                    // kaç farklı ürün satırı var
        public DeliveryStatus DeliveryStatus { get; set; } // YENİ: kartta teslimat etiketi
        public DateTime? DispatchedAt { get; set; } // YENİ
        public DateTime? DeliveredAt { get; set; }  // YENİ
    }
}