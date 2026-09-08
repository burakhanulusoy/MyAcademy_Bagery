using Bagery.WebUI.MediatorPattern.Results.UserResults;

namespace Bagery.WebUI.MediatorPattern.Results.CommentResults;

public record GetCommentByBlogQueryResult(Guid Id,GetUsersQueryResult AppUser,Guid AppUserId)
{
}
