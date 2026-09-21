namespace Bagery.WebUI.MediatorPattern.Results.OrderResults
{
    public class GetMyOrderInvoiceQueryResult
    {
        public byte[] Content { get; set; } = [];             // PDF dosyasının kendisi
        public string FileName { get; set; } = string.Empty;  // Bagery-Fatura-BGR-XXXX.pdf
    }
}