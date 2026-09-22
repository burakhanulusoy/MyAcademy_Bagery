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
    public class GetWriterDashboardQueryHandler(AppDbContext _context,
                                                UserManager<AppUser> _userManager,
                                                IHttpContextAccessor _httpContextAccessor) : IRequestHandler<GetWriterDashboardQuery, GetWriterDashboardQueryResult>
    {
        private static readonly CultureInfo Tr = CultureInfo.GetCultureInfo("tr-TR");
        private const int XpPerLevel = 500;
        private static readonly string[] LevelTitles = ["Çırak Fırıncı", "Hamur Ustası", "Pasta Kalfası", "Baş Pastacı", "Efsane Şef"];

        public async Task<GetWriterDashboardQueryResult> Handle(GetWriterDashboardQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext!.User)
                       ?? throw new IdentityException("Bu sayfa için giriş yapmalısınız.");
            var userId = user.Id;

            // ---- 1) Yazarın blogları ve her bloga gelen yorumlar ----
            var blogs = await _context.Blogs.AsNoTracking()
                .Where(b => b.AppUserId == userId)
                .Select(b => new
                {
                    b.Id,
                    b.Title,
                    b.CreatedAt,
                    Comments = b.Comments
                        .Select(c => new { c.AppUserId, c.CreatedAt, c.CommentContent, Author = c.AppUser.FullName })
                        .ToList()
                })
                .ToListAsync(cancellationToken);

            // ---- 2) Yazarın yazdığı yorumlar ----
            var myComments = await _context.Comments.AsNoTracking()
                .Where(c => c.AppUserId == userId)
                .Select(c => new { c.CreatedAt, c.BlogId })
                .ToListAsync(cancellationToken);

            // ---- 3) Yazarın ödenmiş siparişleri ----
            var orders = await _context.Orders.AsNoTracking()
                .Where(o => o.AppUserId == userId && o.Status == OrderStatus.Paid)
                .Select(o => new
                {
                    o.CreatedAt,
                    o.PaidPrice,
                    Items = o.OrderItems.Select(i => new { i.ProductName, i.Quantity }).ToList()
                })
                .ToListAsync(cancellationToken);

            // Aldığı yorumlar: kendi bloguna kendi yazdıkları sayılmaz
            var received = blogs
                .SelectMany(b => b.Comments
                    .Where(c => c.AppUserId != userId)
                    .Select(c => new { BlogTitle = b.Title, c.CreatedAt, c.CommentContent, c.Author }))
                .ToList();

            var topBlogs = blogs
                .Select(b => new WriterBlogStat(b.Id, b.Title, b.Comments.Count(c => c.AppUserId != userId)))
                .OrderByDescending(b => b.Comments).ThenBy(b => b.Title)
                .Take(5)
                .ToList();

            // ---- Son 6 ay (Türkiye saatine göre) ----
            var monthStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0, DateTimeKind.Local).AddMonths(-5);

            static bool SameMonth(DateTime utc, DateTime month)
            {
                var local = utc.ToLocalTime();
                return local.Year == month.Year && local.Month == month.Month;
            }

            var months = Enumerable.Range(0, 6)
                .Select(i => monthStart.AddMonths(i))
                .Select(m => new WriterMonthPoint(
                    m.ToString("MMM", Tr),
                    blogs.Count(b => SameMonth(b.CreatedAt, m)),
                    received.Count(c => SameMonth(c.CreatedAt, m)),
                    myComments.Count(c => SameMonth(c.CreatedAt, m))))
                .ToList();

            var latest = received.OrderByDescending(c => c.CreatedAt).FirstOrDefault();

            var favorite = orders
                .SelectMany(o => o.Items)
                .GroupBy(i => i.ProductName)
                .Select(g => new { Name = g.Key, Quantity = g.Sum(i => i.Quantity) })
                .OrderByDescending(x => x.Quantity)
                .FirstOrDefault();

            // ---- Seviye: her şey gerçek veriden ----
            var blogCount = blogs.Count;
            var receivedCount = received.Count;
            var writtenCount = myComments.Count;
            var paidCount = orders.Count;
            var distinctBlogs = myComments.Select(c => c.BlogId).Distinct().Count();
            var bestBlogComments = topBlogs.FirstOrDefault()?.Comments ?? 0;

            var xp = blogCount * 100 + receivedCount * 10 + writtenCount * 5 + paidCount * 20;
            var level = xp / XpPerLevel + 1;

            return new GetWriterDashboardQueryResult
            {
                FullName = user.FullName ?? user.UserName ?? "Yazar",

                Xp = xp,
                Level = level,
                LevelTitle = LevelTitles[Math.Min(level, LevelTitles.Length) - 1], // 5. seviyeden sonrası hep "Efsane Şef"
                LevelProgress = xp % XpPerLevel,
                XpPerLevel = XpPerLevel,

                BlogCount = blogCount,
                CommentsReceived = receivedCount,
                CommentsWritten = writtenCount,
                BlogsCommentedOn = distinctBlogs,
                Months = months,
                TopBlogs = topBlogs,
                LatestComment = latest is null
                    ? null
                    : new WriterLatestComment(latest.Author, latest.BlogTitle, Shorten(latest.CommentContent, 160), latest.CreatedAt),

                PaidOrders = paidCount,
                TotalSpent = orders.Sum(o => o.PaidPrice),
                FavoriteProduct = favorite?.Name,
                FavoriteQuantity = favorite?.Quantity ?? 0,
                LastOrderAt = orders.Count == 0 ? null : orders.Max(o => o.CreatedAt),

                Badges =
                [
                    new("İlk tarif", "İlk blogunu yayınla", "bx-edit-alt", blogCount >= 1),
                    new("Seri yazar", "5 blog yayınla", "bx-book-open", blogCount >= 5),
                    new("Sohbet başlatan", "Bloglarına 10 yorum gelsin", "bx-conversation", receivedCount >= 10),
                    new("Popüler kalem", "Bir bloguna 5 yorum gelsin", "bx-star", bestBlogComments >= 5),
                    new("Yorum gurmesi", "10 yorum yaz", "bx-comment-dots", writtenCount >= 10),
                    new("Keşifçi", "3 farklı bloga yorum yap", "bx-compass", distinctBlogs >= 3),
                    new("Tatlı müşteri", "İlk siparişini ver", "bx-shopping-bag", paidCount >= 1),
                    new("Müdavim", "5 sipariş ver", "bx-crown", paidCount >= 5)
                ]
            };
        }

        private static string Shorten(string text, int max) =>
            text.Length <= max ? text : text[..max].TrimEnd() + "…";
    }
}