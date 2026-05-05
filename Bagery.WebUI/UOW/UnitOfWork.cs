
using Bagery.WebUI.Context;

namespace Bagery.WebUI.UOW
{
    public class UnitOfWork(AppDbContext _context) : IUnitOfWork
    {
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;

        }
    }
}
