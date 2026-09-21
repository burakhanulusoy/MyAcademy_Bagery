using Bagery.WebUI.Entities;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Globalization;

namespace Bagery.WebUI.Services.InvoiceServices
{
    // QuestPDF ile A4 fatura. Sayfa üç bölümden oluşur: Header (logo + belge no), Content (taraflar, ürünler, toplamlar), Footer.
    public class InvoicePdfService(IWebHostEnvironment _environment) : IInvoicePdfService
    {
        private const string Navy = "#2b3c6b";   // tema laciverti
        private const string Peach = "#e3a087";  // tema şeftalisi
        private const string Muted = "#848484";
        private static readonly CultureInfo Tr = new("tr-TR");

        // Controller dosya adında da kullanıyor: "BGR-0D999750"
        public static string InvoiceNo(string orderNo) => "BGR-" + orderNo[..8].ToUpperInvariant();

        public byte[] Generate(Order order, Contact? seller)
        {
            // wwwroot'taki logo (beyaz zeminde okunan tek logo)
            var logoPath = Path.Combine(_environment.WebRootPath, "Bagery Pack", "Bagery", "assets", "images", "logo-4.png");
            var issuedAt = (order.PaidAt ?? order.CreatedAt).ToLocalTime();

            // Taksitte karttan çekilen tutar sepet toplamından fazladır, aradaki fark vade farkı
            var installmentFee = order.PaidPrice - (order.TotalPrice + order.ShippingPrice - order.CouponPrice);

            return Document.Create(document =>
            {
                document.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(40); // 40 pt ~ 1,4 cm
                    page.DefaultTextStyle(x => x.FontSize(10).FontColor("#222222"));

                    page.Header().Element(header => ComposeHeader(header, logoPath, InvoiceNo(order.OrderNo), issuedAt, order.OrderNo));
                    page.Content().Element(content => ComposeContent(content, order, seller, installmentFee));

                    page.Footer().AlignCenter().Text(text =>
                    {
                        text.DefaultTextStyle(x => x.FontSize(8).FontColor(Muted));
                        text.Span("Bu belge sipariş bilgilendirme amaçlıdır, e-Arşiv fatura yerine geçmez.   Sayfa ");
                        text.CurrentPageNumber();
                        text.Span(" / ");
                        text.TotalPages();
                    });
                });
            }).GeneratePdf(); // byte[] olarak döner
        }

        // ---------- ÜST: logo + FATURA + belge bilgileri ----------
        private static void ComposeHeader(IContainer container, string logoPath, string invoiceNo, DateTime issuedAt, string orderNo)
        {
            container.BorderBottom(3).BorderColor(Peach).PaddingBottom(12).Row(row =>
            {
                if (File.Exists(logoPath))
                    row.ConstantItem(150).Image(logoPath).FitWidth();
                else
                    row.ConstantItem(150).Text("Bagery.").FontSize(24).Bold().FontColor(Navy); // logo bulunamazsa yazı

                row.RelativeItem().AlignRight().Column(col =>
                {
                    col.Item().AlignRight().Text("FATURA").FontSize(22).Bold().FontColor(Navy);
                    col.Item().AlignRight().Text($"Belge no: {invoiceNo}");
                    col.Item().AlignRight().Text($"Tarih: {issuedAt:dd.MM.yyyy HH:mm}");
                    col.Item().AlignRight().Text($"Sipariş no: {orderNo}").FontSize(8).FontColor(Muted);
                });
            });
        }

        // ---------- İÇERİK ----------
        private static void ComposeContent(IContainer container, Order order, Contact? seller, decimal installmentFee)
        {
            container.PaddingTop(20).Column(col =>
            {
                col.Spacing(18); // bölümler arası boşluk

                // Satıcı / Alıcı yan yana
                col.Item().Row(row =>
                {
                    row.RelativeItem().Element(c => PartyBlock(c, "SATICI", "Bagery Caffe", seller?.Address, seller?.PhoneNumber, seller?.Email));
                    row.ConstantItem(30); // iki blok arası boşluk
                    row.RelativeItem().Element(c => PartyBlock(c, "ALICI", order.FullName, order.Address, order.PhoneNumber, order.Email));
                });

                // Ürün tablosu (kalem kalem)
                col.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(25);  // #
                        columns.RelativeColumn();    // Ürün (kalan genişlik)
                        columns.ConstantColumn(45);  // Adet
                        columns.ConstantColumn(80);  // Birim fiyat
                        columns.ConstantColumn(85);  // Tutar
                    });

                    table.Header(header =>
                    {
                        header.Cell().Element(HeaderCell).Text("#");
                        header.Cell().Element(HeaderCell).Text("Ürün");
                        header.Cell().Element(HeaderCell).AlignRight().Text("Adet");
                        header.Cell().Element(HeaderCell).AlignRight().Text("Birim fiyat");
                        header.Cell().Element(HeaderCell).AlignRight().Text("Tutar");
                    });

                    var lineNo = 0;
                    foreach (var item in order.OrderItems)
                    {
                        lineNo++;
                        var name = item.VariantName is null ? item.ProductName : $"{item.ProductName} ({item.VariantName})";

                        table.Cell().Element(BodyCell).Text(lineNo.ToString());
                        table.Cell().Element(BodyCell).Text(name);
                        table.Cell().Element(BodyCell).AlignRight().Text(item.Quantity.ToString());
                        table.Cell().Element(BodyCell).AlignRight().Text(Money(item.UnitPrice));
                        table.Cell().Element(BodyCell).AlignRight().Text(Money(item.UnitPrice * item.Quantity));
                    }
                });

                // Toplamlar (sağa yaslı)
                col.Item().AlignRight().Width(230).Column(totals =>
                {
                    totals.Spacing(4);
                    TotalLine(totals, "Ara toplam", Money(order.TotalPrice));
                    if (order.CouponPrice > 0)
                        TotalLine(totals, $"İndirim ({order.CouponCode})", "-" + Money(order.CouponPrice));
                    TotalLine(totals, "Kargo", order.ShippingPrice == 0 ? "Ücretsiz" : Money(order.ShippingPrice));
                    if (installmentFee > 0)
                        TotalLine(totals, $"Vade farkı ({order.InstallmentCount} taksit)", Money(installmentFee));

                    totals.Item().BorderTop(2).BorderColor(Navy).PaddingTop(6).Row(row =>
                    {
                        row.RelativeItem().Text("Genel toplam").FontSize(13).Bold().FontColor(Navy);
                        row.AutoItem().Text(Money(order.PaidPrice)).FontSize(13).Bold().FontColor(Navy);
                    });
                });

                // Ödeme notu
                var paymentText = order.InstallmentCount == 0 ? "tek çekim" : $"{order.InstallmentCount} taksit";
                col.Item().Background("#fcf5f3").Padding(10)
                   .Text($"Ödeme: Kart ile, {paymentText}. Fiyatlara KDV dahildir.")
                   .FontSize(9).FontColor("#555555");
            });
        }

        // ---------- yardımcılar ----------

        private static void PartyBlock(IContainer container, string title, string name, string? address, string? phone, string? email)
        {
            container.Column(col =>
            {
                col.Spacing(2);
                col.Item().Text(title).FontSize(9).Bold().FontColor(Peach);
                col.Item().Text(name).Bold();
                if (!string.IsNullOrWhiteSpace(address)) col.Item().Text(address);
                if (!string.IsNullOrWhiteSpace(phone)) col.Item().Text(phone);
                if (!string.IsNullOrWhiteSpace(email)) col.Item().Text(email);
            });
        }

        private static void TotalLine(ColumnDescriptor column, string label, string value)
        {
            column.Item().Row(row =>
            {
                row.RelativeItem().Text(label).FontColor("#555555");
                row.AutoItem().Text(value);
            });
        }

        // Tablo başlık hücresi: lacivert zemin, beyaz kalın yazı
        private static IContainer HeaderCell(IContainer c) =>
            c.Background(Navy).PaddingVertical(6).PaddingHorizontal(6).DefaultTextStyle(x => x.FontColor(Colors.White).Bold());

        // Tablo satır hücresi: alt çizgi
        private static IContainer BodyCell(IContainer c) =>
            c.BorderBottom(1).BorderColor("#eeeeee").PaddingVertical(6).PaddingHorizontal(6);

        // PDF'te ₺ yerine "TL": QuestPDF'in yazı tipinde ₺ işareti olmayabilir, olmazsa PDF üretimi hata verir
        private static string Money(decimal value) => value.ToString("N2", Tr) + " TL";
    }
}