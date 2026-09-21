using Bagery.WebUI.Entities;
using Bagery.WebUI.Repositories.GenericRepositories;

namespace Bagery.WebUI.Repositories.CouponRepositories
{
    public interface ICouponRepository : IGenericRepository<Coupon>
    {
        Task<Coupon?> GetActiveByCodeAsync(string couponCode);
        Task<bool> CodeExistsAsync(string couponCode, Guid? exceptId = null); // YENİ: aynı kod ikinci kez eklenmesin
        Task<List<Coupon>> GetActiveCouponsAsync();                           // YENİ: sepette gösterilecekler
    }
}
