using Bagery.WebUI.Enums;

namespace Bagery.WebUI.MediatorPattern.Results.DashboardResults
{
    public class GetSiteOverviewQueryResult
    {
        // Katalog
        public int ProductCount { get; set; }
        public int CategoryCount { get; set; }
        public int VariantCount { get; set; }
        public int AvailableVariantCount { get; set; }
        public int ProductsWithoutVariants { get; set; }
        public decimal MinPrice { get; set; }
        public decimal AveragePrice { get; set; }
        public decimal MaxPrice { get; set; }

        // İçerik
        public int BlogCount { get; set; }
        public int CommentCount { get; set; }
        public int TestimonialCount { get; set; }
        public int BannerCount { get; set; }
        public int PromotionCount { get; set; }
        public int ClientCount { get; set; }
        public int HistoryCount { get; set; }
        public int VideoCount { get; set; }

        // Kullanıcılar
        public int UserCount { get; set; }
        public int ConfirmedUserCount { get; set; }
        public int DeletedUserCount { get; set; }
        public List<ChartItem> Roles { get; set; } = [];

        // İletişim
        public int MessageCount { get; set; }
        public int UnreadMessageCount { get; set; }
        public List<ChartItem> MessageStatuses { get; set; } = [];
        public List<RecentMessage> RecentMessages { get; set; } = [];

        // Satış altyapısı (tüm zamanlar)
        public int CouponCount { get; set; }
        public int ActiveCouponCount { get; set; }
        public int InstallmentRateCount { get; set; }
        public int OrderCount { get; set; }
        public int PaidOrderCount { get; set; }
        public decimal LifetimeRevenue { get; set; }

        // Grafik ve tablolar
        public List<CategoryStat> Categories { get; set; } = [];
        public List<ChartItem> PriceBuckets { get; set; } = [];
        public List<MonthPoint> Activity { get; set; } = [];
        public List<PricedProduct> MostExpensive { get; set; } = [];
        public List<PricedProduct> Cheapest { get; set; } = [];
        public List<ChartItem> MostCommentedBlogs { get; set; } = [];
    }

    public record CategoryStat(string Name, int ProductCount, int VariantCount, decimal MinPrice, decimal AveragePrice, decimal MaxPrice);
    public record MonthPoint(string Label, int Blogs, int Comments, int Messages, int Orders);
    public record PricedProduct(Guid Id, string Name, string Category, string? ImageUrl, decimal Price);
    public record RecentMessage(string NameSurname, string Subject, ContactMessageStatus Status, DateTime CreatedAt);
}