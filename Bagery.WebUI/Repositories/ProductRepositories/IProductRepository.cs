using Bagery.WebUI.Entities;
using Bagery.WebUI.Repositories.GenericRepositories;

namespace Bagery.WebUI.Repositories.ProductRepositories
{
    public interface IProductRepository: IGenericRepository<Product>
    {
        Task<List<Product>> GetProductsWithCategoryAsync();


    }
}
