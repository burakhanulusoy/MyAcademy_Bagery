using Bagery.WebUI.Entities;
using Bagery.WebUI.Repositories.GenericRepositories;

namespace Bagery.WebUI.Repositories.ContactRepositories
{
    public interface IContactRepository : IGenericRepository<Contact>
    {
        Task<List<Contact>> GetContactWithContactSocialMedia();



    }
}
