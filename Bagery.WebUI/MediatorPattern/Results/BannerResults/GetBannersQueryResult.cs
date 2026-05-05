namespace Bagery.WebUI.MediatorPattern.Results.BannerResults;

public record GetBannersQueryResult(Guid Id,
                                   string Title,
                                   string Description,
                                   string ImageUrl,
                                   string BackgroundImageUrl);
