using Bagery.WebUI.Entities.Common;

namespace Bagery.WebUI.Entities
{
    public class OrderItem:BaseEntity
    {
        public Guid ProductId { get; set; }          // sadece referans, navigation YOK
        public Guid? ProductVariantId { get; set; }
        public string ProductName { get; set; }
        public string? VariantName { get; set; }
        public string? ImageUrl { get; set; }
        public decimal UnitPrice { get; set; }       // ürün fiyatı + varyant ek fiyatı
        public int Quantity { get; set; }

        public Guid OrderId { get; set; }
        public Order Order { get; set; }
    }
}
