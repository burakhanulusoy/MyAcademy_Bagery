namespace Bagery.WebUI.MediatorPattern.Results.InstallmentResults
{
    // Ödeme sayfasındaki taksit listesinin tek satırı
    public class GetInstallmentOptionsQueryResult
    {
        public int InstallmentCount { get; set; }  // 0 = tek çekim
        public decimal TotalPrice { get; set; }    // vade farkı dahil toplam
        public decimal MonthlyPrice { get; set; }  // aylık taksit tutarı
    }
}