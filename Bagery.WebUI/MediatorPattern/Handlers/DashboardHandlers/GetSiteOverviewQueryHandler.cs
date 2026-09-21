using Bagery.WebUI.Context;
using Bagery.WebUI.Enums;
using Bagery.WebUI.MediatorPattern.Queries.DashboardQueries;
using Bagery.WebUI.MediatorPattern.Results.DashboardResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Bagery.WebUI.MediatorPattern.Handlers.DashboardHandlers
{
    public class GetSiteOverviewQueryHandler(AppDbContext _context) : IRequestHandler<GetSiteOverviewQuery, GetSiteOverviewQueryResult>
    {
        private static readonly CultureInfo Tr = CultureInfo.GetCultureInfo("tr-TR");

        public async Task<GetSiteOverviewQueryResult> Handle(GetSiteOverviewQuery request, CancellationToken cancellationToken)
        {
            var ct = cancellationToken;

            // ---- 1) Katalog: ürünler + seçenek sayıları tek sorguda ----
            var products = await _context.Products.AsNoTracking()
                .Select(p => new
                {
                    p.Id,
                    p.ProductName,
                    p.Price,
                    p.MainImageUrl,
                    p.CategoryId,
                    CategoryName = p.Category.CategoryName,
                    Variants = p.ProductVariants.Count(),
                    AvailableVariants = p.ProductVariants.Count(v => v.IsAvailable)
                })
                .ToListAsync(ct);

            var categories = await _context.Categories.AsNoTracking()
                .Select(c => new { c.Id, c.CategoryName })
                .ToListAsync(ct);

            // Kategori tablosu (ürünü olmayan kategoriler de görünsün)
            var categoryStats = categories.Select(c =>
            {
                var list = products.Where(p => p.CategoryId == c.Id).ToList();
                return list.Count == 0
                    ? new CategoryStat(c.CategoryName, 0, 0, 0, 0, 0)
                    : new CategoryStat(c.CategoryName, list.Count, list.Sum(p => p.Variants),
                                       list.Min(p => p.Price), Math.Round(list.Average(p => p.Price), 2), list.Max(p => p.Price));
            })
            .OrderByDescending(c => c.ProductCount)
            .ToList();

            // Fiyat aralıkları
            (string Label, decimal Min, decimal Max)[] buckets =
            [
                ("0–100 ₺", 0, 100), ("100–250 ₺", 100, 250), ("250–500 ₺", 250, 500),
                ("500–1.000 ₺", 500, 1000), ("1.000 ₺ üzeri", 1000, decimal.MaxValue)
            ];
            var priceBuckets = buckets
                .Select(b => new ChartItem(b.Label, products.Count(p => p.Price >= b.Min && p.Price < b.Max)))
                .ToList();

            var mostExpensive = products.OrderByDescending(p => p.Price).Take(5)
                .Select(p => new PricedProduct(p.Id, p.ProductName, p.CategoryName, p.MainImageUrl, p.Price)).ToList();
            var cheapest = products.OrderBy(p => p.Price).Take(5)
                .Select(p => new PricedProduct(p.Id, p.ProductName, p.CategoryName, p.MainImageUrl, p.Price)).ToList();

            // ---- 2) Kullanıcılar ve roller ----
            var users = await _context.Users.AsNoTracking()
                .Select(u => new { u.IsDeleted, u.EmailConfirmed })
                .ToListAsync(ct);

            var roles = await (from userRole in _context.UserRoles
                               join role in _context.Roles on userRole.RoleId equals role.Id
                               group userRole by role.Name into g
                               select new { Name = g.Key, Count = g.Count() })
                              .ToListAsync(ct);

            // ---- 3) Mesajlar (tamamı küçük tablo; durum + son mesajlar bellekte) ----
            var messages = await _context.ContactMessages.AsNoTracking()
                .Select(m => new { m.NameSurname, m.Subject, m.MessageStatus, m.CreatedAt })
                .ToListAsync(ct);

            // ---- 4) Siparişler (tüm zamanlar, sadece gereken kolonlar) ----
            var orders = await _context.Orders.AsNoTracking()
                .Select(o => new { o.Status, o.PaidPrice, o.CreatedAt })
                .ToListAsync(ct);

            // ---- 5) Son 6 ayın hareketi (Türkiye saatine göre ay) ----
            var monthStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0, DateTimeKind.Local).AddMonths(-5);
            var sinceUtc = monthStart.ToUniversalTime();

            var blogDates = await _context.Blogs.Where(b => b.CreatedAt >= sinceUtc).Select(b => b.CreatedAt).ToListAsync(ct);
            var commentDates = await _context.Comments.Where(c => c.CreatedAt >= sinceUtc).Select(c => c.CreatedAt).ToListAsync(ct);
            var messageDates = messages.Where(m => m.CreatedAt >= sinceUtc).Select(m => m.CreatedAt).ToList();
            var orderDates = orders.Where(o => o.Status == OrderStatus.Paid && o.CreatedAt >= sinceUtc).Select(o => o.CreatedAt).ToList();

            static int InMonth(IEnumerable<DateTime> dates, DateTime month) =>
                dates.Count(d => { var local = d.ToLocalTime(); return local.Year == month.Year && local.Month == month.Month; });

            var activity = Enumerable.Range(0, 6)
                .Select(i => monthStart.AddMonths(i))
                .Select(m => new MonthPoint(m.ToString("MMM yyyy", Tr),
                    InMonth(blogDates, m), InMonth(commentDates, m), InMonth(messageDates, m), InMonth(orderDates, m)))
                .ToList();

            // ---- 6) En çok yorum alan bloglar ----
            var topBlogs = await _context.Blogs.AsNoTracking()
                .Select(b => new { b.Title, Count = b.Comments.Count })
                .OrderByDescending(b => b.Count)
                .Take(5)
                .ToListAsync(ct);

            // ---- 7) Diğer içerik sayıları ----
            var couponStates = await _context.Coupons.AsNoTracking().Select(c => c.IsActive).ToListAsync(ct);

            return new GetSiteOverviewQueryResult
            {
                ProductCount = products.Count,
                CategoryCount = categories.Count,
                VariantCount = products.Sum(p => p.Variants),
                AvailableVariantCount = products.Sum(p => p.AvailableVariants),
                ProductsWithoutVariants = products.Count(p => p.Variants == 0),
                MinPrice = products.Count == 0 ? 0 : products.Min(p => p.Price),
                AveragePrice = products.Count == 0 ? 0 : Math.Round(products.Average(p => p.Price), 2),
                MaxPrice = products.Count == 0 ? 0 : products.Max(p => p.Price),

                BlogCount = await _context.Blogs.CountAsync(ct),
                CommentCount = await _context.Comments.CountAsync(ct),
                TestimonialCount = await _context.Testimonials.CountAsync(ct),
                BannerCount = await _context.Banners.CountAsync(ct),
                PromotionCount = await _context.Promotions.CountAsync(ct),
                ClientCount = await _context.Clients.CountAsync(ct),
                HistoryCount = await _context.OurHistories.CountAsync(ct),
                VideoCount = await _context.Videos.CountAsync(ct),

                UserCount = users.Count(u => !u.IsDeleted),
                ConfirmedUserCount = users.Count(u => !u.IsDeleted && u.EmailConfirmed),
                DeletedUserCount = users.Count(u => u.IsDeleted),
                Roles = roles.Select(r => new ChartItem(r.Name ?? "-", r.Count)).OrderByDescending(r => r.Value).ToList(),

                MessageCount = messages.Count,
                UnreadMessageCount = messages.Count(m => m.MessageStatus == ContactMessageStatus.Pending),
                MessageStatuses =
                [
                    new("Bekliyor", messages.Count(m => m.MessageStatus == ContactMessageStatus.Pending)),
                    new("Okundu", messages.Count(m => m.MessageStatus == ContactMessageStatus.Read)),
                    new("Yanıtlandı", messages.Count(m => m.MessageStatus == ContactMessageStatus.Replied))
                ],
                RecentMessages = messages.OrderByDescending(m => m.CreatedAt).Take(5)
                    .Select(m => new RecentMessage(m.NameSurname, m.Subject, m.MessageStatus, m.CreatedAt)).ToList(),

                CouponCount = couponStates.Count,
                ActiveCouponCount = couponStates.Count(active => active),
                InstallmentRateCount = await _context.Installments.CountAsync(ct),
                OrderCount = orders.Count,
                PaidOrderCount = orders.Count(o => o.Status == OrderStatus.Paid),
                LifetimeRevenue = orders.Where(o => o.Status == OrderStatus.Paid).Sum(o => o.PaidPrice),

                Categories = categoryStats,
                PriceBuckets = priceBuckets,
                Activity = activity,
                MostExpensive = mostExpensive,
                Cheapest = cheapest,
                MostCommentedBlogs = topBlogs.Select(b => new ChartItem(b.Title, b.Count)).ToList()
            };
        }
    }
}