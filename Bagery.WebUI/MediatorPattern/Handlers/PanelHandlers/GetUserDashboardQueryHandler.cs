using Bagery.WebUI.Context;
using Bagery.WebUI.Entities;
using Bagery.WebUI.Enums;
using Bagery.WebUI.Exceptions;
using Bagery.WebUI.MediatorPattern.Queries.PanelQueries;
using Bagery.WebUI.MediatorPattern.Results.PanelResults;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Bagery.WebUI.MediatorPattern.Handlers.PanelHandlers
{
    public class GetUserDashboardQueryHandler(AppDbContext _context,
                                              UserManager<AppUser> _userManager,
                                              IHttpContextAccessor _httpContextAccessor) : IRequestHandler<GetUserDashboardQuery, GetUserDashboardQueryResult>
    {
        private static readonly CultureInfo Tr = CultureInfo.GetCultureInfo("tr-TR");
        private const int XpPerLevel = 300;
        private static readonly string[] LevelTitles = ["Tatlı Meraklısı", "Şekerli Dost", "Pasta Tutkunu", "Tatlı Gurmesi", "Bagery Efsanesi"];

        public async Task<GetUserDashboardQueryResult> Handle(GetUserDashboardQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext!.User)
                       ?? throw new IdentityException("Bu sayfa için giriş yapmalısınız.");
            var userId = user.Id;

            // ---- 1) Üyenin siparişleri (başarısız ödeme denemeleri hariç) ----
            var orders = await _context.Orders.AsNoTracking()
                .Where(o => o.AppUserId == userId && o.Status != OrderStatus.Failed)
                .Select(o => new
                {
                    o.OrderNo,
                    o.CreatedAt,
                    o.Status,
                    o.PaidPrice,
                    o.CouponCode,
                    Items = o.OrderItems.Select(i => new { i.ProductId, i.ProductName, i.Quantity }).ToList()
                })
                .ToListAsync(cancellationToken);

            // ---- 2) Yazdığı yorum sayısı ----
            var commentCount = await _context.Comments.CountAsync(c => c.AppUserId == userId, cancellationToken);

            var paid = orders.Where(o => o.Status == OrderStatus.Paid).ToList();
            var items = paid.SelectMany(o => o.Items).ToList();
            var totalSpent = paid.Sum(o => o.PaidPrice);
            var itemsBought = items.Sum(i => i.Quantity);

            // En çok aldığı 5 ürün
            var topProducts = items
                .GroupBy(i => i.ProductName)
                .Select(g => new UserProductStat(g.Key, g.Sum(i => i.Quantity)))
                .OrderByDescending(p => p.Quantity).ThenBy(p => p.Name)
                .Take(5)
                .ToList();

            // ---- 3) En sevdiği kategori: aldığı ürünlerin kategorilerine göre adet ----
            var productIds = items.Select(i => i.ProductId).Distinct().ToList();
            var categoryOf = await _context.Products.AsNoTracking()
                .Where(p => productIds.Contains(p.Id))
                .Select(p => new { p.Id, p.Category.CategoryName })
                .ToDictionaryAsync(p => p.Id, p => p.CategoryName, cancellationToken);

            var favoriteCategory = items
                .Where(i => categoryOf.ContainsKey(i.ProductId))           // silinmiş ürünler sayılmaz
                .GroupBy(i => categoryOf[i.ProductId])
                .OrderByDescending(g => g.Sum(i => i.Quantity))
                .Select(g => g.Key)
                .FirstOrDefault();

            // ---- Son 6 ay: sipariş sayısı ve harcama (Türkiye saatine göre) ----
            var monthStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0, DateTimeKind.Local).AddMonths(-5);

            static bool SameMonth(DateTime utc, DateTime month)
            {
                var local = utc.ToLocalTime();
                return local.Year == month.Year && local.Month == month.Month;
            }

            var months = Enumerable.Range(0, 6)
                .Select(i => monthStart.AddMonths(i))
                .Select(m =>
                {
                    var inMonth = paid.Where(o => SameMonth(o.CreatedAt, m)).ToList();
                    return new UserMonthPoint(m.ToString("MMM", Tr), inMonth.Count, inMonth.Sum(o => o.PaidPrice));
                })
                .ToList();

            var last = orders.OrderByDescending(o => o.CreatedAt).FirstOrDefault();

            // ---- Seviye ----
            var xp = paid.Count * 50 + itemsBought * 5 + commentCount * 10;
            var level = xp / XpPerLevel + 1;

            return new GetUserDashboardQueryResult
            {
                FullName = user.FullName ?? user.UserName ?? "Üye",

                Xp = xp,
                Level = level,
                LevelTitle = LevelTitles[Math.Min(level, LevelTitles.Length) - 1],
                LevelProgress = xp % XpPerLevel,
                XpPerLevel = XpPerLevel,

                PaidOrders = paid.Count,
                PendingOrders = orders.Count(o => o.Status == OrderStatus.Pending),
                TotalSpent = totalSpent,
                AverageBasket = paid.Count == 0 ? 0 : Math.Round(totalSpent / paid.Count, 2),
                ItemsBought = itemsBought,
                FavoriteCategory = favoriteCategory,
                Months = months,
                TopProducts = topProducts,
                LastOrder = last is null
                    ? null
                    : new UserLastOrder(last.OrderNo, last.CreatedAt, last.Status, last.PaidPrice, last.Items.Sum(i => i.Quantity)),

                CommentsWritten = commentCount,

                Badges =
                [
                    new("İlk sipariş", "İlk siparişini ver", "bx-shopping-bag", paid.Count >= 1),
                    new("Müdavim", "5 sipariş ver", "bx-crown", paid.Count >= 5),
                    new("Sadık dost", "10 sipariş ver", "bx-heart", paid.Count >= 10),
                    new("Koleksiyoncu", "5 farklı ürün dene", "bx-grid-alt", productIds.Count >= 5),
                    new("Büyük sepet", "Tek siparişte 1.000 ₺ üzeri", "bx-cart", paid.Any(o => o.PaidPrice >= 1000)),
                    new("Kupon avcısı", "Bir kupon kullan", "bxs-coupon", paid.Any(o => o.CouponCode != null)),
                    new("Yorumcu", "İlk yorumunu yaz", "bx-comment-dots", commentCount >= 1),
                    new("Sohbetçi", "5 yorum yaz", "bx-conversation", commentCount >= 5)
                ]
            };
        }
    }
}