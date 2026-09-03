using Bagery.WebUI.MediatorPattern.Queries.ContactMessageQueries;
using Bagery.WebUI.MediatorPattern.Results.ContactMessageResults;
using Bagery.WebUI.Repositories.ContactMessageRepositories;
using Mapster;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.ContactMessageHandlers
{
    public class GetContactMessageByIdQueryCommand(IContactMessageRepository _contactMessageRepository) : IRequestHandler<GetContactByIdMessagesQuery, GetContactMessageByIdQueryResult>
    {
        public async Task<GetContactMessageByIdQueryResult> Handle(GetContactByIdMessagesQuery request, CancellationToken cancellationToken)
        {
            var results = await _contactMessageRepository.GetByIdAsync(request.Id);
            return results.Adapt<GetContactMessageByIdQueryResult>();
        }
    }
}
