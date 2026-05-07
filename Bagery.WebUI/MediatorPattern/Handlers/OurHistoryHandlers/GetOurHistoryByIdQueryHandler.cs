using Bagery.WebUI.MediatorPattern.Queries.OurHistoryQueries;
using Bagery.WebUI.MediatorPattern.Results.OurHistoryResults;
using Bagery.WebUI.Repositories.OurHistoryRepositories;
using Mapster;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.OurHistoryHandlers
{
    public class GetOurHistoryByIdQueryHandler(IOurHistoryRepository ourHistoryRepository) : IRequestHandler<GetOurHistoryByIdQuery, GetOurHistoryByIdQueryResult>
    {
        public async Task<GetOurHistoryByIdQueryResult> Handle(GetOurHistoryByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await ourHistoryRepository.GetByIdAsync(request.Id);
            return item.Adapt<GetOurHistoryByIdQueryResult>();
        }




    }
}
