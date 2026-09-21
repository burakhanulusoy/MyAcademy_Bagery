using Bagery.WebUI.MediatorPattern.Queries.CouponQueries;
using Bagery.WebUI.MediatorPattern.Results.CouponResults;
using Bagery.WebUI.Repositories.CouponRepositories;
using Mapster;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.CouponHandlers
{
    public class GetActiveCouponsQueryHandler(ICouponRepository _couponRepository) : IRequestHandler<GetActiveCouponsQuery, List<GetActiveCouponsQueryResult>>
    {
        public async Task<List<GetActiveCouponsQueryResult>> Handle(GetActiveCouponsQuery request, CancellationToken cancellationToken)
        {
            var coupons = await _couponRepository.GetActiveCouponsAsync();
            return coupons.Adapt<List<GetActiveCouponsQueryResult>>();
        }
    }
}