namespace Bagery.WebUI.Enums
{
    public enum OrderStatus
    {
        Pending = 0, // PayTR'ye gönderildi, callback bekleniyor
        Paid = 1,    // callback: status = success
        Failed = 2   // callback: status = failed
    }
}
