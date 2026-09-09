// Results/BlogResults/GetBlogForCommentQueryResult.cs
using Bagery.WebUI.MediatorPattern.Results.UserResults;

namespace Bagery.WebUI.MediatorPattern.Results.BlogResults;

public record GetBlogForCommentQueryResult(
    Guid Id,
    string Title,
    string ImageUrl1,
    DateTime CreatedAt,
    GetUserForFobiaTemplateQueryResult AppUser,
    Guid AppUserId);