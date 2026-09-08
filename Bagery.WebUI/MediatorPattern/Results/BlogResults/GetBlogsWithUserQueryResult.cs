using Bagery.WebUI.MediatorPattern.Results.UserResults;

namespace Bagery.WebUI.MediatorPattern.Results.BlogResults;

public record GetBlogsWithUserQueryResult(
    Guid Id,
    string Title,
    string ShortDescription,
    string MainDescription,
    string BackgroundImageUrl,
    string LongTitle,
    string LongDescription,
    string ImageUrl1,
    string LastDescription,
    string LastDescriptionTitle,
    DateTime CreatedAt,
    GetUserForFobiaTemplateQueryResult AppUser,
    Guid AppUserId
);