<div align="center">

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

### 02 · Hesap Doğrulama

E-posta ve şifreyle kayıt olan kullanıcı, hesabını doğruladıktan sonra oturum açabilir.

```mermaid
flowchart LR
    A["Kayıt"]
    B["Identity<br/>Kod üretimi"]
    C["E-posta<br/>Doğrulama kodu"]
    D["Kod kontrolü"]
    E["Hesap doğrulandı<br/>EmailConfirmed"]
    A b1@--> B
    B b2@--> C
    C b3@--> D
    D b4@--> E
    class A entry;
    class B,C process;
    class D decision;
    class E success;

    classDef entry fill:#EFF6FF,stroke:#3B82F6,color:#172554,stroke-width:1.5px;
    classDef process fill:#F5F3FF,stroke:#8B5CF6,color:#2E1065,stroke-width:1.5px;
    classDef success fill:#ECFDF5,stroke:#10B981,color:#064E3B,stroke-width:1.5px;
    classDef decision fill:#FFF7ED,stroke:#F59E0B,color:#78350F,stroke-width:1.5px;
    classDef failure fill:#FFF1F2,stroke:#F43F5E,color:#881337,stroke-width:1.5px;

    b1@{ animation: slow }
    b2@{ animation: slow }
    b3@{ animation: slow }
    b4@{ animation: slow }
```

**Giriş koşulu:** Doğru şifre tek başına yeterli değildir. E-posta doğrulanmamışsa oturum açılmaz ve yeniden kod gönderilir. Hatalı veya süresi dolmuş kodla doğrulama tamamlanmaz. Google ile giriş ayrı sağlayıcı akışıdır.

### 03 · Şifremi Unuttum

Şifre kurtarma işlemi, Identity tarafından üretilen kullanıcıya özel token’ı içeren e-posta bağlantısıyla yürütülür.

```mermaid
flowchart LR
    A["Şifre yenileme<br/>talebi"]
    B["Identity<br/>Özel token"]
    C["E-posta<br/>Yenileme bağlantısı"]
    D["Yeni şifre<br/>Token kontrolü"]
    E["Şifre güncellendi<br/>Giriş ekranı"]
    A c1@--> B
    B c2@--> C
    C c3@--> D
    D c4@--> E
    class A entry;
    class B,C process;
    class D decision;
    class E success;

    classDef entry fill:#EFF6FF,stroke:#3B82F6,color:#172554,stroke-width:1.5px;
    classDef process fill:#F5F3FF,stroke:#8B5CF6,color:#2E1065,stroke-width:1.5px;
    classDef success fill:#ECFDF5,stroke:#10B981,color:#064E3B,stroke-width:1.5px;
    classDef decision fill:#FFF7ED,stroke:#F59E0B,color:#78350F,stroke-width:1.5px;
    classDef failure fill:#FFF1F2,stroke:#F43F5E,color:#881337,stroke-width:1.5px;

    c1@{ animation: slow }
    c2@{ animation: slow }
    c3@{ animation: slow }
    c4@{ animation: slow }
```

Token, bağlantıya eklenmeden önce URL’ye uygun biçimde kodlanır. Şifre güncellemesi `ResetPasswordAsync` ile gerçekleştirilir. Geçersiz token veya şifre kuralı ihlalinde işlem reddedilir. Şifre sıfırlamak, hesap doğrulamasının yerine geçmez.

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

### 06 · Dijital Barista

Asistan, uygulamanın menü bilgileriyle beslenir ve önerilerini mevcut ürünlerle ilişkilendirir.

```mermaid
flowchart LR
    A["Müşteri sorusu"]
    B["Güncel menü<br/>Veritabanı"]
    C["OpenAI<br/>Yanıt ve ürün kimlikleri"]
    D["Ürün eşleştirme"]
    E["Yanıt ve öneriler"]
    A f1@--> B
    B f2@--> C
    C f3@--> D
    D f4@--> E
    class A entry;
    class B,C,D process;
    class E success;

    classDef entry fill:#EFF6FF,stroke:#3B82F6,color:#172554,stroke-width:1.5px;
    classDef process fill:#F5F3FF,stroke:#8B5CF6,color:#2E1065,stroke-width:1.5px;
    classDef success fill:#ECFDF5,stroke:#10B981,color:#064E3B,stroke-width:1.5px;
    classDef decision fill:#FFF7ED,stroke:#F59E0B,color:#78350F,stroke-width:1.5px;
    classDef failure fill:#FFF1F2,stroke:#F43F5E,color:#881337,stroke-width:1.5px;

    f1@{ animation: slow }
    f2@{ animation: slow }
    f3@{ animation: slow }
    f4@{ animation: slow }
```

