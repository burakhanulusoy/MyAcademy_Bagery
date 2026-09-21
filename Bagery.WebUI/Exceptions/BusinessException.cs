namespace Bagery.WebUI.Exceptions
{
    // "Ürün bulunamadı", "Kupon geçersiz" gibi iş kuralı hataları için. PAYTR İÇİN
  
    public class BusinessException(string message) : Exception(message);
}
