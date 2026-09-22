using Bagery.WebUI.Context;
using Bagery.WebUI.Entities;
using Bagery.WebUI.Enums;
using Bagery.WebUI.MediatorPattern.Queries.UserQueries;
using Bagery.WebUI.MediatorPattern.Results.UserResults;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bagery.WebUI.MediatorPattern.Handlers.UserHandlers
{
    public class GetAdminUserDetailQueryHandler(AppDbContext _context,
                                                UserManager<AppUser> _userManager) : IRequestHandler<GetAdminUserDetailQuery, GetAdminUserDetailQueryResult?>
    {
        public async Task<GetAdminUserDetailQueryResult?> Handle(GetAdminUserDetailQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user is null) return null;

            var roles = await _userManager.GetRolesAsync(user);

            // Siparişler (başarısız denemeler de görünsün: destek için gerekli olabiliyor)
            var orders = await _context.Orders.AsNoTracking()
                .Where(o => o.AppUserId == user.Id)
                .Select(o => new
                {
                    o.OrderNo,
                    o.CreatedAt,
                    o.Status,
                    o.DeliveryStatus,
                    o.PaidPrice,
                    ItemCount = o.OrderItems.Sum(i => (int?)i.Quantity) ?? 0
                })
                .ToListAsync(cancellationToken);

            var paid = orders.Where(o => o.Status == OrderStatus.Paid).ToList();

            // Blogları ve bloglarına gelen yorumlar
            var blogs = await _context.Blogs.AsNoTracking()
                .Where(b => b.AppUserId == user.Id)
                .Select(b => new
                {
                    b.Id,
                    b.Title,
                    b.CreatedAt,
                    CommentCount = b.Comments.Count(c => c.AppUserId != user.Id) // kendi yorumları sayılmaz
                })
                .ToListAsync(cancellationToken);

            // Yazdığı yorumlar
            var comments = await _context.Comments.AsNoTracking()
                .Where(c => c.AppUserId == user.Id)
                .OrderByDescending(c => c.CreatedAt)
                .Take(5)
                .Select(c => new AdminUserComment(c.BlogId, c.Blog.Title, c.CommentContent, c.CreatedAt))
                .ToListAsync(cancellationToken);

            var commentsWritten = await _context.Comments.CountAsync(c => c.AppUserId == user.Id, cancellationToken);

            return new GetAdminUserDetailQueryResult
            {
                Id = user.Id,
                FullName = user.FullName ?? "-",
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                ImageUrl = user.ImageUrl,
                Job = user.Job,
                AboutMe = user.AboutMe,
                FacebookUrl = user.FacebookUrl,
                TwitterUrl = user.TwitterUrl,
                InstagramUrl = user.InstagramUrl,
                EmailConfirmed = user.EmailConfirmed,
                Roles = roles.OrderBy(r => r).ToList(),

                PaidOrders = paid.Count,
                PendingOrders = orders.Count(o => o.Status == OrderStatus.Pending),
                FailedOrders = orders.Count(o => o.Status == OrderStatus.Failed),
                TotalSpent = paid.Sum(o => o.PaidPrice),
                AverageBasket = paid.Count == 0 ? 0 : Math.Round(paid.Sum(o => o.PaidPrice) / paid.Count, 2),
                LastOrderAt = orders.Count == 0 ? null : orders.Max(o => o.CreatedAt),
                Orders = orders.OrderByDescending(o => o.CreatedAt).Take(5)
                    .Select(o => new AdminUserOrder(o.OrderNo, o.CreatedAt, o.Status, o.DeliveryStatus, o.PaidPrice, o.ItemCount))
                    .ToList(),

                BlogCount = blogs.Count,
                CommentsWritten = commentsWritten,
                CommentsReceived = blogs.Sum(b => b.CommentCount),
                Blogs = blogs.OrderByDescending(b => b.CreatedAt).Take(5)
                    .Select(b => new AdminUserBlog(b.Id, b.Title, b.CreatedAt, b.CommentCount))
                    .ToList(),
                Comments = comments
            };
        }
    }
}