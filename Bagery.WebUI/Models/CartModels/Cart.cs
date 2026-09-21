namespace Bagery.WebUI.Models.CartModels
{
    public class Cart
    {
        public const decimal FreeShippingLimit = 5000m; // Sepet 5000 ₺'yi geçerse kargo ücretsiz
        public const decimal ShippingFee = 150m;//Geçmezse alınan kargo ücreti	
        public const int MaxQuantityPerItem = 50;       // bir üründen en fazla kaç adet

        public List<CartItem> Items { get; set; } = [];

        public string? CouponCode { get; set; }
        public decimal CouponPrice { get; set; }        // kuponun değeri
        public decimal CouponMinPrice { get; set; }

        // Aşağıdakileri Recalculate() doldurur, elle set etme
        public int TotalQuantity { get; set; }          // header'daki sepet rozetindeki sayı
        public decimal SubTotal { get; set; }
        public decimal CouponDiscount { get; set; }     // gerçekte düşülen indirim
        public decimal ShippingPrice { get; set; }
        public decimal PaidPrice { get; set; }

        public bool IsEmpty => Items.Count == 0;

        public void AddItem(CartItem item)
        {
            var existing = Items.FirstOrDefault(x => x.IsSame(item.ProductId, item.VariantId));

            if (existing is null)
            {
                item.Quantity = Math.Min(item.Quantity, MaxQuantityPerItem);
                Items.Add(item);
            }
            else
            {
                // PAY_TR'deki gibi adedi artır, ama sınırı aşmasın
                existing.Quantity = Math.Min(existing.Quantity + item.Quantity, MaxQuantityPerItem);
                existing.UnitPrice = item.UnitPrice; // admin fiyatı değiştirdiyse güncel fiyat geçsin
            }

            Recalculate();
        }

        public void DecreaseItem(Guid productId, Guid? variantId)
        {
            var existing = Items.FirstOrDefault(x => x.IsSame(productId, variantId));
            if (existing is null) return;

            existing.Quantity--;
            if (existing.Quantity <= 0) Items.Remove(existing); // 1'den 0'a düşünce satır gitsin

            Recalculate();
        }

        public void RemoveItem(Guid productId, Guid? variantId)
        {
            Items.RemoveAll(x => x.IsSame(productId, variantId));
            Recalculate();
        }

        // "Sepet kupon için yeterli mi?" kontrolü handler'da yapılacak,
        // çünkü hata mesajını kullanıcıya oradan döneceğiz. Burası sadece uygular.
        public void ApplyCoupon(string couponCode, decimal couponPrice, decimal minPrice)
        {
            CouponCode = couponCode;
            CouponPrice = couponPrice;
            CouponMinPrice = minPrice;
            Recalculate();
        }

        public void RemoveCoupon()
        {
            CouponCode = null;
            CouponPrice = 0;
            CouponMinPrice = 0;
            Recalculate();
        }

        // PAY_TR'deki CalculateCart'ın karşılığı
        public void Recalculate()
        {
            TotalQuantity = Items.Sum(x => x.Quantity);
            SubTotal = Items.Sum(x => x.LineTotal);

            // PAY_TR'deki kural aynı; tek fark: boş sepette 150 TL kargo göstermesin
            ShippingPrice = SubTotal == 0 || SubTotal > FreeShippingLimit ? 0 : ShippingFee;

            // Ürün çıkarılınca sepet kupon limitinin altına düştüyse kupon düşer (PAY_TR'deki kural)
            if (CouponCode is not null && SubTotal < CouponMinPrice)
            {
                CouponCode = null;
                CouponPrice = 0;
                CouponMinPrice = 0;
            }

            // İndirim ara toplamı geçemez; yoksa ödenecek tutar eksiye düşer
            CouponDiscount = Math.Min(CouponPrice, SubTotal);
            PaidPrice = SubTotal + ShippingPrice - CouponDiscount;
        }
    }
}