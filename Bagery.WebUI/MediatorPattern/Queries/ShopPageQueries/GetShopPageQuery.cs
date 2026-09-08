using Bagery.WebUI.MediatorPattern.Results.ShopPageResults;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Queries.ShopPageQueries;

public record GetShopPageQuery(
    Guid? CategoryId, string? Search, decimal? Min, decimal? Max, int Page
) : IRequest<ShopPageResult>;