Kapsam dışı olarak sınıflandırılan sorular için yönlendirici yanıt gösterilir. Önerilen ürün kimlikleri veritabanından alınmış katalogla eşleştirilir.

### 07 · Yorum Moderasyonu

Yorum oluşturma isteği, kullanıcı yetkisi ve içerik kontrolünden sonra değerlendirilir.

```mermaid
flowchart LR
    A["Yorum isteği<br/>Admin / Writer"]
    B["Doğrulama<br/>Form · Oturum · Rol"]
    C{"OpenAI<br/>İçerik kontrolü"}
    D["Yorumu kaydet"]
    E["Yorumu reddet"]
    A g1@--> B
    B g2@--> C
    C g3@-->|"SAFE"| D
    C g4@-->|"TOXIC"| E
    class A entry;
    class B process;
    class C decision;
    class D success;
    class E failure;

    classDef entry fill:#EFF6FF,stroke:#3B82F6,color:#172554,stroke-width:1.5px;
    classDef process fill:#F5F3FF,stroke:#8B5CF6,color:#2E1065,stroke-width:1.5px;
    classDef success fill:#ECFDF5,stroke:#10B981,color:#064E3B,stroke-width:1.5px;
    classDef decision fill:#FFF7ED,stroke:#F59E0B,color:#78350F,stroke-width:1.5px;
    classDef failure fill:#FFF1F2,stroke:#F43F5E,color:#881337,stroke-width:1.5px;

    g1@{ animation: slow }
    g2@{ animation: slow }
    g3@{ animation: slow }
    g4@{ animation: slow }
```

Uygunsuz olarak sınıflandırılan yorum kaydedilmez; kullanıcıya geri bildirim verilir.

### 08 · PDF Sipariş Belgesi

Belge üretimi, sipariş sahipliği ve ödeme durumu kontrolünden sonra gerçekleştirilir.

```mermaid
flowchart LR
    A["Belge talebi"]
    B["Sahiplik ve<br/>Paid kontrolü"]
    C["Sipariş ve<br/>satıcı bilgileri"]
    D["QuestPDF"]
    E["Görüntüle<br/>veya indir"]
    A h1@--> B
    B h2@--> C
    C h3@--> D
    D h4@--> E
    class A entry;
    class B decision;
    class C,D process;
    class E success;

    classDef entry fill:#EFF6FF,stroke:#3B82F6,color:#172554,stroke-width:1.5px;
    classDef process fill:#F5F3FF,stroke:#8B5CF6,color:#2E1065,stroke-width:1.5px;
    classDef success fill:#ECFDF5,stroke:#10B981,color:#064E3B,stroke-width:1.5px;
    classDef decision fill:#FFF7ED,stroke:#F59E0B,color:#78350F,stroke-width:1.5px;
    classDef failure fill:#FFF1F2,stroke:#F43F5E,color:#881337,stroke-width:1.5px;

    h1@{ animation: slow }
    h2@{ animation: slow }
    h3@{ animation: slow }
    h4@{ animation: slow }
```

Başkasına ait veya ödenmemiş sipariş için kişisel PDF çıktısı sunulmaz.

---

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

<!-- GÖRSEL EKLEME: Her bölümdeki img satırları hazırdır. Görseli docs/images/ altına koyabilir veya src değerini GitHub'a yüklediğin görsel bağlantısıyla değiştirebilirsin. Ardından ilgili img satırının çevresindeki yorum işaretlerini kaldır. Her img satırı bir fotoğraf alanıdır; aynı bölüme ek görseller ekleyebilirsin. -->

### 🏠 Ana Sayfa ve İşletme Vitrini

<details>
<summary><b>Ana Sayfa Görsellerini Görmek İçin Tıklayın</b></summary>
<br>

> Ziyaretçileri karşılayan ürün sunumları, tanıtım alanları, işletme bilgileri ve dinamik içerikler.

