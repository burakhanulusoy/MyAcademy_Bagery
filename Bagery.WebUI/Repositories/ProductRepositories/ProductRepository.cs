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

        public Task<List<Product>> GetProductsWithCategoryAsync()
        {
            return _table.AsNoTracking().Include(x => x.Category).ToListAsync();
        }
    }
}
