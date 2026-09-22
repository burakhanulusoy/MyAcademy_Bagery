namespace Bagery.WebUI.MediatorPattern.Results.PanelResults
{
    public class GetWriterDashboardQueryResult
    {
        public string FullName { get; set; } = string.Empty;

        // Seviye (gerçek veriden hesaplanan oyunlaştırma)
        public int Xp { get; set; }
        public int Level { get; set; }
        public string LevelTitle { get; set; } = string.Empty;
        public int LevelProgress { get; set; }   // bu seviyede toplanan XP
        public int XpPerLevel { get; set; }      // bir seviye kaç XP

        // Blog ve yorumlar
        public int BlogCount { get; set; }
        public int CommentsReceived { get; set; }  // başkalarının bloglarına yazdığı yorumlar
        public int CommentsWritten { get; set; }   // yazarın yazdığı yorumlar
        public int BlogsCommentedOn { get; set; }  // kaç farklı bloga yorum yaptı
        public List<WriterMonthPoint> Months { get; set; } = [];
        public List<WriterBlogStat> TopBlogs { get; set; } = [];
        public WriterLatestComment? LatestComment { get; set; }

        // Alışveriş
        public int PaidOrders { get; set; }
        public decimal TotalSpent { get; set; }
        public string? FavoriteProduct { get; set; }
        public int FavoriteQuantity { get; set; }
        public DateTime? LastOrderAt { get; set; }

        public List<PanelBadge> Badges { get; set; } = [];
    }

    public record WriterMonthPoint(string Label, int Blogs, int Received, int Written);
    public record WriterBlogStat(Guid Id, string Title, int Comments);
    public record WriterLatestComment(string? Author, string BlogTitle, string Content, DateTime CreatedAt);
    public record PanelBadge(string Title, string Requirement, string Icon, bool Unlocked);
}