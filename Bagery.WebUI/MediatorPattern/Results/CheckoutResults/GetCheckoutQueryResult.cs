namespace Bagery.WebUI.MediatorPattern.Results.CheckoutResults
{
    public class GetCheckoutQueryResult
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }   // AppUser'da boş olabilir, kullanıcı formda doldurur
        public string? Address { get; set; }
        public bool IsCartEmpty { get; set; }      // boşsa controller sepet sayfasına yönlendirir
    }
}