using Bagery.WebUI.Context;
using Bagery.WebUI.Entities;
using Bagery.WebUI.Repositories.GenericRepositories;
using Microsoft.EntityFrameworkCore;

namespace Bagery.WebUI.Repositories.CommentRepositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(AppDbContext _context) : base(_context)
        {
        }
        public Task<List<Comment>> GetAllCommentWithUserAndBlogAsync()
        {
            return _table.AsNoTracking()
                         .Include(x => x.AppUser)
                         .Include(x => x.Blog)
                             .ThenInclude(b => b.AppUser)
                         .OrderByDescending(x => x.CreatedAt)
                         .ToListAsync();
        }

        public Task<Comment> GetCommentByIdWithUserAndBlogAsync(Guid Id)
        {
            return _table.AsNoTracking()
                         .Include(x => x.AppUser)
                         .Include(x => x.Blog)
                             .ThenInclude(b => b.AppUser)
                         .FirstOrDefaultAsync(x => x.Id == Id);
        }

        public Task<List<Comment>> GetCommentByUserWitBlogAsync(Guid UserId)
        {
            return _table.AsNoTracking()
                         .Include(x => x.Blog)
                             .ThenInclude(b => b.AppUser)
                         .Where(x => x.AppUserId == UserId)
                         .OrderByDescending(x => x.CreatedAt)
                         .ToListAsync();
        }



        public Task<List<Comment>> GetCommentsByBlogIdAsync(Guid blogId)
        {
            return _table.AsNoTracking()
                         .Include(x => x.AppUser)
                         .Where(x => x.BlogId == blogId)
                         .OrderByDescending(x => x.CreatedAt)
                         .ToListAsync();
        }
    }
}
