using Bagery.WebUI.Context;
using Bagery.WebUI.Enums;
using Bagery.WebUI.MediatorPattern.Queries.UserQueries;
using Bagery.WebUI.MediatorPattern.Results.UserResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bagery.WebUI.MediatorPattern.Handlers.UserHandlers
{
    // Liste sadece okur: 5 toplu sorgu, sonra eşleştirme bellekte
    public class GetAdminUsersQueryHandler(AppDbContext _context) : IRequestHandler<GetAdminUsersQuery, GetAdminUsersQueryResult>
    {
        public async Task<GetAdminUsersQueryResult> Handle(GetAdminUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _context.Users.AsNoTracking()
                .Where(u => !u.IsDeleted)
                .Select(u => new { u.Id, u.FullName, u.Email, u.ImageUrl, u.Job, u.EmailConfirmed })
                .ToListAsync(cancellationToken);

            // Kullanıcı -> roller
            var roleRows = await (from userRole in _context.UserRoles
                                  join role in _context.Roles on userRole.RoleId equals role.Id
                                  select new { userRole.UserId, role.Name })
                                 .ToListAsync(cancellationToken);

            var roleMap = roleRows.GroupBy(x => x.UserId)
                .ToDictionary(g => g.Key, g => g.Select(x => x.Name ?? "-").OrderBy(n => n).ToList());

            // Kullanıcı -> ödenmiş sipariş özeti
            var orderMap = (await _context.Orders.AsNoTracking()
                .Where(o => o.Status == OrderStatus.Paid)
                .GroupBy(o => o.AppUserId)
                .Select(g => new { UserId = g.Key, Count = g.Count(), Total = g.Sum(o => o.PaidPrice), Last = g.Max(o => o.CreatedAt) })
                .ToListAsync(cancellationToken))
                .ToDictionary(x => x.UserId);

            var blogMap = (await _context.Blogs.AsNoTracking()
                .GroupBy(b => b.AppUserId)
                .Select(g => new { UserId = g.Key, Count = g.Count() })
                .ToListAsync(cancellationToken))
                .ToDictionary(x => x.UserId, x => x.Count);

            var commentMap = (await _context.Comments.AsNoTracking()
                .GroupBy(c => c.AppUserId)
                .Select(g => new { UserId = g.Key, Count = g.Count() })
                .ToListAsync(cancellationToken))
                .ToDictionary(x => x.UserId, x => x.Count);

            var items = users.Select(u =>
            {
                var roles = roleMap.GetValueOrDefault(u.Id, []);
                orderMap.TryGetValue(u.Id, out var orders);

                return new AdminUserListItem(
                    u.Id, u.FullName ?? "-", u.Email, u.ImageUrl, u.Job, u.EmailConfirmed, roles,
                    orders?.Count ?? 0, orders?.Total ?? 0, orders?.Last,
                    blogMap.GetValueOrDefault(u.Id), commentMap.GetValueOrDefault(u.Id));
            }).ToList();

            // Süzme
            var filtered = items.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var search = request.Search.Trim();
                filtered = filtered.Where(x =>
                    x.FullName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    (x.Email ?? "").Contains(search, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(request.Role))
            {
                filtered = request.Role == "none"
                    ? filtered.Where(x => x.Roles.Count == 0)                 // hiç rolü olmayanlar
                    : filtered.Where(x => x.Roles.Contains(request.Role));
            }

            return new GetAdminUsersQueryResult
            {
                Users = filtered.OrderByDescending(x => x.PaidOrders).ThenBy(x => x.FullName).ToList(),
                TotalCount = items.Count,
                AdminCount = items.Count(x => x.Roles.Contains("Admin")),
                WriterCount = items.Count(x => x.Roles.Contains("Writer")),
                WaiterCount = items.Count(x => x.Roles.Contains("Waiter")),
                MemberCount = items.Count(x => x.Roles.Contains("User")),
                NoRoleCount = items.Count(x => x.Roles.Count == 0),
                UnconfirmedCount = items.Count(x => !x.EmailConfirmed)
            };
        }
    }
}