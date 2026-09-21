using Bagery.WebUI.Enums;

namespace Bagery.WebUI.MediatorPattern.Results.DashboardResults
{
    public class GetSalesDashboardQueryResult
    {
        // Dönem (yerel saat)
        public int Days { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        // Para: sadece ödemesi onaylanan siparişler, kuruşuna kadar
        public decimal Revenue { get; set; }            // karttan çekilen toplam
        public decimal RevenueChange { get; set; }      // önceki döneme göre %
        public decimal ProductSales { get; set; }       // ürünlerin toplamı
        public decimal ShippingIncome { get; set; }
        public decimal InstallmentIncome { get; set; }  // vade farkı
        public decimal CouponDiscount { get; set; }
        public decimal AverageBasket { get; set; }
        public decimal AverageBasketChange { get; set; }

        // Adetler
        public int PaidOrders { get; set; }
        public decimal PaidOrdersChange { get; set; }
        public int PendingOrders { get; set; }
        public int FailedOrders { get; set; }
        public int ItemsSold { get; set; }
        public decimal SuccessRate { get; set; }        // ödenen / (ödenen + başarısız) %

        // Grafikler
        public List<DailyPoint> Daily { get; set; } = [];
        public int[] Hourly { get; set; } = new int[24];
        public List<ChartItem> CategoryRevenue { get; set; } = [];
        public List<ChartItem> PaymentTypes { get; set; } = [];
        public List<ChartItem> CardBrands { get; set; } = [];

        // Listeler
        public List<ProductSale> TopProducts { get; set; } = [];
        public List<ProductSale> LeastProducts { get; set; } = [];
        public List<CouponUsage> Coupons { get; set; } = [];
        public List<RecentOrder> RecentOrders { get; set; } = [];
    }

    // Bu sonuca ait küçük tipler (sadece burada kullanıldıkları için aynı dosyada)
    public record DailyPoint(string Label, decimal Revenue, int Orders);
    public record ProductSale(Guid ProductId, string Name, string Category, string? ImageUrl, int Quantity, decimal Revenue, decimal Share);
    public record CouponUsage(string Code, int Uses, decimal Discount);
    public record RecentOrder(string OrderNo, string FullName, DateTime CreatedAt, decimal PaidPrice, OrderStatus Status);
}