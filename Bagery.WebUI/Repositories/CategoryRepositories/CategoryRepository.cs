using Bagery.WebUI.Context;
using Bagery.WebUI.Entities;
using Bagery.WebUI.Repositories.GenericRepositories;
using Microsoft.EntityFrameworkCore;

namespace Bagery.WebUI.Repositories.CategoryRepositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext _context) : base(_context)
        {
        }
        //eager loading
        public Task<List<Category>> GetCategoryWithProjectsAsync()
        {
            return _table.AsNoTracking().Include(x=>x.Products).ToListAsync();
        }
    }
}
