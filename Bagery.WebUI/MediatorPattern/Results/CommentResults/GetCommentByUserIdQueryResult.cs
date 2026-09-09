// Results/CommentResults/GetCommentByUserIdQueryResult.cs
using Bagery.WebUI.MediatorPattern.Results.BlogResults;

namespace Bagery.WebUI.MediatorPattern.Results.CommentResults;

public record GetCommentByUserIdQueryResult(
    Guid Id,
    string CommentContent,
    DateTime CreatedAt,
    Guid AppUserId,
    GetBlogForCommentQueryResult Blog,
    Guid BlogId);