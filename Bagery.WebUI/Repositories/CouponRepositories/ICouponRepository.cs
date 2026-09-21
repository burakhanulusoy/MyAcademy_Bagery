using Bagery.WebUI.Entities;
using Bagery.WebUI.Repositories.GenericRepositories;

namespace Bagery.WebUI.Repositories.CouponRepositories
{
    public interface ICouponRepository : IGenericRepository<Coupon>
    {
        Task<Coupon?> GetActiveByCodeAsync(string couponCode);
    }
}
