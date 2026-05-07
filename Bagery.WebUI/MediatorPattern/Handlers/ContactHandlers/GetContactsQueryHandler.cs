using Bagery.WebUI.MediatorPattern.Queries.ContactQueries;
using Bagery.WebUI.MediatorPattern.Results.ContactResults;
using Bagery.WebUI.Repositories.ContactRepositories;
using Mapster;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.ContactHandlers
{
    public class GetContactsQueryHandler(IContactRepository contactRepository) : IRequestHandler<GetContactsQuery, List<GetContactsWithSocialMediaQueryResult>>
    {
        public async Task<List<GetContactsWithSocialMediaQueryResult>> Handle(GetContactsQuery request, CancellationToken cancellationToken)
        {
            var item = await contactRepository.GetContactWithContactSocialMedia();
            return item.Adapt<List<GetContactsWithSocialMediaQueryResult>>();
            
        }
    }
}
