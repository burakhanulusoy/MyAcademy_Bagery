using Bagery.WebUI.MediatorPattern.Results.PromotionResults;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Queries.PromotionQueries;

public record GetPromotionsQuery:IRequest<List<GetPromotionsQueryResult>>;
