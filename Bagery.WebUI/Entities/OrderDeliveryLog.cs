using Bagery.WebUI.Entities.Common;

namespace Bagery.WebUI.Entities
{
    // Teslimat işlem geçmişi: kim, ne yaptı (saat: CreatedAt)
    public class OrderDeliveryLog : BaseEntity
    {
        public string Action { get; set; } = string.Empty;       // "Yola çıktı", "Teslim edildi", "Geri alındı: Yola çıktı"
        public string PerformedBy { get; set; } = string.Empty;  // işlemi yapanın adı (o anki hali, sonradan değişse de kayıt değişmez)

        public Guid OrderId { get; set; }
        public Order Order { get; set; } = null!;
    }
}