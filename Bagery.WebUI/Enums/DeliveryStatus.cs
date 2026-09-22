namespace Bagery.WebUI.Enums
{
    // Sadece ÖDENMİŞ siparişler için anlamlı; ödeme durumundan (OrderStatus) ayrı
    public enum DeliveryStatus
    {
        Waiting = 0,    // ödendi, henüz çıkmadı
        OnTheWay = 1,   // yola çıktı
        Delivered = 2   // teslim edildi
    }
}