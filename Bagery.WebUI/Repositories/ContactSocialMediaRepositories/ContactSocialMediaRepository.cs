using Bagery.WebUI.Context;
using Bagery.WebUI.Entities;
using Bagery.WebUI.Repositories.GenericRepositories;

namespace Bagery.WebUI.Repositories.ContactSocialMediaRepositories
{
    public class ContactSocialMediaRepository : GenericRepository<ContactSocialMedia>, IContactSocialMediaRepository
    {
        public ContactSocialMediaRepository(AppDbContext _context) : base(_context)
        {
        }
    }
}
