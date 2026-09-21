namespace Bagery.WebUI.Models.CartModels
{
    public class CartItem
    {
        public Guid ProductId { get; set; }          // PAY_TR'deki int CartItemId yerine: Bagery'de Id'ler Guid
        public Guid? VariantId { get; set; }         // seçenek seçilmediyse null
        public string ProductName { get; set; } = string.Empty;
        public string? VariantName { get; set; }
        public string? ImageUrl { get; set; }
        public decimal UnitPrice { get; set; }       // Price + AdditionalPrice, handler'da hesaplanır
        public int Quantity { get; set; }

        public decimal LineTotal => UnitPrice * Quantity; // sadece hesaplanır, session'dan okunmaz

        // Aynı ürünün farklı seçenekleri sepette ayrı satır olsun diye ikisine birden bakıyoruz
        public bool IsSame(Guid productId, Guid? variantId) => ProductId == productId && VariantId == variantId;
    }
}
