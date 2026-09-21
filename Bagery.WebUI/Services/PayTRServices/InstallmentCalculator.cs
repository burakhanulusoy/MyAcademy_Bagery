namespace Bagery.WebUI.Services.PayTRServices
{
    // Taksit tablosunda GÖSTERİLEN tutar ile karttan ÇEKİLEN tutar aynı formülden çıksın diye tek yerde
    public static class InstallmentCalculator
    {
        // PAY_TR'deki formül: tutar * (1 + oran / 100), 2 haneye yuvarla
        public static decimal ApplyRate(decimal amount, decimal rate)
        {
            return decimal.Round(amount * (1 + rate / 100m), 2, MidpointRounding.AwayFromZero);
        }

        // Aylık taksit tutarı (tek çekimde toplamın kendisi)
        public static decimal MonthlyPrice(decimal total, int installmentCount)
        {
            return installmentCount <= 1
                ? total
                : decimal.Round(total / installmentCount, 2, MidpointRounding.AwayFromZero);
        }
    }
}