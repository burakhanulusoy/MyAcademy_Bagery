using Bagery.WebUI.MediatorPattern.Queries.OurHistoryQueries;
using Bagery.WebUI.MediatorPattern.Results.OurHistoryResults;
using Bagery.WebUI.Repositories.OurHistoryRepositories;
using Mapster;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.OurHistoryHandlers
{
    public class GetOurHistoriesQueryHandler(IOurHistoryRepository ourHistoryRepository) : IRequestHandler<GetOurHistoriesQuery, List<GetOurHistoriesQueryResult>>
    {
        public async Task<List<GetOurHistoriesQueryResult>> Handle(GetOurHistoriesQuery request, CancellationToken cancellationToken)
        {
            var item = await ourHistoryRepository.GetAllAsync();
            return item.Adapt<List<GetOurHistoriesQueryResult>>();

        }
    }
}
