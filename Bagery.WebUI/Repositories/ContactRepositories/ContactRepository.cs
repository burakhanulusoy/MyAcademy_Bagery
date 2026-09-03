using Bagery.WebUI.Context;
using Bagery.WebUI.Entities;
using Bagery.WebUI.Repositories.GenericRepositories;
using Microsoft.EntityFrameworkCore;

namespace Bagery.WebUI.Repositories.ContactRepositories
{
    public class ContactRepository : GenericRepository<Contact>, IContactRepository
    {
        public ContactRepository(AppDbContext _context) : base(_context)
        {
        }

        public Task<Contact> GetContactLastAsync()
        {
            return _table.Include(c=>c.ContactSocialMedias).OrderBy(x => x.CreatedAt).AsNoTracking().FirstOrDefaultAsync();
        }

        public Task<List<Contact>> GetContactWithContactSocialMedia()
        {
            return _table.Include(x => x.ContactSocialMedias).OrderByDescending(x=>x.CreatedAt).AsNoTracking().ToListAsync();

        }
    }
}
