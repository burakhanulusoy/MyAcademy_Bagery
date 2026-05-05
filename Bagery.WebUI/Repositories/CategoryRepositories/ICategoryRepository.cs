using Bagery.WebUI.Entities;
using Bagery.WebUI.Repositories.GenericRepositories;

namespace Bagery.WebUI.Repositories.CategoryRepositories
{
    public interface ICategoryRepository:IGenericRepository<Category>
    {
        Task<List<Category>> GetCategoryWithProjectsAsync();

    }
}
