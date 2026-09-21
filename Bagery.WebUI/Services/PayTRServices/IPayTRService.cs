namespace Bagery.WebUI.Services.PayTRServices
{
    // PayTR ile konuşan TEK yer. Handler'lar hash, HttpClient, form alanı bilmez.
    // Bir sorun olursa BusinessException fırlatır, ExceptionFilter kullanıcıya JSON olarak döner.
    public interface IPayTRService
    {
        // Kartın ilk 8 hanesi -> kart ailesi + kredi kartı mı
        Task<PayTRCardInfo> GetCardInfoAsync(string binNumber, CancellationToken cancellationToken = default);

        // Bütün kart ailelerinin taksit oranları
        Task<List<PayTRInstallmentRate>> GetInstallmentRatesAsync(CancellationToken cancellationToken = default);

        // Başarılıysa PayTR'nin döndürdüğü 3D Secure HTML'ini döner
        Task<string> CreatePaymentAsync(PayTRPaymentRequest request, CancellationToken cancellationToken = default);

        // Callback gerçekten PayTR'den mi geldi?
        bool IsCallbackHashValid(string merchantOid, string status, string totalAmount, string hash);
    }
}