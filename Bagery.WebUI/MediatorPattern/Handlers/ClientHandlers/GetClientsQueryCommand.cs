using Bagery.WebUI.MediatorPattern.Queries.ClientQueries;
using Bagery.WebUI.MediatorPattern.Results.ClientResults;
using Bagery.WebUI.Repositories.ClientRepositories;
using Mapster;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.ClientHandlers
{
    public class GetClientsQueryCommand(IClientRepository _clientRepository) : IRequestHandler<GetClientsQuery, List<GetClientsQueryResult>>
    {
        public async Task<List<GetClientsQueryResult>> Handle(GetClientsQuery request, CancellationToken cancellationToken)
        {
            var result = await _clientRepository.GetAllAsync();
            return result.Adapt<List<GetClientsQueryResult>>();
        }
    }
}
