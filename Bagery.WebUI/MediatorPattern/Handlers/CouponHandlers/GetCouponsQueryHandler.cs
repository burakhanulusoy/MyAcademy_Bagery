using Bagery.WebUI.Enums;
using Bagery.WebUI.MediatorPattern.Queries.CouponQueries;
using Bagery.WebUI.MediatorPattern.Results.CouponResults;
using Bagery.WebUI.Repositories.CouponRepositories;
using Bagery.WebUI.Repositories.OrderRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bagery.WebUI.MediatorPattern.Handlers.CouponHandlers
{
    public class GetCouponsQueryHandler(ICouponRepository _couponRepository,
                                        IOrderRepository _orderRepository) : IRequestHandler<GetCouponsQuery, List<GetCouponsQueryResult>>
    {
        public async Task<List<GetCouponsQueryResult>> Handle(GetCouponsQuery request, CancellationToken cancellationToken)
        {
            // Her kupon kodu kaç ÖDENMİŞ siparişte kullanıldı: { "HOSGELDIN50": 3, ... }
            var usage = await _orderRepository.GetQueryable()
                .Where(x => x.Status == OrderStatus.Paid && x.CouponCode != null)
                .GroupBy(x => x.CouponCode!)
                .Select(g => new { Code = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Code, x => x.Count, cancellationToken);

            var coupons = await _couponRepository.GetQueryable()
                .OrderByDescending(x => x.CreatedAt) // en yeni kupon üstte
                .ToListAsync(cancellationToken);

            return coupons.Select(x => new GetCouponsQueryResult
            {
                Id = x.Id,
                CouponCode = x.CouponCode,
                CouponPrice = x.CouponPrice,
                MinPrice = x.MinPrice,
                IsActive = x.IsActive,
                UsageCount = usage.GetValueOrDefault(x.CouponCode) // hiç kullanılmadıysa 0
            }).ToList();
        }
    }
}