using Bagery.WebUI.Enums;

namespace Bagery.WebUI.MediatorPattern.Results.PanelResults
{
    public class GetUserDashboardQueryResult
    {
        public string FullName { get; set; } = string.Empty;

        // Seviye (gerçek veriden hesaplanan oyunlaştırma)
        public int Xp { get; set; }
        public int Level { get; set; }
        public string LevelTitle { get; set; } = string.Empty;
        public int LevelProgress { get; set; }
        public int XpPerLevel { get; set; }

        // Alışveriş
        public int PaidOrders { get; set; }
        public int PendingOrders { get; set; }
        public decimal TotalSpent { get; set; }
        public decimal AverageBasket { get; set; }
        public int ItemsBought { get; set; }
        public string? FavoriteCategory { get; set; }
        public List<UserMonthPoint> Months { get; set; } = [];
        public List<UserProductStat> TopProducts { get; set; } = [];
        public UserLastOrder? LastOrder { get; set; }

        // Topluluk
        public int CommentsWritten { get; set; }

        public List<PanelBadge> Badges { get; set; } = []; // PanelBadge: Writer sonucunda tanımlı
    }

    public record UserMonthPoint(string Label, int Orders, decimal Spent);
    public record UserProductStat(string Name, int Quantity);
    public record UserLastOrder(string OrderNo, DateTime CreatedAt, OrderStatus Status, decimal PaidPrice, int ItemCount);
}