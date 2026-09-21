namespace Bagery.WebUI.MediatorPattern.Results.PaymentResults
{
    // PAY_TR'deki PaymentCreateResult (Success/ErrorMessage yok, hata olursa exception)
    public class CreatePaymentResult
    {
        public string OrderNo { get; set; } = string.Empty;
        public string RedirectHtml { get; set; } = string.Empty; // PayTR'nin 3D Secure sayfası
    }
}