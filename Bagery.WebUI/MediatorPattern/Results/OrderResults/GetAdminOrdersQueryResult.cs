using Bagery.WebUI.Enums;

namespace Bagery.WebUI.MediatorPattern.Results.OrderResults
{
    // Liste sayfasının tamamı: özet kartları + filtre + tablo
    public class GetAdminOrdersQueryResult
    {
        public OrderStatus? SelectedStatus { get; set; }   // hangi filtre butonu seçili görünsün
        public decimal PaidTotal { get; set; }             // sadece ödenen siparişlerin toplamı (ciro)
        public int PaidCount { get; set; }
        public int PendingCount { get; set; }
        public int FailedCount { get; set; }
        public int TotalCount => PaidCount + PendingCount + FailedCount;
        public List<GetAdminOrderListItemResult> Orders { get; set; } = [];
    }
}