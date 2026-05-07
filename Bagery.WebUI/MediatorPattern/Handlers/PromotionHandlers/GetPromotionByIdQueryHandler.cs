using Bagery.WebUI.MediatorPattern.Queries.PromotionQueries;
using Bagery.WebUI.MediatorPattern.Results.PromotionResults;
using Bagery.WebUI.Repositories.PromotionRepositories;
using Mapster;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.PromotionHandlers
{
    public class GetPromotionByIdQueryHandler(IPromotionRepository promotionRepository) : IRequestHandler<GetPromotionByIdQuery, GetPromotionByIdQueryResult>
    {
        public async Task<GetPromotionByIdQueryResult> Handle(GetPromotionByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await promotionRepository.GetByIdAsync(request.Id);
            return item.Adapt<GetPromotionByIdQueryResult>();

        }
    }
}
