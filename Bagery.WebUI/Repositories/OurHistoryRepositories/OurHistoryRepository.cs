using Bagery.WebUI.Context;
using Bagery.WebUI.Entities;
using Bagery.WebUI.Repositories.GenericRepositories;

namespace Bagery.WebUI.Repositories.OurHistoryRepositories
{
    public class OurHistoryRepository : GenericRepository<OurHistory>, IOurHistoryRepository
    {
        public OurHistoryRepository(AppDbContext _context) : base(_context)
        {
        }



    }
}
