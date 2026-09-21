using Bagery.WebUI.Context;
using Bagery.WebUI.Entities;
using Bagery.WebUI.Repositories.GenericRepositories;
using Microsoft.EntityFrameworkCore;

namespace Bagery.WebUI.Repositories.InstallmentRepositories
{
    public class InstallmentRepository : GenericRepository<Installment>, IInstallmentRepository
    {
        public InstallmentRepository(AppDbContext _context) : base(_context)
        {
        }

        // Taksit tablosu: bu kart ailesinin bütün taksitleri (2, 3, 6, 9...)
        public Task<List<Installment>> GetByBrandAsync(string brand)
        {
            return _table.AsNoTracking()
                         .Where(x => x.Brand == brand)
                         .OrderBy(x => x.InstallmentCount)
                         .ToListAsync();
        }

        // Ödeme anı: kullanıcının seçtiği tek taksitin oranı
        public Task<Installment?> GetAsync(string brand, int installmentCount)
        {
            return _table.AsNoTracking()
                         .FirstOrDefaultAsync(x => x.Brand == brand && x.InstallmentCount == installmentCount);
        }

        public Task AddRangeAsync(IEnumerable<Installment> installments)
        {
            return _table.AddRangeAsync(installments);
        }

        public Task RemoveAllAsync()
        {
            return _table.IgnoreQueryFilters().ExecuteDeleteAsync();  // böyle yapma nedenim her değişiklikte soft delete yapma cunku ger guncellemede soft delete uzerıne yazılan cok databasei şişiriidi boyle olması gerekıyır
        }
    }
}
