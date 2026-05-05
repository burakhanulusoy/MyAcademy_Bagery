using Bagery.WebUI.MediatorPattern.Queries.BannerQueries;
using Bagery.WebUI.MediatorPattern.Results.BannerResults;
using Bagery.WebUI.Repositories.BannerRepositories;
using Mapster;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.BannerHandlers
{
    public class GetBannerByIdQueryHandler(IBannerRepository _bannerRepository) : IRequestHandler<GetBannerByIdQuery, GetBannerByIdQueryResult>
    {
        public async Task<GetBannerByIdQueryResult> Handle(GetBannerByIdQuery request, CancellationToken cancellationToken)
        {

            var banner = await _bannerRepository.GetByIdAsync(request.Id);
            return banner.Adapt<GetBannerByIdQueryResult>();


        }
    }
}
