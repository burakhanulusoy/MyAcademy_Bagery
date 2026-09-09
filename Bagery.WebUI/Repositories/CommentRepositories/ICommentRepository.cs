using Bagery.WebUI.Entities;
using Bagery.WebUI.Repositories.GenericRepositories;

namespace Bagery.WebUI.Repositories.CommentRepositories
{
    public interface ICommentRepository:IGenericRepository<Comment>
    {
        Task<List<Comment>> GetAllCommentWithUserAndBlogAsync();
        Task<Comment> GetCommentByIdWithUserAndBlogAsync(Guid Id);
        Task<List<Comment>> GetCommentByUserWitBlogAsync(Guid UserId);
        Task<List<Comment>> GetCommentsByBlogIdAsync(Guid blogId);

    }
}
