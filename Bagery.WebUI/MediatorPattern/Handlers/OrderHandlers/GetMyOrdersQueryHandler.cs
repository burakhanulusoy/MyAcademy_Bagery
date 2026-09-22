using Bagery.WebUI.Entities;
using Bagery.WebUI.Enums;
using Bagery.WebUI.MediatorPattern.Queries.OrderQueries;
using Bagery.WebUI.MediatorPattern.Results.OrderResults;
using Bagery.WebUI.Repositories.OrderRepositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bagery.WebUI.MediatorPattern.Handlers.OrderHandlers
{
    public class GetMyOrdersQueryHandler(IOrderRepository _orderRepository,
                                         UserManager<AppUser> _userManager,
                                         IHttpContextAccessor _httpContextAccessor) : IRequestHandler<GetMyOrdersQuery, List<GetMyOrdersQueryResult>>
    {
        public async Task<List<GetMyOrdersQueryResult>> Handle(GetMyOrdersQuery request, CancellationToken cancellationToken)
        {
            var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext!.User);
            if (!Guid.TryParse(userId, out var appUserId))
                return [];

            return await _orderRepository.GetQueryable()
                // Sadece kendi siparişleri. Başarısız ödeme denemeleri sipariş sayılmaz, listede görünmez.
                .Where(x => x.AppUserId == appUserId && x.Status != OrderStatus.Failed)
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new GetMyOrdersQueryResult
                {
                    OrderNo = x.OrderNo,
                    CreatedAt = x.CreatedAt,
                    Status = x.Status,
                    PaidPrice = x.PaidPrice,
                    FirstProductName = x.OrderItems.Select(i => i.ProductName).FirstOrDefault() ?? "",
                    ProductLineCount = x.OrderItems.Count,
                    DeliveryStatus=x.DeliveryStatus,
                    DispatchedAt = x.DispatchedAt, // YENİ
                    DeliveredAt = x.DeliveredAt,   // YENİ
                })
                .ToListAsync(cancellationToken);
        }
    }
}