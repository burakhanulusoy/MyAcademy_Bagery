using Bagery.WebUI.Entities.Common;

namespace Bagery.WebUI.Entities
{
    public class Coupon:BaseEntity
    {
        public string CouponCode { get; set; }       // BÜYÜK harfle kaydet: HOSGELDIN50
        public decimal CouponPrice { get; set; }     // indirim tutarı (₺)
        public decimal MinPrice { get; set; }        // sepet en az bu kadar olmalı
        public bool IsActive { get; set; }
    }
}
