using Bagery.WebUI.Entities;
using Bagery.WebUI.Repositories.GenericRepositories;

namespace Bagery.WebUI.Repositories.ContactSocialMediaRepositories
{
    public interface IContactSocialMediaRepository : IGenericRepository<ContactSocialMedia>
    {
        Task<List<ContactSocialMedia>> GetContactSocialMediaWithContactAsync();

    }
}
