using Bagery.WebUI.Enums;
using Bagery.WebUI.MediatorPattern.Queries.OrderQueries;
using Bagery.WebUI.MediatorPattern.Results.OrderResults;
using Bagery.WebUI.Repositories.OrderRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bagery.WebUI.MediatorPattern.Handlers.OrderHandlers
{
    public class GetAdminOrdersQueryHandler(IOrderRepository _orderRepository) : IRequestHandler<GetAdminOrdersQuery, GetAdminOrdersQueryResult>
    {
        public async Task<GetAdminOrdersQueryResult> Handle(GetAdminOrdersQuery request, CancellationToken cancellationToken)
        {
            // 1) Özet kartları: her durumdan kaç sipariş var, toplam tutarları ne
            //    GroupBy veritabanında çalışır; siparişlerin hepsi belleğe çekilmez
            var summary = await _orderRepository.GetQueryable()
                                                .GroupBy(x => x.Status)
                                                .Select(g => new { Status = g.Key, Count = g.Count(), Total = g.Sum(x => x.PaidPrice) })
                                                .ToListAsync(cancellationToken);

            // 2) Tablo: filtre seçildiyse sadece o durum
            var query = _orderRepository.GetQueryable();
            if (request.Status is OrderStatus status)
                query = query.Where(x => x.Status == status);

            var orders = await query
                .OrderByDescending(x => x.CreatedAt) // en yeni sipariş en üstte
                .Select(x => new GetAdminOrderListItemResult
                {
                    OrderNo = x.OrderNo,
                    CreatedAt = x.CreatedAt,
                    FullName = x.FullName,
                    Email = x.Email,
                    ItemCount = x.OrderItems.Sum(i => i.Quantity), // Include gerekmiyor (açıklaması aşağıda)
                    PaidPrice = x.PaidPrice,
                    InstallmentCount = x.InstallmentCount,
                    Status = x.Status
                })
                .ToListAsync(cancellationToken);

            return new GetAdminOrdersQueryResult
            {
                SelectedStatus = request.Status,
                PaidTotal = summary.Where(x => x.Status == OrderStatus.Paid).Sum(x => x.Total),
                PaidCount = summary.Where(x => x.Status == OrderStatus.Paid).Sum(x => x.Count),
                PendingCount = summary.Where(x => x.Status == OrderStatus.Pending).Sum(x => x.Count),
                FailedCount = summary.Where(x => x.Status == OrderStatus.Failed).Sum(x => x.Count),
                Orders = orders
            };
        }
    }
}