

# 🥐 MyAcademy_Bagery

### Yapay Zekâ Destekli Kafe ve Pastane Yönetim Uygulaması

![ASP.NET Core 9](https://img.shields.io/badge/ASP.NET_Core-9.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![PostgreSQL](https://img.shields.io/badge/Database-PostgreSQL-4169E1?style=for-the-badge&logo=postgresql&logoColor=white)
![MediatR](https://img.shields.io/badge/Pattern-MediatR_%26_CQRS-7C3AED?style=for-the-badge)
![Identity](https://img.shields.io/badge/Auth-ASP.NET_Core_Identity-334155?style=for-the-badge)

![PayTR](https://img.shields.io/badge/Payment-PayTR-0F766E?style=for-the-badge)
![SignalR](https://img.shields.io/badge/Realtime-SignalR-2563EB?style=for-the-badge)
![Amazon S3](https://img.shields.io/badge/Storage-Amazon_S3-FF9900?style=for-the-badge)
![OpenAI](https://img.shields.io/badge/AI-OpenAI-111827?style=for-the-badge)

<pre>
       ░░    ░░
        ░░  ░░
      ▄▄▄▄▄▄▄▄▄▄
      █        █▀▀█
      █        █▄▄█
       ▀▀▀▀▀▀▀▀
     ▀▀▀▀▀▀▀▀▀▀▀▀
</pre>

<p align="center">
  <strong>Ürün keşfinden ödemeye, sipariş yönetiminden canlı teslimat takibine.</strong>
</p>
<p align="center">
  <i>Menüye dayalı dijital barista, PayTR ödeme entegrasyonu, SignalR bildirimleri<br>
  ve rol bazlı yönetim panellerini bir araya getiren .NET 9 MVC uygulaması.</i>
</p>

</div>

---

**MyAcademy_Bagery**, bir kafe ve pastane işletmesinin dijital ürün sunumunu, çevrim içi sipariş sürecini ve günlük yönetim ihtiyaçlarını aynı uygulamada buluşturur. Müşteriler ürünleri ve seçeneklerini inceleyebilir, sepetlerine kupon uygulayabilir, ödemelerini tamamlayabilir ve siparişlerinin teslimat durumunu takip edebilir.

İşletme tarafında ise **ürün ve içerik yönetimi, satış analizleri, kullanıcı yetkilendirmesi ve teslimat operasyonu** ayrı paneller üzerinden yürütülür. OpenAI destekli dijital barista ve yorum moderasyonu, Amazon S3 görsel depolama, e-posta bildirimleri ve PDF sipariş çıktıları bu akışı tamamlar.

Proje geliştirilirken **MediatR ile komut–sorgu ayrımı, Repository ve Unit of Work, FluentValidation, Mapster ve merkezi hata işleme** gibi yaklaşımlar birlikte kullanılmıştır.

---

## 🌟 Temel Modüller ve Yetenekler

### 🧠 1. Yapay Zekâ Destekli Dijital Barista ve Yorum Moderasyonu

Yapay zekâ entegrasyonu, müşterinin menüyü keşfetmesine ve blog yorumlarının yayın öncesinde değerlendirilmesine odaklanır.

- **Menüye Dayalı Yanıtlar:** Dijital baristaya veritabanındaki ürün adları, fiyatlar, açıklamalar, hazırlanış bilgileri ve kategoriler iletilir. Böylece sorular uygulamanın kendi menüsü bağlamında ele alınır.
- **Ürün Önerileri:** Bütçeye göre seçim ve yiyecek–içecek eşleştirmesi gibi taleplere yanıt üretilir. Modelin döndürdüğü ürün kimlikleri mevcut katalogla eşleştirilerek ilgili ürünler sunulur.
- **Kapsam Kontrolü:** Asistan, Bagery ve menüyle ilgili konulara yanıt verecek şekilde yapılandırılmıştır. Kapsam dışı olarak sınıflandırılan sorular için yönlendirici bir mesaj gösterilir.
- **Yorum Denetimi:** Blog yorumu kaydedilmeden önce içerik kontrol edilir. Uygunsuz olarak sınıflandırılan yorumlar kullanıcıya bilgi verilerek reddedilir.

### 🛒 2. Varyantlı Ürün Kataloğu ve Dinamik Sepet

Sipariş deneyimi; ürün seçimi, seçeneklerin fiyatlandırılması ve sepet toplamlarının hesaplanması üzerine kuruludur.

- **Ürün ve Varyant Yönetimi:** Kategoriler, ürünler ve ürün seçenekleri yönetilebilir. Varyantlara ek fiyat ve kullanılabilirlik bilgisi tanımlanabilir.
- **Session Tabanlı Sepet:** Ürün ekleme, adet güncelleme ve satır kaldırma işlemleri aynı sepet üzerinden yürütülür. Ürün ve varyant birlikte değerlendirilir.
- **Kupon ve Kargo Hesabı:** Kuponun aktifliği ve minimum sepet koşulu kontrol edilir; ürünler değiştikçe ara toplam, indirim ve kargo tutarı yeniden hesaplanır.
- **Ödeme Öncesi Güncellik:** Fiyatı değişen, kaldırılan veya seçeneği kullanılamayan ürünler ödeme öncesinde yeniden kontrol edilir. Gerekli durumlarda sepet güncellenir ve kullanıcı bilgilendirilir.

### 💳 3. PayTR ile Ödeme ve Taksit Entegrasyonu

Sepetten ödeme sonucuna kadar olan işlemler, uygulamadaki sipariş kayıtlarıyla ilişkilendirilmiştir.

- **Kartla Ödeme:** Ödeme formundaki bilgiler doğrulandıktan sonra PayTR isteği oluşturulur ve kullanıcı 3D doğrulama sürecine yönlendirilir.
- **Taksit Seçenekleri:** Kart bilgisine ve kayıtlı taksit oranlarına göre seçenekler hesaplanır. Seçilen taksitin tutara etkisi siparişe yansıtılır.
- **Sunucu Bildirimi:** Kesin ödeme durumu, PayTR'nin callback bildirimi üzerinden belirlenir. Gelen bildirimin hash değeri kontrol edilir; işlenmiş sipariş durumu yeniden işlenmeden yanıtlanır.
- **Sipariş Kaydı:** Ürün adı, varyant, adet, birim fiyat, kupon ve ödeme bilgileri sipariş anındaki değerleriyle saklanır.

### 📡 4. SignalR ile Canlı Sipariş ve Teslimat Takibi

Ödeme sonrasında sipariş, personelin takip edebildiği teslimat sürecine geçer. Müşteri ve personel ekranları aynı siparişin güncel durumunu izler.

- **Anlık Sipariş Bildirimi:** Ödenmiş sipariş bilgisi SignalR üzerinden personel grubuna iletilir.
- **Aşamalı Teslimat:** Siparişler **Bekliyor → Yolda → Teslim edildi** durumlarıyla takip edilir. Teslimat ilerletme işlemi için ödemenin tamamlanmış olması gerekir.
- **Müşteriye Özel Takip:** Kullanıcı, kendisine ait siparişin bildirim grubuna katılarak durum değişikliklerini izleyebilir. Gruba katılmadan önce sipariş sahipliği kontrol edilir.
- **İşlem Geçmişi ve Geri Alma:** Yapılan teslimat işlemleri kaydedilir. Garson son adımı beş dakika içinde geri alabilir; admin için bu süre kısıtı uygulanmaz. Geri alma işlemi de geçmişe ve bildirimlere yansır.

### 🔐 5. Identity ile Hesap Doğrulama ve Şifre Kurtarma

Hesap işlemleri ASP.NET Core Identity üzerinden yürütülür; kullanıcıların erişebildiği ekranlar rollerine göre ayrılır.

- **E-posta ile Hesap Doğrulama:** Kayıt sonrasında ASP.NET Core Identity ile doğrulama kodu üretilir ve kullanıcının e-posta adresine gönderilir. Kod doğrulandığında hesabın `EmailConfirmed` bilgisi güncellenir.
- **Doğrulanmadan Giriş Yapılamaması:** E-posta ve şifreyle girişte, şifre doğru olsa bile e-posta doğrulanmamışsa oturum açılmaz. Kullanıcıya yeniden doğrulama kodu gönderilir; doğrulama tamamlandıktan sonra giriş yapılabilir.
- **Şifremi Unuttum:** Identity tarafından kullanıcıya özel şifre sıfırlama token'ı üretilir. Token, URL'ye uygun biçimde kodlanarak e-posta bilgisiyle birlikte özel şifre yenileme bağlantısına eklenir ve e-posta ile iletilir.
- **Bağlantı Üzerinden Şifre Yenileme:** Kullanıcı e-postadaki bağlantıdan yeni şifresini belirler. Token doğrulaması ve şifre güncellemesi `ResetPasswordAsync` üzerinden gerçekleştirilir; işlem başarılıysa kullanıcı giriş ekranına yönlendirilir.
- **Google ile Giriş:** Google hesabıyla oturum açma desteklenir. Mevcut Google giriş akışında e-posta doğrulanmış kabul edilerek hesap oluşturulur veya mevcut hesapla ilişkilendirilir.
- **Rol Bazlı Erişim:** `Admin`, `Writer`, `User` ve `Waiter` alanları farklı sorumluluklara göre düzenlenmiştir. Oturum yönetiminde cookie tabanlı kimlik doğrulama kullanılır.
- **Hesap Kontrolleri:** Tekil e-posta, şifre kuralları ve başarısız giriş denemelerinde hesap kilitleme ayarları uygulanır.

### ☁️ 6. Amazon S3 ile Görsel Depolama

Yüklenen görseller, uygulamanın dosya servisi üzerinden Amazon S3 üzerinde yönetilir.

- **Merkezi Yükleme:** Görseller S3 bucket'ına aktarılır ve oluşan adres ilgili kayıtta kullanılır.
- **Benzersiz Dosya Adları:** Yükleme sırasında dosya adına benzersiz bir kimlik eklenir.
- **URL Üzerinden Sunum:** Arayüzde kullanılan görseller S3 adresleri üzerinden görüntülenir.
- **Dosya Yönetimi:** Yükleme ve silme işlemleri `IFileService` arayüzü üzerinden ortak bir serviste toplanır.

### 📬 7. E-posta Bildirimleri ve PDF Sipariş Belgesi

Hesap ve sipariş süreçleri, kullanıcıya gönderilen bilgilendirmelerle desteklenir.

- **Hesap E-postaları:** Identity ile üretilen doğrulama kodu ve kişiye özel şifre yenileme bağlantısı, MailKit/MimeKit tabanlı e-posta servisiyle gönderilir.
- **Sipariş Onayı:** Başarılı ödeme sonrasında sipariş bilgilerini içeren e-posta gönderilir. Temel adres yapılandırıldığında e-postaya belge bağlantısı da eklenir.
- **Dinamik PDF:** QuestPDF ile sipariş bilgilerini içeren PDF çıktısı oluşturulur; belge tarayıcıda görüntülenebilir veya indirilebilir.
- **Siparişe Bağlı Erişim:** Kişisel PDF sorgusunda kullanıcı sahipliği ve siparişin ödenmiş olması kontrol edilir.

### 📊 8. Satış Analizleri ve İçerik Yönetimi

Yönetim paneli, işletmenin sipariş hareketlerini ve sitede yayınlanan içerikleri bir arada yönetmesine olanak tanır.

- **Dönemsel Satış Analizi:** 7, 30, 90 ve 365 günlük dönemler için satış tutarı, ortalama sepet ve sipariş durumları görüntülenir; önceki dönemle karşılaştırma yapılır.
- **Ürün ve Kategori Performansı:** Günlük satışlar, saatlik sipariş dağılımı, kategori gelirleri, en çok ve en az satan ürünler incelenebilir.
- **Ödeme ve Kupon Görünümü:** Tek çekim/taksit dağılımı, kart aileleri ve kupon kullanımı raporlanır.
- **Dinamik İçerikler:** Blog, yorum, banner, tanıtım, işletme geçmişi, referans, müşteri görüşü ve iletişim içerikleri yönetilebilir. Yazarlar için ayrı içerik ekranları bulunur.

---

## 🏗️ Mimari ve Tasarım Yaklaşımı

MyAcademy_Bagery, **tek bir ASP.NET Core MVC projesi içinde sorumlulukları ayrıştırılmış** bir yapıya sahiptir. Sunum tarafı Controller, Razor View ve View Component yapılarıyla; uygulama işlemleri ise MediatR komutları, sorguları ve handler'larıyla düzenlenmiştir.

| Yapı / Konsept | Projedeki Kullanımı |
| :--- | :--- |
| **Web Uygulaması** | .NET 9, ASP.NET Core MVC, Razor Views ve View Components |
| **Komut ve Sorgu Ayrımı** | MediatR ile Command / Query / Handler organizasyonu |
| **Veri Erişimi** | EF Core 9, Npgsql ve PostgreSQL |
| **Veri İşlemleri** | Generic Repository ve Unit of Work |
| **Doğrulama** | FluentValidation ile ayrı validator sınıfları |
| **Nesne Eşleme** | Mapster ile model ve entity dönüşümleri |
| **Bağımlılık Yönetimi** | Dependency Injection ve Scrutor ile repository kayıtları |
| **Veri Yaşam Döngüsü** | AuditDbContextInterceptor, zaman damgaları ve soft delete |
| **Hata İşleme** | Özel exception türleri ve ortak ExceptionFilter |
| **Kimlik ve Yetki** | ASP.NET Core Identity, cookie oturumu ve Google Authentication |
| **Arayüz Bileşenleri** | Bootstrap, jQuery, SweetAlert2 ve ApexCharts |

### 📨 MediatR ile Komut ve Sorgu Ayrımı

Controller'lardan gelen istekler, ilgili komut veya sorgu nesnesiyle handler'a aktarılır. Veri değiştiren işlemler `Commands`, okuma işlemleri `Queries`, bunların uygulamaları `Handlers` altında toplanır. Bu düzen, bir işlevin doğrulama ve iş kurallarının nerede yürütüldüğünü takip etmeyi kolaylaştırır.

### 🗃️ Generic Repository ve Unit of Work

Ortak ekleme, okuma, güncelleme ve silme işlemleri Generic Repository üzerinden sunulur. Modüle özgü sorgular ilgili repository'lerde yer alır. Unit of Work, aynı `AppDbContext` üzerinde biriken değişiklikleri `SaveChangesAsync` çağrısıyla kaydeder. Bazı raporlama handler'ları da doğrudan `AppDbContext` üzerinden sorgu çalıştırır.

### 🛡️ Interceptor ile Kayıt Takibi ve Soft Delete

`AuditDbContextInterceptor`, `BaseEntity` türevi kayıtların eklenme ve güncellenme zamanlarını yönetir. Silme işlemleri bu kayıtlarda `IsDeleted` alanını güncelleyecek şekilde dönüştürülür. `AppDbContext` üzerindeki global sorgu filtresi, silinmiş kayıtları normal sorgu sonuçlarından çıkarır.

### ✅ FluentValidation ve Merkezi Hata İşleme

Form ve işlem doğrulamaları validator sınıflarında tanımlanır. `ExceptionFilter`, tanımlı doğrulama, Identity ve iş kuralı hatalarını yakalar; normal sayfa isteklerinde `ModelState` üzerinden, AJAX olarak işaretlenen isteklerde JSON yanıtıyla kullanıcıya iletir.

### ⚙️ Scrutor ve Mapster

Scrutor, assembly içindeki repository sınıflarını tarayarak arayüzleri üzerinden dependency injection yapısına kaydeder. Mapster ise model–entity eşlemelerinde tekrarlanan alan atamalarını azaltır. Diğer uygulama servisleri servis kayıt uzantılarında tanımlanır.

---

## 👥 Kullanıcı Rolleri ve Yönetim Alanları

| Rol / Alan | Yetki ve İşlevler |
| :--- | :--- |
| **Ziyaretçi** | Ürün ve blog inceleme, sepet oluşturma, iletişim ve hesap ekranlarına erişim. |
| **User** | Profil ve şifre yönetimi, kendi siparişlerini görüntüleme, teslimat takibi ve ödenmiş siparişin PDF çıktısına erişim. |
| **Writer** | Kendi blog ve yorumlarını yönetme; yazar paneli, profil ve kişisel sipariş ekranlarını kullanma. |
| **Waiter** | Teslimat panosunu izleme, sipariş durumunu ilerletme ve izin verilen süre içinde son adımı geri alma. |
| **Admin** | Ürün, kategori, varyant, kullanıcı, rol, içerik, kupon, taksit, sipariş ve teslimat yönetimi; satış panolarına erişim. |

Ödeme için oturum açılması gerekir. Blog yorumu oluşturma işlemi **Admin** ve **Writer** rollerine açıktır. Kişisel sipariş ve belge erişimleri kullanıcı sahipliğine göre kontrol edilir.

---

## 🔌 Entegrasyonlar

| Entegrasyon | Uygulamaya Katkısı |
| :--- | :--- |
| **OpenAI** | Menüye dayalı dijital barista ve yayın öncesi yorum moderasyonu. |
| **PayTR** | Kartla ödeme, kart bilgisi sorgulama, taksit işlemleri ve ödeme sonucu bildirimi. |
| **SignalR** | Ödenmiş sipariş ve teslimat değişikliklerinin bağlı ekranlara iletilmesi. |
| **Amazon S3** | Görsellerin bulutta saklanması ve URL üzerinden sunulması. |
| **Google Authentication** | Google hesabıyla oturum açılması. |
| **Gmail SMTP / MailKit** | Identity doğrulama kodu, kullanıcıya özel şifre yenileme bağlantısı ve sipariş e-postaları. |
| **QuestPDF** | Sipariş verilerinden görüntülenebilir ve indirilebilir PDF oluşturulması. |

---

## 🔄 Mimari ve İş Akışları

Diyagramlar süreçleri ayrı ve kısa akışlar halinde gösterir. Hareketli bağlantılar işlem yönünü vurgular; gerçek zamanlı uygulama verisini temsil etmez.

<!-- Uyumluluk: Bu diyagramlar Mermaid edge ID ve animation özelliklerini kullanır. Destek, GitHub tarafından kullanılan Mermaid sürümüne bağlıdır. Eski sürümde syntax hatası alınırsa e1@ benzeri edge ID öneklerini ve animation satırlarını kaldırarak statik görünüm kullanılabilir. -->

### 01 · Uygulama Mimarisi

Sunum, iş kuralları ve veri erişimi aynı MVC uygulaması içinde ayrı sorumluluklarla düzenlenmiştir.

```mermaid
flowchart TB
    UI["Müşteri ve Yönetim Panelleri<br/>Razor Views · MVC Areas"]
    MVC["Controller'lar"]
    HANDLER["MediatR<br/>Komut · Sorgu · İş kuralları"]
    REPO["Veri Erişimi<br/>Repository · Unit of Work"]
    DB[("EF Core · PostgreSQL")]
    SERVICE["Uygulama Servisleri"]
    EXTERNAL["PayTR · OpenAI · S3 · SMTP"]
    UI a1@--> MVC
    MVC a2@--> HANDLER
    HANDLER a3@--> REPO
    REPO a4@--> DB
    HANDLER a5@--> SERVICE
    SERVICE a6@--> EXTERNAL
    class UI,MVC entry;
    class HANDLER,SERVICE process;
    class REPO,DB success;
    class EXTERNAL decision;

    classDef entry fill:#EFF6FF,stroke:#3B82F6,color:#172554,stroke-width:1.5px;
    classDef process fill:#F5F3FF,stroke:#8B5CF6,color:#2E1065,stroke-width:1.5px;
    classDef success fill:#ECFDF5,stroke:#10B981,color:#064E3B,stroke-width:1.5px;
    classDef decision fill:#FFF7ED,stroke:#F59E0B,color:#78350F,stroke-width:1.5px;
    classDef failure fill:#FFF1F2,stroke:#F43F5E,color:#881337,stroke-width:1.5px;

    a1@{ animation: slow }
    a2@{ animation: slow }
    a3@{ animation: slow }
    a4@{ animation: slow }
    a5@{ animation: slow }
    a6@{ animation: slow }
```

Bu şema ana istek yolunu özetler. Bazı raporlama handler’ları doğrudan AppDbContext kullanır; dijital barista controller’ı kendi servisine erişir. Identity oturum ve yetkilendirmeyi, SignalR canlı bildirimleri yönetir.


### 04 · Sepet ve Ödeme

Sepet kontrolünden ödeme sonucuna kadar ana akış aşağıdadır. Ödeme durumu PayTR’nin sunucu bildirimiyle kesinleşir.

```mermaid
flowchart TB
    A["Sepet<br/>Ürün · Varyant · Kupon"]
    B["Ödeme Kontrolleri<br/>Oturum · Form · Fiyat · Taksit"]
    C["PayTR İsteği<br/>Kabul sonrası Pending sipariş"]
    D["3D Doğrulama ve Callback<br/>Hash · Sipariş durumu kontrolü"]
    E{"Ödeme sonucu"}
    F["Paid<br/>Personel bildirimi ve e-posta"]
    G["Failed<br/>Hata bilgisini kaydet"]
    A d1@--> B
    B d2@--> C
    C d3@--> D
    D d4@--> E
    E d5@-->|"Başarılı"| F
    E d6@-->|"Başarısız"| G
    class A entry;
    class B,C,D process;
    class E decision;
    class F success;
    class G failure;

    classDef entry fill:#EFF6FF,stroke:#3B82F6,color:#172554,stroke-width:1.5px;
    classDef process fill:#F5F3FF,stroke:#8B5CF6,color:#2E1065,stroke-width:1.5px;
    classDef success fill:#ECFDF5,stroke:#10B981,color:#064E3B,stroke-width:1.5px;
    classDef decision fill:#FFF7ED,stroke:#F59E0B,color:#78350F,stroke-width:1.5px;
    classDef failure fill:#FFF1F2,stroke:#F43F5E,color:#881337,stroke-width:1.5px;

    d1@{ animation: slow }
    d2@{ animation: slow }
    d3@{ animation: slow }
    d4@{ animation: slow }
    d5@{ animation: slow }
    d6@{ animation: slow }
```

Geçersiz hash içeren bildirim ödeme durumunu değiştirmez. Daha önce işlenmiş sipariş yeniden işlenmez. Tarayıcının sonuç sayfasına dönüşü callback’ten ayrı gerçekleşir. Sepet ödeme başlatılırken korunur; başarı dönüşünde sipariş bulunur ve durumu Failed değilse temizlenir.

### 05 · Teslimat ve Canlı Bildirim

Ödenmiş siparişler üç teslimat durumuyla izlenir. Her durum değişikliği kaydedildikten sonra ilgili ekranlara bildirilir.

```mermaid
flowchart LR
    A["Bekliyor"]
    B["Yolda"]
    C["Teslim edildi"]
    D["İşlem geçmişi<br/>ve durum kaydı"]
    E["SignalR<br/>Personel ve müşteri"]
    A e1@--> B
    B e2@--> C
    B e3@--> D
    C e4@--> D
    D e5@--> E
    class A decision;
    class B entry;
    class C success;
    class D,E process;

    classDef entry fill:#EFF6FF,stroke:#3B82F6,color:#172554,stroke-width:1.5px;
    classDef process fill:#F5F3FF,stroke:#8B5CF6,color:#2E1065,stroke-width:1.5px;
    classDef success fill:#ECFDF5,stroke:#10B981,color:#064E3B,stroke-width:1.5px;
    classDef decision fill:#FFF7ED,stroke:#F59E0B,color:#78350F,stroke-width:1.5px;
    classDef failure fill:#FFF1F2,stroke:#F43F5E,color:#881337,stroke-width:1.5px;

    e1@{ animation: slow }
    e2@{ animation: slow }
    e3@{ animation: slow }
    e4@{ animation: slow }
    e5@{ animation: slow }
```

Teslimat ilerletme yalnızca Paid siparişlerde yapılır. Garson son adımı 5 dakika içinde geri alabilir; admin için süre sınırı uygulanmaz. Geri alma da kaydedilir ve yayınlanır. Müşteri bildirim grubuna katılırken sipariş sahipliği kontrol edilir.

### 📂 Proje Organizasyonu

<details>
<summary><b>Klasör Yapısını Görmek İçin Tıklayın</b></summary>

```text
MyAcademy_Bagery/
└── Bagery.WebUI/
    ├── Areas/                 # Admin, Writer, User ve Waiter alanları
    ├── Controllers/           # Genel site, hesap, sepet ve ödeme uç noktaları
    ├── Context/               # EF Core ve Identity veritabanı bağlamı
    ├── Entities/              # Veritabanı varlıkları
    ├── Enums/                 # Ödeme, teslimat ve mesaj durumları
    ├── Exceptions/            # Uygulamaya özgü hata türleri
    ├── Extensions/            # Servis kayıtları ve yardımcı genişletmeler
    ├── Filters/               # Ortak hata işleme
    ├── Hubs/                  # SignalR OrderHub
    ├── IdentityValidations/   # Identity hata mesajları
    ├── Interceptors/          # Audit ve soft delete işlemleri
    ├── MediatorPattern/       # Commands, Queries, Handlers ve Results
    ├── Migrations/            # Veritabanı şema değişiklikleri
    ├── Models/                # Sepet, form ve yardımcı modeller
    ├── Repositories/          # Veri erişim arayüzleri ve uygulamaları
    ├── Services/              # Ödeme, e-posta, dosya, PDF, AI ve bildirimler
    ├── UOW/                   # Unit of Work
    ├── Validators/            # FluentValidation kuralları
    ├── ViewComponents/        # Tekrar kullanılabilir arayüz bileşenleri
    ├── Views/                 # Razor sayfaları ve ortak yerleşimler
    ├── wwwroot/               # Tema dosyaları ve statik varlıklar
    └── Program.cs             # Uygulamanın başlangıç ve yönlendirme ayarları
```

</details>

---

## 📸 Ekran Görüntüleri ve Kullanım Senaryoları




##ana sayfa
<img width="1886" height="946" alt="Ekran görüntüsü 2026-09-24 120822" src="https://github.com/user-attachments/assets/68b9cd7c-6af3-4c96-b307-2b9a6d1e27c7" />
<img width="1893" height="948" alt="Ekran görüntüsü 2026-09-24 120836" src="https://github.com/user-attachments/assets/eb64eb14-2d52-4b04-9df1-ff4ef2e40a24" />
<img width="1888" height="946" alt="Ekran görüntüsü 2026-09-24 120843" src="https://github.com/user-attachments/assets/80e9389a-2c1f-46c1-bc6e-1d7a0021233d" />
<img width="1882" height="948" alt="Ekran görüntüsü 2026-09-24 120852" src="https://github.com/user-attachments/assets/67f429fb-15cc-47b8-b82e-9249542e16ef" />


<img width="1895" height="943" alt="Ekran görüntüsü 2026-09-24 120917" src="https://github.com/user-attachments/assets/ffc6849f-1f85-4d83-ab20-94ed0752987e" />


<img width="1891" height="945" alt="Ekran görüntüsü 2026-09-24 120932" src="https://github.com/user-attachments/assets/ef71cdd2-88bb-4013-992f-14f7c3f0d526" />

<img width="1890" height="947" alt="Ekran görüntüsü 2026-09-24 120941" src="https://github.com/user-attachments/assets/80651a49-5c4d-4969-8d8a-54fa0ff382d0" />

<img width="1888" height="942" alt="Ekran görüntüsü 2026-09-24 120951" src="https://github.com/user-attachments/assets/6edeac7d-73ed-4b76-a8e1-9dcf099ca9e3" />
<img width="1895" height="950" alt="Ekran görüntüsü 2026-09-24 120959" src="https://github.com/user-attachments/assets/c92acfc5-03cc-4e3d-b909-02db7061fa93" />

#Blog yazıları
<img width="1888" height="946" alt="Ekran görüntüsü 2026-09-24 121656" src="https://github.com/user-attachments/assets/80696e2d-1b56-421b-8acb-86bbdc646c7a" />


<img width="1890" height="943" alt="Ekran görüntüsü 2026-09-24 121702" src="https://github.com/user-attachments/assets/fb7f7baa-62fd-41cd-a795-eaf4b77d0b67" />

<img width="1885" height="945" alt="Ekran görüntüsü 2026-09-24 121709" src="https://github.com/user-attachments/assets/e1be59ef-4816-409d-b51c-ae87e3f27835" />
<img width="1891" height="958" alt="Ekran görüntüsü 2026-09-24 121718" src="https://github.com/user-attachments/assets/d19f0304-6908-42da-8645-466e1dc53ee1" />



blog yazabılmek ıcıınvgırıs gereklı
<img width="1890" height="951" alt="Ekran görüntüsü 2026-09-24 121752" src="https://github.com/user-attachments/assets/e33388b0-c625-4780-9369-276c24310d2f" />

<img width="1887" height="948" alt="Ekran görüntüsü 2026-09-24 122005" src="https://github.com/user-attachments/assets/662a4c14-1b21-45c5-9638-4eca15bf5274" />

open ai astra yorum analizi
<img width="1887" height="946" alt="Ekran görüntüsü 2026-09-24 122045" src="https://github.com/user-attachments/assets/b9a5cebe-17f6-47bc-911b-732ec2b963c3" />
<img width="1885" height="937" alt="Ekran görüntüsü 2026-09-24 122014" src="https://github.com/user-attachments/assets/c56ce187-ac2a-4c1b-a7ef-620e55a58e40" />



#open ai astra  kafe akıllı baristasi 
<img width="1903" height="943" alt="Ekran görüntüsü 2026-09-24 122127" src="https://github.com/user-attachments/assets/dc315cbf-7140-4acd-a192-7c73935856a3" />
<img width="1901" height="942" alt="Ekran görüntüsü 2026-09-24 122141" src="https://github.com/user-attachments/assets/8bd26448-8aff-49c6-ad33-e2d735c02438" />

<img width="1885" height="942" alt="Ekran görüntüsü 2026-09-24 122224" src="https://github.com/user-attachments/assets/91216c09-c7fe-4d53-9a20-bc7e58974621" />

<img width="1900" height="948" alt="Ekran görüntüsü 2026-09-24 122304" src="https://github.com/user-attachments/assets/60acdf4e-1b87-4552-b16d-71487ad9a408" />

#iletişim
<img width="1885" height="942" alt="Ekran görüntüsü 2026-09-24 122322" src="https://github.com/user-attachments/assets/e06257c0-be42-4c20-847d-ef361c5aa2a8" />



<img width="1900" height="948" alt="Ekran görüntüsü 2026-09-24 122332" src="https://github.com/user-attachments/assets/c8335ac1-aeea-46b2-a808-8a2138cd013f" />

<img width="1892" height="942" alt="Ekran görüntüsü 2026-09-24 122612" src="https://github.com/user-attachments/assets/6d0abf17-c4c4-44e8-8156-672a8d006807" />

<img width="1887" height="940" alt="Ekran görüntüsü 2026-09-24 122645" src="https://github.com/user-attachments/assets/94e7619b-1d62-46c5-933d-2a4028e3cead" />

eposta spam için kod gönderme mantığı
<img width="1887" height="945" alt="Ekran görüntüsü 2026-09-24 122657" src="https://github.com/user-attachments/assets/df6c765c-7d2a-467f-b23b-818bc39d901e" />

<img width="1888" height="938" alt="Ekran görüntüsü 2026-09-24 122710" src="https://github.com/user-attachments/assets/eb51e8bc-65c1-4f0e-8aed-dfac04083c0f" />
<img width="1883" height="945" alt="Ekran görüntüsü 2026-09-24 122724" src="https://github.com/user-attachments/assets/341fdad7-54f1-4a29-984a-068071c3cfa8" />

admim panleınde gelen mesajlar
<img width="1897" height="963" alt="Ekran görüntüsü 2026-09-24 122757" src="https://github.com/user-attachments/assets/9f39ba9b-536d-4232-8aee-b30470df63d8" />

<img width="1908" height="902" alt="Ekran görüntüsü 2026-09-24 122806" src="https://github.com/user-attachments/assets/b7830d8c-d552-4b21-a1ea-d50a37a79722" />
<img width="1910" height="940" alt="Ekran görüntüsü 2026-09-24 122814" src="https://github.com/user-attachments/assets/20c33886-7ee0-49c1-a590-5c793c95c15c" />




# giriş ve kayıt ol , Google ile giriş ayfyalı
<img width="1900" height="950" alt="Ekran görüntüsü 2026-09-24 123736" src="https://github.com/user-attachments/assets/18ccd8ac-e7d0-4575-a6be-96468b07d6d0" />
<img width="1885" height="956" alt="Ekran görüntüsü 2026-09-24 124117" src="https://github.com/user-attachments/assets/fd6eae75-7d1f-4d5a-89e9-eb49d299fd79" />
<img width="1895" height="942" alt="Ekran görüntüsü 2026-09-24 124306" src="https://github.com/user-attachments/assets/9add6cc4-8436-4ec1-9cac-3eefeefdc936" />

<img width="1891" height="952" alt="Ekran görüntüsü 2026-09-24 124355" src="https://github.com/user-attachments/assets/738b80ec-3995-4197-83ea-eed5a6656dd1" />
<img width="1892" height="950" alt="Ekran görüntüsü 2026-09-24 124400" src="https://github.com/user-attachments/assets/801fe38f-4981-4083-b80d-e402ec627f8a" />



şifremş unuttum 
<img width="1902" height="948" alt="Ekran görüntüsü 2026-09-24 124500" src="https://github.com/user-attachments/assets/9abf13a8-60cb-4e65-b5d8-4c9d8416535d" />
<img width="1897" height="953" alt="Ekran görüntüsü 2026-09-24 124536" src="https://github.com/user-attachments/assets/c96dc1d1-6ea7-41a5-9f90-8af7f71a0e1a" />

<img width="692" height="498" alt="Ekran görüntüsü 2026-09-24 124609" src="https://github.com/user-attachments/assets/3a457fb9-00f4-4c99-b949-9d4cddd6bf85" />
<img width="1895" height="953" alt="Ekran görüntüsü 2026-09-24 124618" src="https://github.com/user-attachments/assets/ff0a89d3-d973-45c7-b02e-eb2cba42bd9a" />
<img width="1900" height="943" alt="Ekran görüntüsü 2026-09-24 124637" src="https://github.com/user-attachments/assets/0f7504f6-66a4-479a-8845-e281b957beb3" />



#ürünler ve sepet sayfası

<img width="1882" height="945" alt="Ekran görüntüsü 2026-09-24 124914" src="https://github.com/user-attachments/assets/b1f09c25-9903-479f-a00d-568eb00c204b" />

<img width="1891" height="942" alt="Ekran görüntüsü 2026-09-24 124920" src="https://github.com/user-attachments/assets/90cd3ef1-e924-451f-9ebc-670f7655711f" />
<img width="1892" height="946" alt="Ekran görüntüsü 2026-09-24 124928" src="https://github.com/user-attachments/assets/042d1d97-392f-4a9e-a981-7d8e2109cdf7" />

filteleme mantığı 
<img width="1887" height="947" alt="Ekran görüntüsü 2026-09-24 124947" src="https://github.com/user-attachments/assets/6508d6ed-050a-451d-8538-28ccd50c3dd4" />

ürü detayı 
<img width="1891" height="948" alt="Ekran görüntüsü 2026-09-24 125004" src="https://github.com/user-attachments/assets/b40c74d2-5f5b-4fa4-a8c0-698b855a551b" />

<img width="1886" height="946" alt="Ekran görüntüsü 2026-09-24 125010" src="https://github.com/user-attachments/assets/356562e2-c86b-421a-861a-f0243b69b4cf" />

sepete ekleme 
<img width="1892" height="953" alt="Ekran görüntüsü 2026-09-24 125025" src="https://github.com/user-attachments/assets/a2bd496a-eede-443b-95f6-992c4ee92cca" />
<img width="1883" height="947" alt="Ekran görüntüsü 2026-09-24 125043" src="https://github.com/user-attachments/assets/0df7163d-cd89-470a-a27a-31b15c8e56ef" />

<img width="1896" height="922" alt="Ekran görüntüsü 2026-09-24 125049" src="https://github.com/user-attachments/assets/835fbb26-35fa-4dbb-88b1-de7f5725f2bf" />

kupon ekleme ve hataları görme
<img width="1887" height="947" alt="Ekran görüntüsü 2026-09-24 125100" src="https://github.com/user-attachments/assets/28def7c7-5ed7-4eaf-a143-0abad2c38c43" />

<img width="1891" height="953" alt="Ekran görüntüsü 2026-09-24 125109" src="https://github.com/user-attachments/assets/5af0ca15-9297-47ae-b8b5-63df41413e5b" />


<img width="1890" height="923" alt="Ekran görüntüsü 2026-09-24 130620" src="https://github.com/user-attachments/assets/5c14d553-8907-44c1-835c-079ccfbaa127" />

siapriş için kişinin sisteme girmesi gerekir
<img width="1897" height="945" alt="Ekran görüntüsü 2026-09-24 125123" src="https://github.com/user-attachments/assets/0b7f71c2-58bd-49dd-9109-0e92e132e6b9" />


#PAYTR ENTEGRASYONU

<img width="1895" height="961" alt="Ekran görüntüsü 2026-09-24 125140" src="https://github.com/user-attachments/assets/d30910f3-d6e6-427d-bd1b-0e0ce356bafa" />
<img width="1893" height="942" alt="Ekran görüntüsü 2026-09-24 125354" src="https://github.com/user-attachments/assets/8791f0c8-60be-4f18-b949-bb0f3336db9d" />
<img width="1886" height="945" alt="Ekran görüntüsü 2026-09-24 125328" src="https://github.com/user-attachments/assets/5266412f-b590-465c-b33c-9af6d07dea7e" />
<img width="1898" height="410" alt="Ekran görüntüsü 2026-09-24 125433" src="https://github.com/user-attachments/assets/4ceb0c89-716c-4e45-a43e-b27718666ad0" />
<img width="1887" height="952" alt="Ekran görüntüsü 2026-09-24 125445" src="https://github.com/user-attachments/assets/908fa325-f299-410a-b052-ed59fded31f5" />

<img width="1892" height="952" alt="Ekran görüntüsü 2026-09-24 125456" src="https://github.com/user-attachments/assets/84087032-cd0c-4e28-bb05-752ac611dba4" />
<img width="1867" height="952" alt="Ekran görüntüsü 2026-09-24 130827" src="https://github.com/user-attachments/assets/e0f6c960-0e11-4aa7-a6d6-6d4fe8315624" />
<img width="1857" height="933" alt="Ekran görüntüsü 2026-09-24 130912" src="https://github.com/user-attachments/assets/3190ccff-1dac-4c60-b0e4-be1f60ce1854" />



#sİGNALr ENTEGRASYONU VE GARSON PANELİ
<img width="1910" height="978" alt="Ekran görüntüsü 2026-09-24 130647" src="https://github.com/user-attachments/assets/2534ddbb-7495-4429-b621-952b2e165ff2" />
<img width="1896" height="946" alt="Ekran görüntüsü 2026-09-24 130704" src="https://github.com/user-attachments/assets/44ae0635-4cb2-41c3-9e60-301038c9e4f5" />
<img width="1883" height="952" alt="Ekran görüntüsü 2026-09-24 130737" src="https://github.com/user-attachments/assets/2e3eea2b-4946-4521-862f-89a31929fc5b" />
<img width="1897" height="945" alt="Ekran görüntüsü 2026-09-24 130719" src="https://github.com/user-attachments/assets/c51ed65e-5e53-49b1-961e-3e144ba69d21" />



#ADMİN PANELİ

<img width="1887" height="935" alt="Ekran görüntüsü 2026-09-24 131011" src="https://github.com/user-attachments/assets/ebf248a6-fb94-41d5-bd2b-b33be672b588" />
<img width="1882" height="942" alt="Ekran görüntüsü 2026-09-24 131023" src="https://github.com/user-attachments/assets/4da23d06-62ef-43d8-a728-98f165c2ffd7" />
<img width="1888" height="947" alt="Ekran görüntüsü 2026-09-24 131030" src="https://github.com/user-attachments/assets/a0f6e384-f898-4951-911c-15edf9a27768" />
<img width="1896" height="947" alt="Ekran görüntüsü 2026-09-24 131051" src="https://github.com/user-attachments/assets/bc44a1e3-51b6-4b23-9bf8-57160500964d" />


<img width="1890" height="945" alt="Ekran görüntüsü 2026-09-24 131058" src="https://github.com/user-attachments/assets/c334208c-82b1-4898-8229-ff62b76dd6e6" />
<img width="1887" height="945" alt="Ekran görüntüsü 2026-09-24 131105" src="https://github.com/user-attachments/assets/ff21a365-473c-4283-85d7-17d1bc33f6ee" />
<img width="1892" height="957" alt="Ekran görüntüsü 2026-09-24 131134" src="https://github.com/user-attachments/assets/3d6683bd-74ad-45ad-a670-460417bab266" />



BANNER
<img width="1905" height="942" alt="Ekran görüntüsü 2026-09-24 131145" src="https://github.com/user-attachments/assets/4c9b3f02-220a-41fc-83f3-4ccc98db45fa" />
KATEGORİLER
<img width="1901" height="942" alt="Ekran görüntüsü 2026-09-24 131200" src="https://github.com/user-attachments/assets/2b980409-3c03-4145-800f-a4a877eccc08" />
<img width="1910" height="941" alt="Ekran görüntüsü 2026-09-24 131155" src="https://github.com/user-attachments/assets/b82c8478-ce75-48c5-9b9d-fc9341d7b5a2" />
ÜRÜNLER
<img width="1892" height="943" alt="Ekran görüntüsü 2026-09-24 131210" src="https://github.com/user-attachments/assets/7ca9d248-58d2-4b63-91b1-cea33cc4b614" />
<img width="1893" height="940" alt="Ekran görüntüsü 2026-09-24 131220" src="https://github.com/user-attachments/assets/e7feecc7-cebb-464c-adce-4be318e01055" />
<img width="1892" height="942" alt="Ekran görüntüsü 2026-09-24 131230" src="https://github.com/user-attachments/assets/75ee1708-d6cb-4493-bf09-621c7af7c551" />
<img width="1887" height="950" alt="Ekran görüntüsü 2026-09-24 131239" src="https://github.com/user-attachments/assets/15c07311-d34b-49a8-83b0-4a4c4934955c" />
<img width="1891" height="946" alt="Ekran görüntüsü 2026-09-24 131245" src="https://github.com/user-attachments/assets/c95035c2-caf4-4865-bbf5-b46808122beb" />
MARKALRIMIZ
<img width="1910" height="945" alt="Ekran görüntüsü 2026-09-24 131316" src="https://github.com/user-attachments/assets/70c68b95-28a5-401d-9dbd-0670e3ee7c0e" />

PROMOSYON VE KUPONLAR
<img width="1906" height="945" alt="Ekran görüntüsü 2026-09-24 131324" src="https://github.com/user-attachments/assets/d3d9718d-a36e-4f6a-9e01-498939f927fb" />
<img width="1906" height="945" alt="Ekran görüntüsü 2026-09-24 131441" src="https://github.com/user-attachments/assets/61bd1463-a75f-4b6a-8402-7754f6291689" />
<img width="1892" height="937" alt="Ekran görüntüsü 2026-09-24 131632" src="https://github.com/user-attachments/assets/18cc911a-1338-49ac-af82-46ba366f62af" />
<img width="535" height="275" alt="Ekran görüntüsü 2026-09-24 131701" src="https://github.com/user-attachments/assets/39df7907-4acb-4432-bdd4-6913f8a144e3" />

TARİHÇEMİZ
<img width="1901" height="940" alt="Ekran görüntüsü 2026-09-24 131712" src="https://github.com/user-attachments/assets/6084af64-8e69-403b-b729-4c4e877e28a1" />
YORUM YAPNALAR
<img width="1901" height="941" alt="Ekran görüntüsü 2026-09-24 131717" src="https://github.com/user-attachments/assets/3b26614a-f86e-41be-a142-9b174b74f275" />
BLOGLAR VE BLOGLARIM

<img width="1897" height="942" alt="Ekran görüntüsü 2026-09-24 131723" src="https://github.com/user-attachments/assets/28df6ed4-0bcf-462a-9369-6b17255d7c90" />
<img width="1907" height="942" alt="Ekran görüntüsü 2026-09-24 131729" src="https://github.com/user-attachments/assets/8e84eb77-7a06-4a74-a4b3-6c84dc0c419d" />
<img width="1912" height="952" alt="Ekran görüntüsü 2026-09-24 131739" src="https://github.com/user-attachments/assets/38081834-0c58-4f41-83b0-cf3b2a4a753d" />
YORUMLAR 
<img width="1888" height="936" alt="Ekran görüntüsü 2026-09-24 131747" src="https://github.com/user-attachments/assets/bea54b2a-7ef8-49b1-a6e2-b7135bae46e0" />
İLETİŞİM BİLGİKARI
<img width="1907" height="945" alt="Ekran görüntüsü 2026-09-24 131756" src="https://github.com/user-attachments/assets/d5a84a3a-d4d9-4883-8b3f-754691f65289" />
<img width="1902" height="935" alt="Ekran görüntüsü 2026-09-24 131802" src="https://github.com/user-attachments/assets/0bd12c34-fdb1-420f-95dd-39d375637ddd" />

SİPARİŞLER
<img width="1896" height="945" alt="Ekran görüntüsü 2026-09-24 131814" src="https://github.com/user-attachments/assets/5fcf6840-01f8-47cc-820a-b4603bd388d9" />
<img width="1908" height="940" alt="Ekran görüntüsü 2026-09-24 131822" src="https://github.com/user-attachments/assets/78b0bab9-f9fc-4562-8005-cdad476c157e" />

<img width="1887" height="942" alt="Ekran görüntüsü 2026-09-24 131836" src="https://github.com/user-attachments/assets/6675eedb-ed41-47be-8d5e-5078665abcd8" />


































## ⚙️ Kurulum

<details>
<summary><b>Gereksinimleri ve Yerel Kurulum Adımlarını Görmek İçin Tıklayın</b></summary>

### Gereksinimler

- .NET 9 SDK ve PostgreSQL.
- .NET 9 ile uyumlu geliştirme ortamı.
- Veritabanı migration'larını uygulamak için EF Core 9 araçları.
- İlgili özellikler için Google OAuth, Amazon S3, SMTP, PayTR ve OpenAI erişim bilgileri.

### 1. Bağımlılıkları yükleme

Depoyu indirdikten veya klonladıktan sonra `Bagery.WebUI` klasöründe çalıştırın:

```bash
dotnet restore
```

### 2. Yapılandırma

Proje User Secrets kullanımına uygun olarak tanımlanmıştır. Geliştirme ortamına ait değerleri User Secrets üzerinden tanımlayabilirsiniz. Gerçek erişim anahtarlarını ve şifreleri depoya eklemeyin.

| Ayar anahtarı | Kullanım |
| --- | --- |
| `ConnectionStrings:PostgreSQL` | PostgreSQL bağlantı bilgisi |
| `Authentication:Google:ClientId` | Google OAuth istemci kimliği |
| `Authentication:Google:ClientSecret` | Google OAuth istemci sırrı |
| `AWS:AccessKey` | S3 erişim anahtarı |
| `AWS:SecretKey` | S3 gizli erişim anahtarı |
| `AWS:BucketName` | Görsellerin saklanacağı bucket |
| `Email:Admin` | Gönderici e-posta hesabı |
| `Email:Code` | SMTP kimlik doğrulaması için kullanılan şifre |
| `PayTR:merchantId` | PayTR mağaza kimliği |
| `PayTR:merchantKey` | PayTR mağaza anahtarı |
| `PayTR:merchantSalt` | PayTR hash işlemlerinde kullanılan değer |
| `PayTR:testMode` | Test modu ayarı |
| `PayTR:debugOn` | PayTR hata ayıklama ayarı |
| `PayTR:baseUrl` | Sipariş e-postasındaki belge bağlantısının temel adresi |
| `OpenAI:ApiKey` | Dijital barista ve yorum moderasyonu erişim anahtarı |

Örnek bağlantı tanımı; değerleri kendi ortamınıza göre değiştirin:

```bash
dotnet user-secrets set "ConnectionStrings:PostgreSQL" "Host=localhost;Port=5432;Database=BageryDb;Username=YOUR_USER;Password=YOUR_PASSWORD"
```

Mevcut e-posta servisi `smtp.gmail.com:587`, S3 kaydı ise `eu-north-1` bölgesini kullanır. Servis hesaplarının yapılandırması bu uygulama ayarlarıyla uyumlu olmalıdır.

### 3. Veritabanını hazırlama

EF Core aracı kurulu değilse uyumlu bir 9.x sürümünü yükleyin. Ardından mevcut migration'ları uygulayın:

```bash
dotnet ef database update
```

İlk kurulumda `Admin`, `Writer`, `User` ve `Waiter` rollerinin ve ilk yönetici hesabının hazırlanması gerekir. Kaynak kodda başlangıçta bunları otomatik oluşturan bir seed akışı bulunmaz; kayıt işlemi mevcut `User` rolüne atama yapar. Migration'ların uygulanması tek başına kullanıma hazır demo hesapları oluşturmaz.

### 4. Uygulamayı başlatma

```bash
dotnet run --launch-profile https
```

HTTPS profili uygulamayı `https://localhost:7152` adresinde başlatacak şekilde tanımlıdır.

PayTR ödeme sonucunun alınabilmesi için mağaza panelindeki bildirim adresi, dışarıdan erişilebilen uygulama adresinin **`/CallBack/Index`** yoluna yönlendirilmelidir. Bu sunucu bildirimi, tarayıcının başarı veya başarısızlık sayfasına dönüşünden ayrı çalışır.

</details>

---

<div align="center">

## 💬 Proje ve İletişim

Projenin geliştirme süreci ve teknik detayları hakkında iletişime geçebilirsiniz.

<a href="https://www.linkedin.com/in/burakhanulusoy/">
  <img src="https://img.shields.io/badge/LinkedIn-Burakhan%20Ulusoy-0A66C2?style=for-the-badge&logo=linkedin&logoColor=white" alt="Burakhan Ulusoy LinkedIn" />
</a>

<br>
<br>

**🥐 MyAcademy_Bagery · ASP.NET Core MVC ile ürün, sipariş ve teslimat yönetimi**

</div>
