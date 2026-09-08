using Bagery.WebUI.Entities;
using Bagery.WebUI.Repositories.GenericRepositories;

namespace Bagery.WebUI.Repositories.BlogRepositories;

public interface IBlogRepository:IGenericRepository<Blog>
{

    Task<List<Blog>> GetAllBlogsWithUserAsync();
    Task<List<Blog>> GetBlogLast4Async();
    Task<List<Blog>> GetBlogByUserIdAsync(Guid UserId);
    Task<Blog> GetBlogByIdWithUser(Guid Id); 

}
