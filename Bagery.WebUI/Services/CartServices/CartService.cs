using Bagery.WebUI.Extensions;
using Bagery.WebUI.Models.CartModels;

namespace Bagery.WebUI.Services.CartServices
{
    public class CartService(IHttpContextAccessor _httpContextAccessor) : ICartService
    {
        // PAY_TR'de önce Guid bir "SessionId" üretip sepeti onun altına koyuyordun.
        // Buna gerek yok: session zaten her tarayıcıya ayrı. Sabit bir anahtar yeterli.
        private const string CartSessionKey = "Bagery.Cart";

        private ISession Session =>
            _httpContextAccessor.HttpContext?.Session
            ?? throw new InvalidOperationException("Session bulunamadı. Program.cs'te app.UseSession() var mı?");

        public Cart GetCart()
        {
            return Session.GetObject<Cart>(CartSessionKey) ?? new Cart(); // ilk kez geliyorsa boş sepet
        }

        public void SaveCart(Cart cart)
        {
            cart.Recalculate(); // kaydetmeden önce tutarlar mutlaka güncel olsun
            Session.SetObject(CartSessionKey, cart);
        }

        public void ClearCart()
        {
            Session.Remove(CartSessionKey); // ödeme başarılı olunca Success sayfasında çağrılacak
        }
    }
}