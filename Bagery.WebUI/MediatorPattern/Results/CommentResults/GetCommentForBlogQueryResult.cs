// Results/CommentResults/GetCommentForBlogQueryResult.cs
using Bagery.WebUI.MediatorPattern.Results.UserResults;

namespace Bagery.WebUI.MediatorPattern.Results.CommentResults;

public record GetCommentForBlogQueryResult(
    Guid Id,
    string CommentContent,
    DateTime CreatedAt,
    GetUserForCommentQueryResult AppUser,
    Guid AppUserId);