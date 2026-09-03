using Bagery.WebUI.MediatorPattern.Queries.ContactQueries;
using Bagery.WebUI.MediatorPattern.Results.ContactResults;
using Bagery.WebUI.Repositories.ContactRepositories;
using Mapster;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.ContactHandlers
{
    public class GetContactLastQueryHandler(IContactRepository repository) : IRequestHandler<GetContactLastQuery, GetContactLastQueryResult>
    {
        public async Task<GetContactLastQueryResult> Handle(GetContactLastQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetContactLastAsync();
            return result.Adapt<GetContactLastQueryResult>();


        }
    }
}
