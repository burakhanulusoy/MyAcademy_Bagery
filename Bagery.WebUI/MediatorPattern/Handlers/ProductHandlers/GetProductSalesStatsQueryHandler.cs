using Bagery.WebUI.Context;
using Bagery.WebUI.Enums;
using Bagery.WebUI.MediatorPattern.Queries.ProductQueries;
using Bagery.WebUI.MediatorPattern.Results.ProductResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Bagery.WebUI.MediatorPattern.Handlers.ProductHandlers
{
    public class GetProductSalesStatsQueryHandler(AppDbContext _context) : IRequestHandler<GetProductSalesStatsQuery, GetProductSalesStatsQueryResult>
    {
        private static readonly CultureInfo Tr = CultureInfo.GetCultureInfo("tr-TR");

        public async Task<GetProductSalesStatsQueryResult> Handle(GetProductSalesStatsQuery request, CancellationToken cancellationToken)
        {
            // Sadece ödemesi onaylanan siparişlerdeki bu ürünün kalemleri
            var rows = await _context.Orders.AsNoTracking()
                .Where(o => o.Status == OrderStatus.Paid)
                .SelectMany(o => o.OrderItems.Where(i => i.ProductId == request.ProductId)
                    .Select(i => new { o.CreatedAt, i.Quantity, i.UnitPrice }))
                .ToListAsync(cancellationToken);

            // Son 6 ay (Türkiye saatine göre)
            var monthStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0, DateTimeKind.Local).AddMonths(-5);

            var months = Enumerable.Range(0, 6)
                .Select(i => monthStart.AddMonths(i))
                .Select(m => new ProductMonthSale(
                    m.ToString("MMM", Tr),
                    rows.Where(r => { var local = r.CreatedAt.ToLocalTime(); return local.Year == m.Year && local.Month == m.Month; })
                        .Sum(r => r.Quantity)))
                .ToList();

            return new GetProductSalesStatsQueryResult
            {
                SoldQuantity = rows.Sum(r => r.Quantity),
                Revenue = rows.Sum(r => r.UnitPrice * r.Quantity),
                OrderCount = rows.Count,
                LastSoldAt = rows.Count == 0 ? null : rows.Max(r => r.CreatedAt),
                Months = months
            };
        }
    }
}