using Bagery.WebUI.MediatorPattern.Queries.ContactMessageQueries;
using Bagery.WebUI.MediatorPattern.Results.ContactMessageResults;
using Bagery.WebUI.Repositories.ContactMessageRepositories;
using Mapster;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.ContactMessageHandlers
{
    public class GetContactMessageQueryCommand(IContactMessageRepository _contactMessageRepository) : IRequestHandler<GetContactMessagesQuery, List<GetContactMessagesQueryResult>>
    {
        public async Task<List<GetContactMessagesQueryResult>> Handle(GetContactMessagesQuery request, CancellationToken cancellationToken)
        {
            var results = await _contactMessageRepository.GetAllAsync();

            return results.Where(x => x.IsVerified)
                          .OrderByDescending(x => x.CreatedAt)
                          .Adapt<List<GetContactMessagesQueryResult>>();
        }
    }
}
