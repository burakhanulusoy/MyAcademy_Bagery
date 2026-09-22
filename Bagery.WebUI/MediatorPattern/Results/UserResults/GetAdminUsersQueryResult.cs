namespace Bagery.WebUI.MediatorPattern.Results.UserResults
{
    public class GetAdminUsersQueryResult
    {
        public List<AdminUserListItem> Users { get; set; } = []; // süzülmüş liste
        public int TotalCount { get; set; }                      // süzme öncesi toplam
        public int AdminCount { get; set; }
        public int WriterCount { get; set; }
        public int WaiterCount { get; set; }
        public int MemberCount { get; set; }
        public int NoRoleCount { get; set; }
        public int UnconfirmedCount { get; set; }
    }

    public record AdminUserListItem(
        Guid Id, string FullName, string? Email, string? ImageUrl, string? Job,
        bool EmailConfirmed, List<string> Roles,
        int PaidOrders, decimal TotalSpent, DateTime? LastOrderAt,
        int BlogCount, int CommentCount);
}