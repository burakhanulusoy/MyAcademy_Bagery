using Bagery.WebUI.Context;
using Bagery.WebUI.Entities;
using Bagery.WebUI.Repositories.GenericRepositories;
using Microsoft.EntityFrameworkCore;

namespace Bagery.WebUI.Repositories.OurHistoryRepositories
{
    public class OurHistoryRepository : GenericRepository<OurHistory>, IOurHistoryRepository
    {
        public OurHistoryRepository(AppDbContext _context) : base(_context)
        {
        }

        public Task<OurHistory> GetOurHistoryLastAsync()
        {
            return _table.AsNoTracking().OrderBy(x => x.Id).FirstOrDefaultAsync();
        }
    }
}
