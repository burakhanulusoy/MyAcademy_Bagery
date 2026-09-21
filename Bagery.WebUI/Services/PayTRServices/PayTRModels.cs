namespace Bagery.WebUI.Services.PayTRServices
{
    // BIN sorgusunun cevabı. PAY_TR'deki CardBrandResponse'un karşılığı
    // (Success ve ErrorMessage alanları yok: hata olursa servis exception fırlatıyor)
    public record PayTRCardInfo(string Brand, bool IsCreditCard);

    // PayTR'den gelen tek bir taksit oranı, ör. ("world", 3, 2.45)
    public record PayTRInstallmentRate(string Brand, int InstallmentCount, decimal Rate);

    // user_basket'e giden tek satır
    public record PayTRBasketItem(string Name, decimal UnitPrice, int Quantity);

    // Ödeme isteği için gereken her şey. Handler (Adım 8) doldurup servise verecek.
    public class PayTRPaymentRequest
    {
        public string OrderNo { get; set; } = string.Empty;
        public string UserIp { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public decimal PaymentAmount { get; set; }          // taksit farkı dahil
        public int InstallmentCount { get; set; }            // 0 = tek çekim
        public string CardType { get; set; } = string.Empty; // taksitte kart ailesi (world, bonus...)

        public string CardOwner { get; set; } = string.Empty;
        public string CardNumber { get; set; } = string.Empty;
        public string ExpiryMonth { get; set; } = string.Empty;
        public string ExpiryYear { get; set; } = string.Empty;
        public string Cvv { get; set; } = string.Empty;

        // PAY_TR'deki sabit "Ahmet Yılmaz" UserModel'inin yerine; gerçek kullanıcıdan gelecek
        public string UserName { get; set; } = string.Empty;
        public string UserAddress { get; set; } = string.Empty;
        public string UserPhone { get; set; } = string.Empty;

        public List<PayTRBasketItem> Basket { get; set; } = [];

        public string OkUrl { get; set; } = string.Empty;
        public string FailUrl { get; set; } = string.Empty;
    }
}