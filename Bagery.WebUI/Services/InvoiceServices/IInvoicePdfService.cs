using Bagery.WebUI.Entities;

namespace Bagery.WebUI.Services.InvoiceServices
{
    public interface IInvoicePdfService
    {
        // Siparişi (ürünleriyle) ve satıcı bilgisini alır, PDF dosyasının byte'larını döner
        byte[] Generate(Order order, Contact? seller);
    }
}