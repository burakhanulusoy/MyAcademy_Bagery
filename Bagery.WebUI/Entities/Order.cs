using Bagery.WebUI.Entities.Common;
using Bagery.WebUI.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bagery.WebUI.Entities
{
    public class Order:BaseEntity
    {
        public string OrderNo { get; set; }          // PayTR merchant_oid
        public OrderStatus Status { get; set; }
        public string? StatusMessage { get; set; }   // callback'ten gelen sonuç / hata mesajı

        public decimal TotalPrice { get; set; }      // ürünlerin toplamı (ara toplam)
        public decimal ShippingPrice { get; set; }
        public string? CouponCode { get; set; }
        public decimal CouponPrice { get; set; }     // uygulanan indirim tutarı
        public decimal PaidPrice { get; set; }       // karttan çekilen nihai tutar (taksit farkı dahil)
        public int InstallmentCount { get; set; }    // 0 = tek çekim
        public string? CardBrand { get; set; }       // taksitte kart ailesi: world, bonus...


        // Teslimat bilgileri: siparişin anlık kopyası
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string? Note { get; set; }

        public DateTime? PaidAt { get; set; }

        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public IList<OrderItem> OrderItems { get; set; } = new List<OrderItem>();


    }
}
