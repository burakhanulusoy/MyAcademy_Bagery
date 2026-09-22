using Bagery.WebUI.Enums;

namespace Bagery.WebUI.MediatorPattern.Results.UserResults
{
    public class GetAdminUserDetailQueryResult
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? ImageUrl { get; set; }
        public string? Job { get; set; }
        public string? AboutMe { get; set; }
        public string? FacebookUrl { get; set; }
        public string? TwitterUrl { get; set; }
        public string? InstagramUrl { get; set; }
        public bool EmailConfirmed { get; set; }
        public List<string> Roles { get; set; } = [];

        // Alışveriş
        public int PaidOrders { get; set; }
        public int PendingOrders { get; set; }
        public int FailedOrders { get; set; }
        public decimal TotalSpent { get; set; }
        public decimal AverageBasket { get; set; }
        public DateTime? LastOrderAt { get; set; }
        public List<AdminUserOrder> Orders { get; set; } = [];

        // İçerik
        public int BlogCount { get; set; }
        public int CommentsWritten { get; set; }
        public int CommentsReceived { get; set; } // bloglarına gelen yorumlar
        public List<AdminUserBlog> Blogs { get; set; } = [];
        public List<AdminUserComment> Comments { get; set; } = [];
    }

    public record AdminUserOrder(string OrderNo, DateTime CreatedAt, OrderStatus Status, DeliveryStatus DeliveryStatus, decimal PaidPrice, int ItemCount);
    public record AdminUserBlog(Guid Id, string Title, DateTime CreatedAt, int CommentCount);
    public record AdminUserComment(Guid BlogId, string BlogTitle, string Content, DateTime CreatedAt);
}