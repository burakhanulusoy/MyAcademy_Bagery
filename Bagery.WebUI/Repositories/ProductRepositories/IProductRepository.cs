using Bagery.WebUI.Entities;
using Bagery.WebUI.Repositories.GenericRepositories;

namespace Bagery.WebUI.Repositories.ProductRepositories
{
    public interface IProductRepository: IGenericRepository<Product>
    {
        Task<List<Product>> GetProductsWithCategoryAsync();
        Task<Product> GetProductByIdWithProductVariants(Guid id);

        Task<List<Product>> GetProductLast10WithCategory();

    }
}
