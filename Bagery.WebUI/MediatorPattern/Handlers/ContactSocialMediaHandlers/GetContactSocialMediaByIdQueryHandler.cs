using Bagery.WebUI.MediatorPattern.Queries.ContactSocialMediaQueries;
using Bagery.WebUI.MediatorPattern.Results.ContactSocialMediaResults;
using Bagery.WebUI.Repositories.ContactSocialMediaRepositories;
using Mapster;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.ContactSocialMediaHandlers
{
    public class GetContactSocialMediaByIdQueryHandler(IContactSocialMediaRepository contactSocialMediaRepository) : IRequestHandler<GetContactSocailMediaByIdQuery, GetContactSocialMediaByIdQueryResult>
    {
        public async Task<GetContactSocialMediaByIdQueryResult> Handle(GetContactSocailMediaByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await contactSocialMediaRepository.GetByIdAsync(request.Id);
            return item.Adapt<GetContactSocialMediaByIdQueryResult>();
        }
    }
}
