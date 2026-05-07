using Bagery.WebUI.MediatorPattern.Queries.ContactQueries;
using Bagery.WebUI.MediatorPattern.Results.ContactResults;
using Bagery.WebUI.Repositories.ContactRepositories;
using Mapster;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.ContactHandlers
{
    public class GetContactByIdQueryHandler(IContactRepository _contactRepository) : IRequestHandler<GetContactByIdQuery, GetContactByIdQueryResult>
    {
        public async Task<GetContactByIdQueryResult> Handle(GetContactByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await _contactRepository.GetByIdAsync(request.Id);
            return item.Adapt<GetContactByIdQueryResult>();
        }
    }
}
