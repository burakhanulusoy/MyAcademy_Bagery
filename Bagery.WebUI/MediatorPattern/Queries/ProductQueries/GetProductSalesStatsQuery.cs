using Bagery.WebUI.MediatorPattern.Results.ProductResults;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Queries.ProductQueries;

public record GetProductSalesStatsQuery(Guid ProductId) : IRequest<GetProductSalesStatsQueryResult>;