using Bagery.WebUI.Context;
using Bagery.WebUI.Entities;
using Bagery.WebUI.Repositories.GenericRepositories;
using Microsoft.EntityFrameworkCore;

namespace Bagery.WebUI.Repositories.ContactSocialMediaRepositories
{
    public class ContactSocialMediaRepository : GenericRepository<ContactSocialMedia>, IContactSocialMediaRepository
    {
        public ContactSocialMediaRepository(AppDbContext _context) : base(_context)
        {
        }

        public Task<List<ContactSocialMedia>> GetContactSocialMediaWithContactAsync()
        {
            return _table.AsNoTracking().Include(x=>x.Contact).ToListAsync();
        }
    }
}
