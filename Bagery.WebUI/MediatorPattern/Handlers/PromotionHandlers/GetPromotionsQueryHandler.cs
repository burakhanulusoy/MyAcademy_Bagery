using Bagery.WebUI.MediatorPattern.Queries.PromotionQueries;
using Bagery.WebUI.MediatorPattern.Results.PromotionResults;
using Bagery.WebUI.Repositories.PromotionRepositories;
using Mapster;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.PromotionHandlers
{
    public class GetPromotionsQueryHandler(IPromotionRepository promotionRepository) : IRequestHandler<GetPromotionsQuery, List<GetPromotionsQueryResult>>
    {

        public async Task<List<GetPromotionsQueryResult>> Handle(GetPromotionsQuery request, CancellationToken cancellationToken)
        {
            var item = await promotionRepository.GetAllAsync();
            return item.Adapt<List<GetPromotionsQueryResult>>();
        }




    }
}
