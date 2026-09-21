using Bagery.WebUI.Context;
using Bagery.WebUI.Enums;
using Bagery.WebUI.MediatorPattern.Queries.DashboardQueries;
using Bagery.WebUI.MediatorPattern.Results.DashboardResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Bagery.WebUI.MediatorPattern.Handlers.DashboardHandlers
{
    public class GetSalesDashboardQueryHandler(AppDbContext _context) : IRequestHandler<GetSalesDashboardQuery, GetSalesDashboardQueryResult>
    {
        private static readonly CultureInfo Tr = CultureInfo.GetCultureInfo("tr-TR");
        private static readonly int[] AllowedDays = [7, 30, 90, 365];

        // Veritabanından sadece gereken kolonlar
        private record OrderRow(string OrderNo, string FullName, OrderStatus Status, DateTime CreatedAt,
                                decimal PaidPrice, decimal TotalPrice, decimal ShippingPrice, decimal CouponPrice,
                                string? CouponCode, int InstallmentCount, string? CardBrand, List<ItemRow> Items);
        private record ItemRow(Guid ProductId, string ProductName, int Quantity, decimal UnitPrice);

        public async Task<GetSalesDashboardQueryResult> Handle(GetSalesDashboardQuery request, CancellationToken cancellationToken)
        {
            var days = AllowedDays.Contains(request.Days) ? request.Days : 30;

            // ---- 1) Dönem: bugün dahil son N gün + kıyas için ondan önceki N gün ----
            var fromLocal = DateTime.Now.Date.AddDays(-(days - 1));
            var toLocal = DateTime.Now.Date.AddDays(1);
            var fromUtc = fromLocal.ToUniversalTime();              // PostgreSQL tarihleri UTC ister
            var toUtc = toLocal.ToUniversalTime();
            var prevFromUtc = fromLocal.AddDays(-days).ToUniversalTime();

            // ---- 2) İki dönemin siparişleri tek sorguda ----
            var orders = await _context.Orders.AsNoTracking()
                .Where(o => o.CreatedAt >= prevFromUtc && o.CreatedAt < toUtc)
                .Select(o => new OrderRow(o.OrderNo, o.FullName, o.Status, o.CreatedAt,
                    o.PaidPrice, o.TotalPrice, o.ShippingPrice, o.CouponPrice, o.CouponCode, o.InstallmentCount, o.CardBrand,
                    o.OrderItems.Select(i => new ItemRow(i.ProductId, i.ProductName, i.Quantity, i.UnitPrice)).ToList()))
                .ToListAsync(cancellationToken);

            var current = orders.Where(o => o.CreatedAt >= fromUtc).ToList();
            var previous = orders.Where(o => o.CreatedAt < fromUtc).ToList();
            var paid = current.Where(o => o.Status == OrderStatus.Paid).ToList();
            var paidPrev = previous.Where(o => o.Status == OrderStatus.Paid).ToList();
            var failed = current.Count(o => o.Status == OrderStatus.Failed);

            // ---- 3) Para ----
            var revenue = paid.Sum(o => o.PaidPrice);
            var revenuePrev = paidPrev.Sum(o => o.PaidPrice);
            var basket = paid.Count == 0 ? 0 : Math.Round(revenue / paid.Count, 2);
            var basketPrev = paidPrev.Count == 0 ? 0 : Math.Round(revenuePrev / paidPrev.Count, 2);

            // ---- 4) Ürünler: satış kalemleri + katalog (hiç satılmayanları da görmek için) ----
            var items = paid.SelectMany(o => o.Items).ToList();

            var products = await _context.Products.AsNoTracking()
                .Select(p => new { p.Id, p.ProductName, p.MainImageUrl, CategoryName = p.Category.CategoryName })
                .ToListAsync(cancellationToken);
            var productMap = products.ToDictionary(p => p.Id);

            var sales = items.GroupBy(i => i.ProductId).ToDictionary(
                g => g.Key,
                g => (Quantity: g.Sum(i => i.Quantity), Revenue: g.Sum(i => i.UnitPrice * i.Quantity), Name: g.First().ProductName));
            var itemRevenueTotal = sales.Values.Sum(s => s.Revenue);

            // Satış satırını listeye çevirir; ürün silinmişse sipariş anındaki adı kullanır
            ProductSale ToSale(Guid id, string fallbackName, int quantity, decimal revenueOfProduct)
            {
                productMap.TryGetValue(id, out var product);
                var share = itemRevenueTotal == 0 ? 0 : Math.Round(revenueOfProduct * 100 / itemRevenueTotal, 1);
                return new ProductSale(id, product?.ProductName ?? fallbackName, product?.CategoryName ?? "Silinmiş ürün",
                                       product?.MainImageUrl, quantity, revenueOfProduct, share);
            }

            var topProducts = sales
                .OrderByDescending(s => s.Value.Quantity).ThenByDescending(s => s.Value.Revenue)
                .Take(10)
                .Select(s => ToSale(s.Key, s.Value.Name, s.Value.Quantity, s.Value.Revenue))
                .ToList();

            // En az satanlar: katalogdaki TÜM ürünler üzerinden -> hiç satılmayanlar en üstte
            var leastProducts = products
                .Select(p => sales.TryGetValue(p.Id, out var s)
                    ? ToSale(p.Id, p.ProductName, s.Quantity, s.Revenue)
                    : ToSale(p.Id, p.ProductName, 0, 0))
                .OrderBy(s => s.Quantity).ThenBy(s => s.Revenue).ThenBy(s => s.Name)
                .Take(10)
                .ToList();

            var categoryRevenue = sales
                .GroupBy(s => productMap.TryGetValue(s.Key, out var p) ? p.CategoryName : "Silinmiş ürünler")
                .Select(g => new ChartItem(g.Key, g.Sum(s => s.Value.Revenue)))
                .OrderByDescending(c => c.Value)
                .ToList();

            // ---- 5) Günlük seri: satış olmayan günler 0 olarak yer alsın (grafik boşluksuz) ----
            var byDay = paid.GroupBy(o => o.CreatedAt.ToLocalTime().Date)
                            .ToDictionary(g => g.Key, g => (Revenue: g.Sum(o => o.PaidPrice), Count: g.Count()));

            var daily = Enumerable.Range(0, days).Select(i =>
            {
                var day = fromLocal.AddDays(i);
                return byDay.TryGetValue(day, out var d)
                    ? new DailyPoint(day.ToString("dd MMM", Tr), d.Revenue, d.Count)
                    : new DailyPoint(day.ToString("dd MMM", Tr), 0, 0);
            }).ToList();

            // ---- 6) Saatlere göre (Türkiye saati) ----
            var hourly = new int[24];
            foreach (var order in paid)
                hourly[order.CreatedAt.ToLocalTime().Hour]++;

            // ---- 7) Ödeme şekli, kart aileleri, kuponlar ----
            var paymentTypes = new List<ChartItem>
            {
                new("Tek çekim", paid.Count(o => o.InstallmentCount == 0)),
                new("Taksitli", paid.Count(o => o.InstallmentCount > 0))
            };

            var cardBrands = paid.Where(o => o.InstallmentCount > 0)
                .GroupBy(o => string.IsNullOrWhiteSpace(o.CardBrand) ? "bilinmiyor" : o.CardBrand!)
                .Select(g => new ChartItem(g.Key, g.Count()))
                .OrderByDescending(c => c.Value)
                .ToList();

            var coupons = paid.Where(o => o.CouponCode != null)
                .GroupBy(o => o.CouponCode!)
                .Select(g => new CouponUsage(g.Key, g.Count(), g.Sum(o => o.CouponPrice)))
                .OrderByDescending(c => c.Uses)
                .ToList();

            var recentOrders = current.OrderByDescending(o => o.CreatedAt).Take(8)
                .Select(o => new RecentOrder(o.OrderNo, o.FullName, o.CreatedAt, o.PaidPrice, o.Status))
                .ToList();

            return new GetSalesDashboardQueryResult
            {
                Days = days,
                From = fromLocal,
                To = toLocal.AddDays(-1),

                Revenue = revenue,
                RevenueChange = Change(revenue, revenuePrev),
                ProductSales = paid.Sum(o => o.TotalPrice),
                ShippingIncome = paid.Sum(o => o.ShippingPrice),
                InstallmentIncome = paid.Sum(o => o.PaidPrice - (o.TotalPrice + o.ShippingPrice - o.CouponPrice)),
                CouponDiscount = paid.Sum(o => o.CouponPrice),
                AverageBasket = basket,
                AverageBasketChange = Change(basket, basketPrev),

                PaidOrders = paid.Count,
                PaidOrdersChange = Change(paid.Count, paidPrev.Count),
                PendingOrders = current.Count(o => o.Status == OrderStatus.Pending),
                FailedOrders = failed,
                ItemsSold = items.Sum(i => i.Quantity),
                SuccessRate = paid.Count + failed == 0 ? 0 : Math.Round(paid.Count * 100m / (paid.Count + failed), 1),

                Daily = daily,
                Hourly = hourly,
                CategoryRevenue = categoryRevenue,
                PaymentTypes = paymentTypes,
                CardBrands = cardBrands,

                TopProducts = topProducts,
                LeastProducts = leastProducts,
                Coupons = coupons,
                RecentOrders = recentOrders
            };
        }

        // Önceki döneme göre yüzde değişim (önceki dönem 0 ise: şimdi varsa +100, yoksa 0)
        private static decimal Change(decimal now, decimal before) =>
            before == 0 ? (now > 0 ? 100 : 0) : Math.Round((now - before) / before * 100, 1);
    }
}