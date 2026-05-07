using Bagery.WebUI.MediatorPattern.Results.PromotionResults;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Queries.PromotionQueries;

public record GetPromotionByIdQuery(Guid Id):IRequest<GetPromotionByIdQueryResult>;
