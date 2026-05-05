using Bagery.WebUI.MediatorPattern.Queries.BannerQueries;
using Bagery.WebUI.MediatorPattern.Results.BannerResults;
using Bagery.WebUI.Repositories.BannerRepositories;
using Bagery.WebUI.UOW;
using Mapster;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.BannerHandlers
{
    public class GetBannersQueryHandler(IBannerRepository _bannerRepository,IUnitOfWork _unitOfWork) : IRequestHandler<GetBannersQuery, List<GetBannersQueryResult>>
    {
        public async Task<List<GetBannersQueryResult>> Handle(GetBannersQuery request, CancellationToken cancellationToken)
        {

            var banners = await _bannerRepository.GetAllAsync();
            return banners.Adapt<List<GetBannersQueryResult>>();
        




        }
    }
}
