using Bagery.WebUI.MediatorPattern.Results.UserResults;

namespace Bagery.WebUI.MediatorPattern.Results.CommentResults;

public record GetCommentsByBlogIdQueryResult(
    Guid Id,
    string CommentContent,
    DateTime CreatedAt,
    GetUserForCommentQueryResult AppUser,
    Guid AppUserId);