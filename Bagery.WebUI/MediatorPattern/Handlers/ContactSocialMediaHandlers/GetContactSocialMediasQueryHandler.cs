using Bagery.WebUI.MediatorPattern.Queries.ContactSocialMediaQueries;
using Bagery.WebUI.MediatorPattern.Results.ContactSocialMediaResults;
using Bagery.WebUI.Repositories.ContactSocialMediaRepositories;
using Mapster;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.ContactSocialMediaHandlers
{
    public class GetContactSocialMediasQueryHandler(IContactSocialMediaRepository contactSocialMediaRepository) : IRequestHandler<GetContactSocailMediasQuery, List<GetContacSocialMediasQueryResult>>
    {
        public async Task<List<GetContacSocialMediasQueryResult>> Handle(GetContactSocailMediasQuery request, CancellationToken cancellationToken)
        {

            var item = await contactSocialMediaRepository.GetContactSocialMediaWithContactAsync();
            return item.Adapt<List<GetContacSocialMediasQueryResult>>();

        }
    }
}
