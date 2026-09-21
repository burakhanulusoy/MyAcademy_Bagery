using Bagery.WebUI.MediatorPattern.Queries.CouponQueries;
using Bagery.WebUI.MediatorPattern.Results.CouponResults;
using Bagery.WebUI.Repositories.CouponRepositories;
using Mapster;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.CouponHandlers
{
    public class GetCouponByIdQueryHandler(ICouponRepository _couponRepository) : IRequestHandler<GetCouponByIdQuery, GetCouponByIdQueryResult?>
    {
        public async Task<GetCouponByIdQueryResult?> Handle(GetCouponByIdQuery request, CancellationToken cancellationToken)
        {
            var coupon = await _couponRepository.GetByIdAsync(request.Id);
            return coupon?.Adapt<GetCouponByIdQueryResult>();
        }
    }
}