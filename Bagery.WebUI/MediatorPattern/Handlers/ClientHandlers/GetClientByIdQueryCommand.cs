using Bagery.WebUI.MediatorPattern.Queries.ClientQueries;
using Bagery.WebUI.MediatorPattern.Results.ClientResults;
using Bagery.WebUI.Repositories.ClientRepositories;
using Mapster;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.ClientHandlers
{
    public class GetClientByIdQueryCommand(IClientRepository _clientRepository) : IRequestHandler<GetClientByIdQuery, GetClientByIdQueryResult>
    {
        public async Task<GetClientByIdQueryResult> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _clientRepository.GetByIdAsync(request.Id);
            return result.Adapt<GetClientByIdQueryResult>();
        }
    }
}