<!-- <img width="100%" alt="Ana sayfa üst bölüm" src="docs/images/ana-sayfa-01.png" /> -->

<!-- <img width="100%" alt="Ana sayfa ürün ve tanıtım alanları" src="docs/images/ana-sayfa-02.png" /> -->

<!-- <img width="100%" alt="İşletme içerikleri ve alt bölüm" src="docs/images/ana-sayfa-03.png" /> -->

</details>

---

### 🥐 Ürün Kataloğu ve Varyant Seçimi

<details>
<summary><b>Ürün Ekranlarını Görmek İçin Tıklayın</b></summary>
<br>

> Müşterinin ürünleri incelediği, ürün detaylarına ulaştığı ve uygun varyantı seçerek sepetine eklediği süreç.

<!-- <img width="100%" alt="Ürün kataloğu" src="docs/images/urun-katalogu.png" /> -->

<!-- <img width="100%" alt="Ürün detay sayfası" src="docs/images/urun-detayi.png" /> -->

<!-- <img width="100%" alt="Ürün varyantları ve fiyatları" src="docs/images/urun-varyantlari.png" /> -->

</details>

---

### 🛒 Sepet, Kupon ve Tutar Hesaplama

<details>
<summary><b>Sepet ve Kupon Görsellerini Görmek İçin Tıklayın</b></summary>
<br>

> Sepet kalemleri, adet değişiklikleri, kupon uygulaması ve kargo dahil ödeme özetinin gösterildiği ekranlar.

<!-- <img width="100%" alt="Sepet ve tutar özeti" src="docs/images/sepet.png" /> -->

<!-- <img width="100%" alt="Kupon uygulaması" src="docs/images/kupon-uygulama.png" /> -->

</details>

---

### 💳 PayTR Ödeme ve Taksit Süreci

<details>
<summary><b>Ödeme Akışını Görmek İçin Tıklayın</b></summary>
<br>

> Ödeme bilgilerinin girilmesi, taksit seçeneklerinin görüntülenmesi, 3D doğrulama ve ödeme sonucunun kullanıcıya sunulması.

<!-- <img width="100%" alt="Ödeme formu" src="docs/images/odeme-formu.png" /> -->

<!-- <img width="100%" alt="Taksit seçenekleri" src="docs/images/taksit-secenekleri.png" /> -->

<!-- <img width="100%" alt="Ödeme sonuç ekranı" src="docs/images/odeme-sonucu.png" /> -->

</details>

---

### 📦 Siparişlerim ve Canlı Teslimat Takibi

<details>
<summary><b>Sipariş Takip Ekranlarını Görmek İçin Tıklayın</b></summary>
<br>

> Kullanıcının kendi sipariş geçmişini incelediği ve sipariş detayından güncel teslimat durumunu takip ettiği alan.

<!-- <img width="100%" alt="Kişisel sipariş listesi" src="docs/images/siparislerim.png" /> -->

<!-- <img width="100%" alt="Sipariş detayları" src="docs/images/siparis-detayi.png" /> -->

<!-- <img width="100%" alt="Canlı teslimat durumu" src="docs/images/canli-teslimat.png" /> -->

</details>

---

### 📬 Sipariş E-postası ve PDF Çıktısı

<details>
<summary><b>E-posta ve PDF Örneklerini Görmek İçin Tıklayın</b></summary>
<br>

> Başarılı ödeme sonrasında gönderilen sipariş onayı ve QuestPDF ile oluşturulan sipariş belgesinin görünümü.

<!-- <img width="100%" alt="Sipariş onay e-postası" src="docs/images/siparis-eposta.png" /> -->

<!-- <img width="100%" alt="PDF sipariş belgesi" src="docs/images/siparis-pdf.png" /> -->

</details>

---

### 🤖 Dijital Barista ve Ürün Önerileri

<details>
<summary><b>Yapay Zekâ Ekranlarını Görmek İçin Tıklayın</b></summary>
<br>

> Menü hakkında soru sorma, bütçeye uygun ürün seçimi ve yiyecek–içecek önerilerini inceleme deneyimi.

<!-- <img width="100%" alt="Dijital barista sohbet ekranı" src="docs/images/dijital-barista.png" /> -->

