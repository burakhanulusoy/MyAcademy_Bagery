using Bagery.WebUI.MediatorPattern.Queries.CategoryQueries;
using Bagery.WebUI.MediatorPattern.Queries.ShopPageQueries;
using Bagery.WebUI.MediatorPattern.Results.ShopPageResults;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.ProductHandlers
{
    // Bu handler, Shop sayfasının ihtiyaç duyduğu HER ŞEYİ tek seferde hazırlar:
    // filtrelenmiş ürünler + kategori listesi + sayfalama bilgisi + aktif filtreler.
    // Böylece controller tek satırda iş görür, ViewBag kullanmaya gerek kalmaz.
    public class GetShopPageQueryHandler(IMediator _mediator)
        : IRequestHandler<GetShopPageQuery, ShopPageResult>
    {
        // Bir sayfada kaç ürün gösterilecek.
        // const olduğu için derleme anında sabitlenir, değiştirmek istersen tek yer burası.
        private const int PageSize = 9;

        public async Task<ShopPageResult> Handle(GetShopPageQuery request, CancellationToken cancellationToken)
        {
            // 1) VERİYİ ÇEK
            // Mevcut GetCategoriesWithProductsQuery'yi tekrar kullanıyoruz.
            // Dönen yapı iç içe: her kategorinin içinde kendi ürün listesi var.
            // Örn: [ { Kahveler: [A, B] }, { Tatlılar: [C, D] } ]
            var categories = await _mediator.Send(new GetCategoriesWithProductsQuery(), cancellationToken);

            // 2) KATEGORİ FİLTRESİ + DÜZLEŞTİRME
            // Kategori seçilmişse önce o kategoriyi ayıklıyoruz, seçilmemişse hepsini alıyoruz.
            // SelectMany ise iç içe yapıyı tek düz listeye çeviriyor:
            // [[A,B],[C,D]] -> [A,B,C,D]
            // Sonuç IEnumerable olarak duruyor, henüz hiçbir şey hesaplanmadı (deferred execution).
            var products = request.CategoryId.HasValue
                ? categories.Where(c => c.Id == request.CategoryId).SelectMany(c => c.Products)
                : categories.SelectMany(c => c.Products);

            // 3) ARAMA FİLTRESİ
            // Kullanıcı arama kutusuna bir şey yazdıysa, ürün adında o kelimeyi arıyoruz.
            // IsNullOrWhiteSpace kontrolü: boş string veya sadece boşluk gelirse filtre uygulanmasın.
            // OrdinalIgnoreCase: "KAHVE", "kahve", "Kahve" hepsi aynı sonucu versin.
            if (!string.IsNullOrWhiteSpace(request.Search))
                products = products.Where(p => p.ProductName.Contains(request.Search, StringComparison.OrdinalIgnoreCase));

            // 4) FİYAT FİLTRELERİ
            // HasValue kontrolü sayesinde kullanıcı sadece alt sınır, sadece üst sınır
            // veya ikisini birden girebiliyor. Girmediği alan filtreye dahil olmuyor.
            // Her Where bir öncekinin üstüne biniyor, yani filtreler BİRİKİMLİ çalışıyor:
            // kategori + arama + fiyat aynı anda uygulanabiliyor.
            if (request.Min.HasValue)
                products = products.Where(p => p.Price >= request.Min);

            if (request.Max.HasValue)
                products = products.Where(p => p.Price <= request.Max);

            // 5) FİLTRELERİ ÇALIŞTIR
            // Buraya kadar hiçbir Where gerçekten çalışmadı; LINQ sadece "ne yapılacağını" biriktirdi.
            // ToList() çağrıldığı anda hepsi tek seferde uygulanıyor.
            // Bu liste FİLTRELENMİŞ ama HENÜZ SAYFALANMAMIŞ tüm ürünleri tutuyor.
            // Sayfa sayısını hesaplamak için toplam adede ihtiyacımız olduğundan bu ara adım şart.
            var list = products.ToList();

            return new ShopPageResult
            {
                // 6) SAYFALAMA
                // Skip: baştan kaç kayıt atlanacak. Take: kaç kayıt alınacak.
                // Sayfa 1 -> Skip(0).Take(9)   (0-8 arası)
                // Sayfa 2 -> Skip(9).Take(9)   (9-17 arası)
                // (Page - 1) çarpanı bu yüzden var: sayfa numarası 1'den, atlama 0'dan başlıyor.
                Products = list.Skip((request.Page - 1) * PageSize).Take(PageSize).ToList(),

                // Sol menüdeki kategori listesi. Ürünler filtrelense bile bu liste hep tam kalır,
                // çünkü kullanıcının başka bir kategoriye geçebilmesi gerekiyor.
                Categories = categories,

                // 7) TOPLAM SAYFA SAYISI
                // (double) dönüşümü KRİTİK: onsuz 20/9 tam sayı bölmesiyle 2 verir ve son 2 ürün kaybolur.
                // double ile 2.22 olur, Ceiling yukarı yuvarlayıp 3 yapar.
                TotalPages = (int)Math.Ceiling(list.Count / (double)PageSize),

                // 8) AKTİF FİLTRELERİ GERİ DÖNDÜR
                // Bunlar neden geri dönüyor? İki sebep:
                // a) Arama kutusunda value="@Model.Search" yazınca aranan kelime kutuda kalsın.
                // b) Sayfalama linklerine eklensin ki 2. sayfaya geçince filtreler kaybolmasın.
                Page = request.Page,
                CategoryId = request.CategoryId,
                Search = request.Search,
                Min = request.Min,
                Max = request.Max
            };
        }
    }
}