namespace Bagery.WebUI.MediatorPattern.Results.BannerResults;

public record GetBannerByIdQueryResult(Guid Id,
                                   string Title,
                                   string Description,
                                   string ImageUrl,
                                   string BackgroundImageUrl);