<!-- <img width="100%" alt="Menüye dayalı ürün önerileri" src="docs/images/ai-urun-onerileri.png" /> -->

</details>

---

### 🔑 Kayıt, Giriş ve Hesap Doğrulama

<details>
<summary><b>Hesap İşlemlerini Görmek İçin Tıklayın</b></summary>
<br>

> Identity ile e-posta doğrulama kodu, doğrulanmamış hesapta giriş kontrolü, Google ile giriş ve kullanıcıya özel bağlantıyla şifre yenileme adımları.

<!-- <img width="100%" alt="Kayıt formu" src="docs/images/kayit.png" /> -->

<!-- <img width="100%" alt="Giriş ve Google ile oturum açma" src="docs/images/giris.png" /> -->

<!-- <img width="100%" alt="E-posta doğrulama ekranı" src="docs/images/eposta-dogrulama.png" /> -->

<!-- <img width="100%" alt="Şifre yenileme ekranı" src="docs/images/sifre-yenileme.png" /> -->

<!-- <img width="100%" alt="Hesap doğrulama e-postası" src="docs/images/hesap-dogrulama-eposta.png" /> -->

<!-- <img width="100%" alt="Kullanıcıya özel şifre yenileme bağlantısı" src="docs/images/sifre-yenileme-eposta.png" /> -->

</details>

---

### ✍️ Blog, Yazar Paneli ve Yorum Moderasyonu

<details>
<summary><b>Blog ve Yazar Ekranlarını Görmek İçin Tıklayın</b></summary>
<br>

> Blog içerikleri, yazarın kendi yazılarını yönetmesi ve uygunsuz olarak sınıflandırılan yorumlara verilen geri bildirim.

<!-- <img width="100%" alt="Blog detay sayfası" src="docs/images/blog-detayi.png" /> -->

<!-- <img width="100%" alt="Yazar yönetim paneli" src="docs/images/yazar-paneli.png" /> -->

<!-- <img width="100%" alt="Yorum moderasyonu geri bildirimi" src="docs/images/yorum-moderasyonu.png" /> -->

</details>

---

### 📊 Satış Analizleri ve Yönetim Özeti

<details>
<summary><b>Dashboard Görsellerini Görmek İçin Tıklayın</b></summary>
<br>

> Dönemsel satış göstergeleri, ürün performansı, kategori dağılımları ve kupon kullanımına ait yönetim ekranları.

<!-- <img width="100%" alt="Satış dashboard genel görünüm" src="docs/images/satis-dashboard.png" /> -->

<!-- <img width="100%" alt="Ürün ve kategori performansı" src="docs/images/urun-performansi.png" /> -->

<!-- <img width="100%" alt="Site genel görünüm paneli" src="docs/images/site-genel-gorunum.png" /> -->

</details>

---

### 🛠️ Admin Yönetim Paneli

<details>
<summary><b>Yönetim Ekranlarını Görmek İçin Tıklayın</b></summary>
<br>

> Ürün, kategori, varyant, kullanıcı, rol, kupon, taksit oranı ve site içeriklerinin yönetildiği alanlar.

<!-- <img width="100%" alt="Ürün yönetimi" src="docs/images/admin-urunler.png" /> -->

<!-- <img width="100%" alt="Varyant yönetimi" src="docs/images/admin-varyantlar.png" /> -->

<!-- <img width="100%" alt="Kullanıcı ve rol yönetimi" src="docs/images/admin-kullanici-rol.png" /> -->

<!-- <img width="100%" alt="Kupon yönetimi" src="docs/images/admin-kuponlar.png" /> -->

<!-- <img width="100%" alt="İçerik ve iletişim yönetimi" src="docs/images/admin-icerikler.png" /> -->

</details>

---

### 🚚 Garson Panosu ve Teslimat Operasyonu

<details>
<summary><b>Teslimat Panosunu Görmek İçin Tıklayın</b></summary>
<br>

> Ödenmiş siparişlerin personel tarafından izlenmesi, teslimat durumunun güncellenmesi ve işlem geçmişinin incelenmesi.

<!-- <img width="100%" alt="Garson teslimat panosu" src="docs/images/garson-panosu.png" /> -->

<!-- <img width="100%" alt="Teslimat durum güncellemesi" src="docs/images/teslimat-durum.png" /> -->

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
