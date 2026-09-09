// Results/CommentResults/GetCommentByIdQueryResult.cs
using Bagery.WebUI.MediatorPattern.Results.BlogResults;
using Bagery.WebUI.MediatorPattern.Results.UserResults;

namespace Bagery.WebUI.MediatorPattern.Results.CommentResults;

public record GetCommentByIdQueryResult(
    Guid Id,
    string CommentContent,
    DateTime CreatedAt,
    GetUserForFobiaTemplateQueryResult AppUser,
    Guid AppUserId,
    GetBlogForCommentQueryResult Blog,
    Guid BlogId);