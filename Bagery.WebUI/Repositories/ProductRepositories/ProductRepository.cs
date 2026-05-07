using Bagery.WebUI.Context;
using Bagery.WebUI.Entities;
using Bagery.WebUI.Repositories.GenericRepositories;
using Microsoft.EntityFrameworkCore;

namespace Bagery.WebUI.Repositories.ProductRepositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext _context) : base(_context)
        {
        }

        public  Task<Product> GetProductByIdWithProductVariants(Guid id)
        {
            return _table.AsNoTracking().Include(x=>x.ProductVariants).Include(x=>x.Category).FirstOrDefaultAsync(x=>x.Id== id);
        }

        public Task<List<Product>> GetProductLast10WithCategory()
        {
            return _table.AsNoTracking().Include(x => x.Category).OrderByDescending(x=>x.CreatedAt).Take(10).ToListAsync();
        }

        public Task<List<Product>> GetProductsWithCategoryAsync()
        {
            return _table.AsNoTracking().Include(x => x.Category).ToListAsync();
        }
    }
}
