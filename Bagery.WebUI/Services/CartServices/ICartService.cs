using Bagery.WebUI.Models.CartModels;

namespace Bagery.WebUI.Services.CartServices
{
    // Bu servisin tek işi sepetin NEREDE saklandığı. Kurallar Cart'ta, akış handler'larda.
    // İleride sepeti veritabanına taşımak istersen sadece bu servisi değiştirirsin.
    public interface ICartService
    {
        Cart GetCart();
        void SaveCart(Cart cart);
        void ClearCart();
    }
}