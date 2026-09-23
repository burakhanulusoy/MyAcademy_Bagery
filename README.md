<div align="center">

# 🥐 MyAcademy_Bagery

### Yapay Zekâ Destekli Kafe, Pastane ve Online Sipariş Uygulaması

![ASP.NET Core 9](https://img.shields.io/badge/ASP.NET_Core-9.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![PostgreSQL](https://img.shields.io/badge/Database-PostgreSQL-4169E1?style=for-the-badge&logo=postgresql&logoColor=white)
![MediatR](https://img.shields.io/badge/Pattern-MediatR_%26_CQRS-7C3AED?style=for-the-badge)
![Identity](https://img.shields.io/badge/Auth-ASP.NET_Core_Identity-334155?style=for-the-badge)

![PayTR](https://img.shields.io/badge/Payment-PayTR-0F766E?style=for-the-badge)
![SignalR](https://img.shields.io/badge/Realtime-SignalR-2563EB?style=for-the-badge)
![Amazon S3](https://img.shields.io/badge/Storage-Amazon_S3-FF9900?style=for-the-badge)
![OpenAI](https://img.shields.io/badge/AI-OpenAI-111827?style=for-the-badge)

<br>

<p align="center">
  <strong>Ürün keşfinden ödemeye, sipariş yönetiminden canlı teslimat takibine.</strong>
</p>
<p align="center">
  <i>Menüye dayalı dijital barista, PayTR ödeme entegrasyonu, SignalR bildirimleri<br>
  ve farklı kullanıcı rollerine özel panellerle bütünleşen bir .NET 9 MVC projesi.</i>
</p>

</div>

---

**MyAcademy_Bagery**, bir kafe ve pastane işletmesinin dijital ürün sunumunu, çevrim içi sipariş sürecini ve günlük yönetim ihtiyaçlarını aynı uygulamada buluşturur. Müşteriler ürünleri ve seçeneklerini inceleyebilir, sepetlerine kupon uygulayabilir, ödemelerini tamamlayabilir ve siparişlerinin teslimat durumunu takip edebilir.

İşletme tarafında ise **ürün ve içerik yönetimi, satış analizleri, kullanıcı yetkilendirmesi ve teslimat operasyonu** ayrı paneller üzerinden yürütülür. OpenAI destekli dijital barista ve yorum moderasyonu, Amazon S3 görsel depolama, e-posta bildirimleri ve PDF sipariş çıktıları bu akışı tamamlar.

Proje geliştirilirken **MediatR ile komut–sorgu ayrımı, Repository ve Unit of Work, FluentValidation, Mapster ve merkezi hata işleme** gibi yaklaşımlar birlikte kullanılmıştır.

---

## 🌟 Projenin Öne Çıkan 8 Özelliği

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

### 🔐 5. Identity, Google Girişi ve Rol Bazlı Paneller

Hesap işlemleri ASP.NET Core Identity üzerinden yürütülür; kullanıcıların erişebildiği ekranlar rollerine göre ayrılır.

- **Hesap Yaşam Döngüsü:** Kayıt, giriş, e-posta doğrulama, şifre yenileme ve profil güncelleme akışları bulunur.
- **Google ile Giriş:** Kullanıcılar Google hesapları üzerinden oturum açabilir.
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

- **Hesap E-postaları:** E-posta doğrulama ve şifre yenileme işlemlerinde MailKit/MimeKit tabanlı servis kullanılır.
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
| **Gmail SMTP / MailKit** | Hesap doğrulama, şifre yenileme ve sipariş e-postaları. |
| **QuestPDF** | Sipariş verilerinden görüntülenebilir ve indirilebilir PDF oluşturulması. |

---


## 🔄 Mimari ve Proje Akışları

### 🏛️ Uygulama Mimarisi

Controller, MediatR, veri erişimi ve dış servislerin uygulama içindeki ilişkisi aşağıdaki diyagramda gösterilmiştir.

<details>
<summary><b>Mimari Diyagramını Görmek İçin Tıklayın</b></summary>

```mermaid
flowchart TB
    subgraph UI["Sunum · ASP.NET Core MVC"]
        WEB["Müşteri arayüzü<br/>Ürünler · Sepet · Ödeme · Blog"]
        PANELS["Rol bazlı alanlar<br/>Admin · Writer · User · Waiter"]
        CTRL["Controller'lar"]
        WEB --> CTRL
        PANELS --> CTRL
    end

    AUTH["ASP.NET Core Identity<br/>Oturum · Rol · Hesap işlemleri"]
    AUTH -.-> CTRL

    subgraph APP["Uygulama işlemleri"]
        MED["MediatR<br/>Command / Query"]
        HAND["Handler'lar<br/>Doğrulama · İş kuralları · Dönüşüm"]
        SERV["Uygulama servisleri"]
        MED --> HAND
        HAND --> SERV
    end

    CTRL --> MED
    CTRL -->|"AI uç noktası"| SERV

    subgraph DATA["Veri erişimi"]
        REPO["Repository'ler"]
        UOW["Unit of Work<br/>Değişiklikleri kaydetme"]
        CTX["AppDbContext · EF Core<br/>Audit interceptor · Soft delete filtresi"]
        DB[("PostgreSQL")]
        REPO --> CTX
        UOW --> CTX
        CTX --> DB
    end

    HAND --> REPO
    HAND --> UOW
    HAND -->|"Raporlama sorguları"| CTX
    AUTH --> CTX
    SERV --> SESSION["Session<br/>Sepet"]
    SERV --> PAY["PayTR<br/>Ödeme · Kart · Taksit"]
    SERV --> S3["Amazon S3<br/>Görseller"]
    SERV --> MAIL["SMTP<br/>Hesap ve sipariş e-postaları"]
    SERV --> AI["OpenAI<br/>Dijital barista · Moderasyon"]
    SERV --> PDF["QuestPDF<br/>Sipariş PDF çıktısı"]
    SERV --> HUB["SignalR · OrderHub<br/>Canlı sipariş ve teslimat bildirimi"]
    HUB -.-> PANELS

    classDef presentation fill:#eff6ff,stroke:#2563eb,color:#172554;
    classDef application fill:#f5f3ff,stroke:#7c3aed,color:#2e1065;
    classDef storage fill:#ecfdf5,stroke:#059669,color:#064e3b;
    classDef integration fill:#fff7ed,stroke:#ea580c,color:#7c2d12;
    class WEB,PANELS,CTRL,AUTH presentation;
    class MED,HAND,SERV application;
    class REPO,UOW,CTX,DB,SESSION storage;
    class PAY,S3,MAIL,AI,PDF,HUB integration;
```

</details>

### 🛍️ Siparişten Teslimata

Müşteri ürününü seçer, sepetini düzenler ve ödeme işlemini başlatır. PayTR bildirimiyle ödeme durumu güncellenir; başarılı ödeme sonrasında personel bilgilendirilir ve teslimat takibi başlar. **Ödeme durumu ile teslimat durumu ayrı olarak yönetilir.**

<details>
<summary><b>Ödeme ve Teslimat Akışını Görmek İçin Tıklayın</b></summary>

```mermaid
flowchart TD
    A["Ürün ve varyant seçimi"] --> B["Session sepeti<br/>Adet · Kupon · Kargo hesabı"]
    B --> C["Oturum açma ve ödeme formu"]
    C --> D{"Form, güncel sepet<br/>ve taksit kontrolleri"}
    D -->|"Düzeltme gerekli"| E["Uyarı göster<br/>Gerekirse sepeti güncelle"]
    E --> B
    D -->|"Uygun"| F["PayTR ödeme isteği"]
    F -->|"İstek kabul edildi"| G["Siparişi Pending kaydet<br/>3D doğrulama akışına geç"]
    G --> H["PayTR sunucu bildirimi<br/>POST /CallBack/Index"]
    H --> I{"Bildirim hash'i geçerli mi?"}
    I -->|"Hayır"| J["Bildirimi reddet<br/>Sipariş durumunu değiştirme"]
    I -->|"Evet"| K{"Sipariş bulundu mu?"}
    K -->|"Hayır"| L["Kayda al ve OK yanıtla"]
    K -->|"Evet"| M{"Ödeme durumu Pending mi?"}
    M -->|"Hayır"| N["Tekrar işleme<br/>OK yanıtla"]
    M -->|"Evet"| O{"Ödeme başarılı mı?"}
    O -->|"Hayır"| P["Failed<br/>Hata bilgisini kaydet"]
    O -->|"Evet"| Q["Paid<br/>Ödeme zamanını kaydet"]
    Q --> R["SignalR ile personeli bilgilendir<br/>Sipariş onay e-postası gönder"]
    Q --> S["Sipariş detayından<br/>PDF belgeye erişim"]
    R --> T["Teslimat: Bekliyor"]
    T --> U["Personel: Yola çıktı"]
    U --> V["Personel: Teslim edildi"]
    U -.-> W["İşlem geçmişini kaydet<br/>Personel ve müşteriye canlı bildirim"]
    V -.-> W

    classDef process fill:#eff6ff,stroke:#2563eb,color:#172554;
    classDef decision fill:#fff7ed,stroke:#ea580c,color:#7c2d12;
    classDef success fill:#ecfdf5,stroke:#059669,color:#064e3b;
    classDef failure fill:#fff1f2,stroke:#e11d48,color:#881337;
    class A,B,C,F,G,H,R,S,W process;
    class D,I,K,M,O decision;
    class Q,T,U,V success;
    class E,J,P failure;
```

</details>

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

<!-- GÖRSEL EKLEME: Her bölümdeki img satırları hazırdır. Görseli docs/images/ altına koyabilir veya src değerini GitHub'a yüklediğin görsel bağlantısıyla değiştirebilirsin. Ardından ilgili img satırının çevresindeki yorum işaretlerini kaldır. Fotoğrafları ekledikten sonra 'Görsel alanı' satırlarını silebilirsin. -->

### 🏠 Ana Sayfa ve İşletme Vitrini

<details>
<summary><b>Ana Sayfa Görsellerini Görmek İçin Tıklayın</b></summary>
<br>

> Ziyaretçileri karşılayan ürün sunumları, tanıtım alanları, işletme bilgileri ve dinamik içerikler.

**📷 Görsel alanı — Ana sayfa üst bölüm**

<!-- <img width="100%" alt="Ana sayfa üst bölüm" src="docs/images/ana-sayfa-01.png" /> -->

**📷 Görsel alanı — Ana sayfa ürün ve tanıtım alanları**

<!-- <img width="100%" alt="Ana sayfa ürün ve tanıtım alanları" src="docs/images/ana-sayfa-02.png" /> -->

**📷 Görsel alanı — İşletme içerikleri ve alt bölüm**

<!-- <img width="100%" alt="İşletme içerikleri ve alt bölüm" src="docs/images/ana-sayfa-03.png" /> -->

</details>

---

### 🥐 Ürün Kataloğu ve Varyant Seçimi

<details>
<summary><b>Ürün Ekranlarını Görmek İçin Tıklayın</b></summary>
<br>

> Müşterinin ürünleri incelediği, ürün detaylarına ulaştığı ve uygun varyantı seçerek sepetine eklediği süreç.

**📷 Görsel alanı — Ürün kataloğu**

<!-- <img width="100%" alt="Ürün kataloğu" src="docs/images/urun-katalogu.png" /> -->

**📷 Görsel alanı — Ürün detay sayfası**

<!-- <img width="100%" alt="Ürün detay sayfası" src="docs/images/urun-detayi.png" /> -->

**📷 Görsel alanı — Ürün varyantları ve fiyatları**

<!-- <img width="100%" alt="Ürün varyantları ve fiyatları" src="docs/images/urun-varyantlari.png" /> -->

</details>

---

### 🛒 Sepet, Kupon ve Tutar Hesaplama

<details>
<summary><b>Sepet ve Kupon Görsellerini Görmek İçin Tıklayın</b></summary>
<br>

> Sepet kalemleri, adet değişiklikleri, kupon uygulaması ve kargo dahil ödeme özetinin gösterildiği ekranlar.

**📷 Görsel alanı — Sepet ve tutar özeti**

<!-- <img width="100%" alt="Sepet ve tutar özeti" src="docs/images/sepet.png" /> -->

**📷 Görsel alanı — Kupon uygulaması**

<!-- <img width="100%" alt="Kupon uygulaması" src="docs/images/kupon-uygulama.png" /> -->

</details>

---

### 💳 PayTR Ödeme ve Taksit Süreci

<details>
<summary><b>Ödeme Akışını Görmek İçin Tıklayın</b></summary>
<br>

> Ödeme bilgilerinin girilmesi, taksit seçeneklerinin görüntülenmesi, 3D doğrulama ve ödeme sonucunun kullanıcıya sunulması.

**📷 Görsel alanı — Ödeme formu**

<!-- <img width="100%" alt="Ödeme formu" src="docs/images/odeme-formu.png" /> -->

**📷 Görsel alanı — Taksit seçenekleri**

<!-- <img width="100%" alt="Taksit seçenekleri" src="docs/images/taksit-secenekleri.png" /> -->

**📷 Görsel alanı — Ödeme sonuç ekranı**

<!-- <img width="100%" alt="Ödeme sonuç ekranı" src="docs/images/odeme-sonucu.png" /> -->

</details>

---

### 📦 Siparişlerim ve Canlı Teslimat Takibi

<details>
<summary><b>Sipariş Takip Ekranlarını Görmek İçin Tıklayın</b></summary>
<br>

> Kullanıcının kendi sipariş geçmişini incelediği ve sipariş detayından güncel teslimat durumunu takip ettiği alan.

**📷 Görsel alanı — Kişisel sipariş listesi**

<!-- <img width="100%" alt="Kişisel sipariş listesi" src="docs/images/siparislerim.png" /> -->

**📷 Görsel alanı — Sipariş detayları**

<!-- <img width="100%" alt="Sipariş detayları" src="docs/images/siparis-detayi.png" /> -->

**📷 Görsel alanı — Canlı teslimat durumu**

<!-- <img width="100%" alt="Canlı teslimat durumu" src="docs/images/canli-teslimat.png" /> -->

</details>

---

### 📬 Sipariş E-postası ve PDF Çıktısı

<details>
<summary><b>E-posta ve PDF Örneklerini Görmek İçin Tıklayın</b></summary>
<br>

> Başarılı ödeme sonrasında gönderilen sipariş onayı ve QuestPDF ile oluşturulan sipariş belgesinin görünümü.

**📷 Görsel alanı — Sipariş onay e-postası**

<!-- <img width="100%" alt="Sipariş onay e-postası" src="docs/images/siparis-eposta.png" /> -->

**📷 Görsel alanı — PDF sipariş belgesi**

<!-- <img width="100%" alt="PDF sipariş belgesi" src="docs/images/siparis-pdf.png" /> -->

</details>

---

### 🤖 Dijital Barista ve Ürün Önerileri

<details>
<summary><b>Yapay Zekâ Ekranlarını Görmek İçin Tıklayın</b></summary>
<br>

> Menü hakkında soru sorma, bütçeye uygun ürün seçimi ve yiyecek–içecek önerilerini inceleme deneyimi.

**📷 Görsel alanı — Dijital barista sohbet ekranı**

<!-- <img width="100%" alt="Dijital barista sohbet ekranı" src="docs/images/dijital-barista.png" /> -->

**📷 Görsel alanı — Menüye dayalı ürün önerileri**

<!-- <img width="100%" alt="Menüye dayalı ürün önerileri" src="docs/images/ai-urun-onerileri.png" /> -->

</details>

---

### 🔑 Kayıt, Giriş ve Hesap Doğrulama

<details>
<summary><b>Hesap İşlemlerini Görmek İçin Tıklayın</b></summary>
<br>

> Kullanıcı kaydı, e-posta doğrulaması, Google ile giriş ve şifre yenileme adımları.

**📷 Görsel alanı — Kayıt formu**

<!-- <img width="100%" alt="Kayıt formu" src="docs/images/kayit.png" /> -->

**📷 Görsel alanı — Giriş ve Google ile oturum açma**

<!-- <img width="100%" alt="Giriş ve Google ile oturum açma" src="docs/images/giris.png" /> -->

**📷 Görsel alanı — E-posta doğrulama ekranı**

<!-- <img width="100%" alt="E-posta doğrulama ekranı" src="docs/images/eposta-dogrulama.png" /> -->

**📷 Görsel alanı — Şifre yenileme ekranı**

<!-- <img width="100%" alt="Şifre yenileme ekranı" src="docs/images/sifre-yenileme.png" /> -->

</details>

---

### ✍️ Blog, Yazar Paneli ve Yorum Moderasyonu

<details>
<summary><b>Blog ve Yazar Ekranlarını Görmek İçin Tıklayın</b></summary>
<br>

> Blog içerikleri, yazarın kendi yazılarını yönetmesi ve uygunsuz olarak sınıflandırılan yorumlara verilen geri bildirim.

**📷 Görsel alanı — Blog detay sayfası**

<!-- <img width="100%" alt="Blog detay sayfası" src="docs/images/blog-detayi.png" /> -->

**📷 Görsel alanı — Yazar yönetim paneli**

<!-- <img width="100%" alt="Yazar yönetim paneli" src="docs/images/yazar-paneli.png" /> -->

**📷 Görsel alanı — Yorum moderasyonu geri bildirimi**

<!-- <img width="100%" alt="Yorum moderasyonu geri bildirimi" src="docs/images/yorum-moderasyonu.png" /> -->

</details>

---

### 📊 Satış Analizleri ve Yönetim Özeti

<details>
<summary><b>Dashboard Görsellerini Görmek İçin Tıklayın</b></summary>
<br>

> Dönemsel satış göstergeleri, ürün performansı, kategori dağılımları ve kupon kullanımına ait yönetim ekranları.

**📷 Görsel alanı — Satış dashboard genel görünüm**

<!-- <img width="100%" alt="Satış dashboard genel görünüm" src="docs/images/satis-dashboard.png" /> -->

**📷 Görsel alanı — Ürün ve kategori performansı**

<!-- <img width="100%" alt="Ürün ve kategori performansı" src="docs/images/urun-performansi.png" /> -->

**📷 Görsel alanı — Site genel görünüm paneli**

<!-- <img width="100%" alt="Site genel görünüm paneli" src="docs/images/site-genel-gorunum.png" /> -->

</details>

---

### 🛠️ Admin Yönetim Paneli

<details>
<summary><b>Yönetim Ekranlarını Görmek İçin Tıklayın</b></summary>
<br>

> Ürün, kategori, varyant, kullanıcı, rol, kupon, taksit oranı ve site içeriklerinin yönetildiği alanlar.

**📷 Görsel alanı — Ürün yönetimi**

<!-- <img width="100%" alt="Ürün yönetimi" src="docs/images/admin-urunler.png" /> -->

**📷 Görsel alanı — Varyant yönetimi**

<!-- <img width="100%" alt="Varyant yönetimi" src="docs/images/admin-varyantlar.png" /> -->

**📷 Görsel alanı — Kullanıcı ve rol yönetimi**

<!-- <img width="100%" alt="Kullanıcı ve rol yönetimi" src="docs/images/admin-kullanici-rol.png" /> -->

**📷 Görsel alanı — Kupon yönetimi**

<!-- <img width="100%" alt="Kupon yönetimi" src="docs/images/admin-kuponlar.png" /> -->

**📷 Görsel alanı — İçerik ve iletişim yönetimi**

<!-- <img width="100%" alt="İçerik ve iletişim yönetimi" src="docs/images/admin-icerikler.png" /> -->

</details>

---

### 🚚 Garson Panosu ve Teslimat Operasyonu

<details>
<summary><b>Teslimat Panosunu Görmek İçin Tıklayın</b></summary>
<br>

> Ödenmiş siparişlerin personel tarafından izlenmesi, teslimat durumunun güncellenmesi ve işlem geçmişinin incelenmesi.

**📷 Görsel alanı — Garson teslimat panosu**

<!-- <img width="100%" alt="Garson teslimat panosu" src="docs/images/garson-panosu.png" /> -->

**📷 Görsel alanı — Teslimat durum güncellemesi**

<!-- <img width="100%" alt="Teslimat durum güncellemesi" src="docs/images/teslimat-durum.png" /> -->

**📷 Görsel alanı — Teslimat işlem geçmişi**

<!-- <img width="100%" alt="Teslimat işlem geçmişi" src="docs/images/teslimat-gecmisi.png" /> -->

</details>

---

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
