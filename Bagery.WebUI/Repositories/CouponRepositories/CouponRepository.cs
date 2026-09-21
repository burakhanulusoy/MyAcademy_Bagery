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
        // YENİ: düzenlemede kuponun kendisi hariç tutulur (exceptId), yoksa "kendi kodu zaten var" derdi
        public Task<bool> CodeExistsAsync(string couponCode, Guid? exceptId = null)
        {
            var code = couponCode.Trim().ToUpperInvariant();
            return _table.AnyAsync(x => x.CouponCode == code && (exceptId == null || x.Id != exceptId));
        }

        // YENİ: sadece aktif kuponlar, en düşük sepet şartından başlayarak
        public Task<List<Coupon>> GetActiveCouponsAsync()
        {
            return _table.AsNoTracking()
                         .Where(x => x.IsActive)
                         .OrderBy(x => x.MinPrice)
                         .ToListAsync();
        }
    }
}