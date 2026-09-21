using Bagery.WebUI.Context;
using Bagery.WebUI.Entities;
using Bagery.WebUI.Repositories.GenericRepositories;
using Microsoft.EntityFrameworkCore;

namespace Bagery.WebUI.Repositories.CouponRepositories
{
    public class CouponRepository : GenericRepository<Coupon>, ICouponRepository
    {
        public CouponRepository(AppDbContext _context) : base(_context)
        {
        }

        public Task<Coupon?> GetActiveByCodeAsync(string couponCode)
        {
            var code = couponCode.Trim().ToUpperInvariant();  // to upper ile farkı toupper HOSGELDİN50  toupperInvariant HOSGELDIN50 ENG UYUMLU YANI   

            return _table.AsNoTracking()
                         .FirstOrDefaultAsync(x => x.IsActive && x.CouponCode == code);
        }
    }
}