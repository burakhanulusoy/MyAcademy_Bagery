using Bagery.WebUI.Context;
using Bagery.WebUI.Entities;
using Bagery.WebUI.Repositories.GenericRepositories;
using Microsoft.EntityFrameworkCore;

namespace Bagery.WebUI.Repositories.BlogRepositories
{
    public class BlogRepository : GenericRepository<Blog>, IBlogRepository
    {
        public BlogRepository(AppDbContext _context) : base(_context)
        {
        }

        public Task<List<Blog>> GetAllBlogsWithUserAsync()
        {
            return _table.AsNoTracking()
                         .Include(x => x.AppUser)
                         .Include(x => x.Comments)
                             .ThenInclude(c => c.AppUser)
                         .OrderByDescending(x => x.CreatedAt)
                         .ToListAsync();
        }

        public Task<List<Blog>> GetBlogByUserIdAsync(Guid userId)
        {
            return _table.AsNoTracking()
                         .Include(x => x.Comments)
                             .ThenInclude(c => c.AppUser)
                         .Where(x => x.AppUserId == userId)
                         .OrderByDescending(x => x.CreatedAt)
                         .ToListAsync();
        }

        public Task<Blog> GetBlogByIdWithUser(Guid Id)
        {
            return _table.AsNoTracking().Include(x=>x.AppUser).Where(x => x.Id == Id).FirstOrDefaultAsync();
        }

     

        public Task<List<Blog>> GetBlogLast4Async()
        {
           return _table.AsNoTracking().Take(4).ToListAsync();
        }
    }
}
