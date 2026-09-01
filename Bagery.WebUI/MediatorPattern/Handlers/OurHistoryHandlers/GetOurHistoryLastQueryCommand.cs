using Bagery.WebUI.MediatorPattern.Queries.OurHistoryQueries;
using Bagery.WebUI.MediatorPattern.Results.OurHistoryResults;
using Bagery.WebUI.Repositories.OurHistoryRepositories;
using Mapster;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.OurHistoryHandlers
{
    public class GetOurHistoryLastQueryCommand(IOurHistoryRepository _ourHistoryRepository) : IRequestHandler<GetOurHistoryLastQuery, GetOurHistoryLastQueryResult>
    {
        public async Task<GetOurHistoryLastQueryResult> Handle(GetOurHistoryLastQuery request, CancellationToken cancellationToken)
        {
            
            var results = await _ourHistoryRepository.GetOurHistoryLastAsync();
            return results.Adapt<GetOurHistoryLastQueryResult>();

        }
    }
}
