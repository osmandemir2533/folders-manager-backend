# Folders Manager

Folders Manager, dosya paylaşımı ve yönetimi için geliştirilmiş modern, güvenli ve kullanıcı dostu bir web uygulamasıdır.  
Kullanıcılar dosya yükleyebilir, indirebilir, kendi dosyalarını yönetebilir ve başkalarının paylaştığı dosyaları görüntüleyebilir.  
Proje, hem frontend hem de backend tarafında güncel teknolojiler ve en iyi güvenlik uygulamaları ile geliştirilmiştir.

[![GitHub](https://img.shields.io/badge/GitHub-Frontend-181717?style=for-the-badge&logo=github&logoColor=white)](https://github.com/osmandemir2533/folders-manager-frontend)

---

## 🚀 Projeyi Çalıştırmak İçin

### Frontend Kurulumu

Projenin frontend kısmına aşağıdaki linkten ulaşabilirsiniz:
[Folders Manager Frontend](https://github.com/osmandemir2533/folders-manager-frontend)
---

## Yapılandırma Kurulumu

Projeyi çalıştırmak için, projenin kök dizininde bir `appsettings.json` dosyası oluşturmanız gerekmektedir. Aşağıdaki adımları takip edin:

1. Kök dizinde `appsettings.json` adında yeni bir dosya oluşturun
2. Aşağıdaki yapılandırmayı ekleyin:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "VERİTABANI_BAĞLANTI_DİZİNİNİZİ_BURAYA_YAZIN"
  },
  "Jwt": {
    "Key": "JWT_GİZLİ_ANAHTARINIZI_BURAYA_YAZIN",
    "Issuer": "YAYINLAYICI_ADINIZI_BURAYA_YAZIN",
    "Audience": "HEDEF_KİTLE_ADINIZI_BURAYA_YAZIN"
  }
}
```

### Yapılandırma Detayları

- **ConnectionStrings**: `VERİTABANI_BAĞLANTI_DİZİNİNİZİ_BURAYA_YAZIN` kısmını gerçek veritabanı bağlantı dizininiz ile değiştirin
- **Jwt**: 
  - `Key`: Güvenli bir gizli anahtar ile değiştirin (en az 32 karakter uzunluğunda)
  - `Issuer`: Uygulamanızın yayınlayıcı adını yazın
  - `Audience`: Uygulamanızın hedef kitle adını yazın

---

```bash
# Repoyu klonlayın
git clone https://github.com/osmandemir2533/folders-manager-frontend.git

# Proje dizinine gidin
cd folders-manager-frontend

# Gerekli paketleri yükleyin
npm install

# Projeyi başlatın
npm run dev
```

### Backend Kurulumu

1. **Veritabanı Hazırlığı**
   - Visual Studio'da `Folders_Auction.sln` projesini açın
   - Çözüm Gezgini'nde (`Solution Explorer`) `Folders_Auction` katmanına sağ tıklayın ve **Başlangıç projesi olarak ayarla** seçeneğini tıklayın.
   - `appsettings.json` dosyasında connection string'i kendi `server name` inizi yazın:

   ```json
   "ConnectionStrings": {
    "DefaultConnection": "Server=your_server_address;Database=your_database_name;User Id=your_user_id;Password=your_secure_password;Encrypt=True;TrustServerCertificate=False;"
   }

   ```

   - SQL Server Management Studio'yu açın
   - Yeni bir veritabanı oluşturun: `FoldersAuctionDB` . Oluşturmasanız bile migration basma adımında otomatik oluşacak
   - Package Manager Console'u açın (Visual Studio'da `Ctrl+Q` tuşlarına basıp "Package Manager Console" yazın)
   - **Dikkat:** Package Manager Console'un üst kısmındaki 'Varsayılan Proje'yi `Folders_Auction_Data_Access` olarak seçin.
   - Aşağıdaki komutları sırasıyla çalıştırın:

   ```powershell
   # İlk migration'ı oluşturun
   Add-Migration mig1

   # Veritabanını güncelleyin
   Update-Database
   ```

   > **Not:** Migration'lar GitHub'da bulunmamaktadır. Her geliştirici kendi migration'larını oluşturmalıdır. Bu, veritabanı şemasının her ortamda doğru şekilde oluşturulmasını sağlar.
   
2. **Otomatik Swagger Başlatma (Visual Studio'da)**
   - Backend projesini her F5 ile başlattığınızda Swagger arayüzünün otomatik olarak açılması için aşağıdaki adımları izleyin:
   
   1. Visual Studio'da `Folders_Auction` katmanına sağ tıklayın ve **Özellikler**'i seçin.
   2. Sol menüden **Hata Ayıkla** sekmesine tıklayın.
   3. "Hata Ayıklama Başlatma Profilleri" bölümünde **Kullanıcı arabirimini aç** butonuna tıklayın.
   4. Açılan pencerede **https** kısmında aşağıdaki alanları doldurun:
      - **URL** kısmına: `https://localhost:7074/swagger`
      - **Uygulama URL'si** kısmına: `https://localhost:7074;http://localhost:5253`
   5. Kaydedin.
   6. Artık backend her başlatıldığında otomatik olarak Swagger arayüzü tarayıcıda açılacaktır.

   > **Not:** Bu ayar kişiseldir ve repoya eklenmez. Repoyu klonlayan herkesin kendi bilgisayarında bu adımları uygulaması gerekir. Aksi halde backend başlar, Swagger'a elle tarayıcıdan ulaşmak için `https://localhost:7074/swagger` adresini kullanabilirsiniz.

3. **Backend Projesini Başlatma**
   - Projeyi çalıştırın (F5 veya IIS Express)

**Not:** Backend projesinde yüklenen dosyalar, `Folders_Auction/wwwroot/uploads` klasörüne kaydedilir. Bu klasör, projenin çalışması için gereklidir ve repoya eklenmiştir. Kullanıcılar dosya yüklediğinde, dosyalar bu klasöre otomatik olarak kaydedilir. Endpointe gelen silme işleminde hem bu klasör altından hemde veritabanından ilgili dosya silinir.

> **Not:** Backend projesi çalışır durumda olmalıdır, aksi takdirde frontend API istekleri başarısız olacaktır.

---

## 📁 Backend Klasör ve Dosya Yapısı

```
Folders_Auction/
│
├── Controllers/
│   ├── AuthController.cs
│   └── FilesController.cs
│
├── Properties/
│   └── ... (VS proje ayar dosyaları)
│
├── wwwroot/
│   ├── uploads/
│   │   └── (yüklenen dosyalar burada gözükecek)
│   └── Docs/
│
├── obj/
│   └── ... (build dosyaları)
│
├── bin/
│   └── ... (derleme çıktıları)
│
├── Folders_Auction.csproj
├── Folders_Auction.csproj.user
├── Folders_Auction.http
├── Program.cs
├── README.md
├── appsettings.json
├── appsettings.Development.json
│
Folders_Auction_Business/
│
├── Managers/
│   ├── AuthManager.cs
│   └── FileManager.cs
│
├── Services/
│   ├── IAuthService.cs
│   └── IFileService.cs
│
├── obj/
│   └── ... (build dosyaları)
│
├── bin/
│   └── ... (derleme çıktıları)
│
├── Folders_Auction_Business.csproj
├── Class1.cs
│
Folders_Auction_Core/
│
├── DTOs/
│   ├── FileDto.cs
│   ├── FileUploadDto.cs
│   ├── UserLoginDto.cs
│   └── UserRegisterDto.cs
│
├── Entities/
│   ├── AppUser.cs
│   └── FileEntity.cs
│
├── obj/
│   └── ... (build dosyaları)
│
├── bin/
│   └── ... (derleme çıktıları)
│
├── Folders_Auction_Core.csproj
├── Class1.cs
│
Folders_Auction_Data_Access/
│
├── Contexts/
│   └── ApplicationDbContext.cs
│
├── Migrations/
│   ├── (migraiton basılınca bu kısımda gözükecek)
│ 
│
├── obj/
│   └── ... (build dosyaları)
│
├── bin/
│   └── ... (derleme çıktıları)
│
├── Folders_Auction_Data_Access.csproj
├── Class1.cs

```

---

# Folders Manager Backend

Folders Manager, dosya paylaşımı ve yönetimi için geliştirilmiş modern, güvenli ve kullanıcı dostu bir web uygulamasının backend kısmıdır.  
Proje, .NET Core tabanlı, katmanlı mimari kullanılarak geliştirilmiş ve en iyi güvenlik uygulamaları ile donatılmıştır.

---

## 🔗 Frontend Projesi

Projenin frontend kısmına aşağıdaki linkten ulaşabilirsiniz:
[Folders Manager Frontend](https://github.com/osmandemir2533/folders-manager-frontend)

Frontend projesi React ile geliştirilmiş olup, modern ve kullanıcı dostu bir arayüz sunmaktadır.

---

## 🧑‍💻 Kullanıcı Deneyimi ve İş Akışı

- **Ana sayfa:**  
  Kullanıcı giriş yapmış olsun veya olmasın, sistemdeki tüm dosyalar listelenir.  
  Bu alan ortak bir paylaşım alanı olarak tasarlanmıştır.

- **Upload (Yükle) butonu:**  
  Her zaman görünür. Ancak kullanıcı giriş yapmamışsa, butona tıklayınca otomatik olarak giriş yapma sayfasına yönlendirilir.

- **Download (İndir) butonu:**  
  Aynı şekilde, giriş yapılmamışsa kullanıcıyı login sayfasına yönlendirir.  
  Giriş yapan kullanıcılar ise dosyaları doğrudan indirebilir.

- **Kullanıcı kayıt/giriş:**  
  Kullanıcılar kolayca kayıt olabilir ve giriş yapabilir.  
  Giriş yapınca backend'den dönen JWT token localStorage'da saklanır ve tüm korumalı işlemlerde kullanılır.

- **Silme (Delete) butonu:**  
  Kullanıcı giriş yaptıktan sonra, ana sayfadaki dosya kartlarında sadece kendi yüklediği dosyalar için silme (Delete) butonu görünür.  
  Başkasının dosyası silinemez.

- **Güvenlik:**  
  Silme işlemi hem frontend hem de backendde güvenli şekilde kontrol edilir.  
  Backend'de her CRUD işleminde JWT token zorunludur ve sadece dosyanın sahibi dosyayı silebilir.

- **Header:**  
  Kullanıcı giriş yaptıysa, header'daki giriş yap ve kayıt ol butonları kaybolur, yerine kullanıcı adı ve profil ikonu gelir.  
  Buradan dashboard veya logout işlemleri yapılabilir.

- **Dashboard:**  
  Sadece giriş yapan kullanıcıya ait dosyalar listelenir.  
  Bunun için özel bir endpoint kullanılır: `GET /api/Files/my`

- **Bildirimler:**  
  Sistemde yapılan işlemler (başarılı veya hatalı) React Toastify ile bildirim olarak kullanıcıya gösterilir.

---

## 🛠️ Kullanılan Teknolojiler & Kütüphaneler

- React (frontend)
- ASP.NET Core (backend)
- SQL Server (veritabanı)
- Entity Framework Core (ORM)
- JWT Authentication
- React Toastify (bildirimler)
- Material UI (arayüz)
- React Icons (ikonlar)

---

## 🔗 API Endpointleri

**Auth**
- `POST /api/Auth/register` — Kayıt ol
- `POST /api/Auth/login` — Giriş yap

**Files**
- `POST /api/Files/upload` — Dosya yükle (JWT zorunlu)
- `GET /api/Files` — Tüm dosyaları listele (herkes erişebilir)
- `GET /api/Files/my` — Sadece giriş yapan kullanıcının dosyalarını getir (JWT zorunlu)
- `GET /api/Files/{id}` — Dosya detayını getir (JWT zorunlu)
- `DELETE /api/Files/{id}` — Dosya sil (JWT zorunlu, sadece sahibi silebilir)
- `GET /api/Files/download/{id}` — Dosya indir (herkes erişebilir)

> Sadece `/api/Files` ve `/api/Files/download/{id}` endpointleri herkese açıktır, diğer tüm işlemler için giriş yapılması gerekir.

---

## 🖼️ Arayüz

Aşağıda uygulamanın temel ekran görüntülerini ekleyebilirsiniz:

- **Ana Sayfa Ekran Görüntüsü**
  > ![Ana Sayfa](https://github.com/user-attachments/assets/8cb877bf-80a5-492f-8bfd-82a7f326e688)

- **Dashboard Ekran Görüntüsü**
  > ![Dashboard](https://github.com/user-attachments/assets/86c26957-8a73-4e9d-aaf8-44b46ffa0ff8)

- **Dosya Kartı Yapısı**
  > ![Kart Yapısı](https://github.com/user-attachments/assets/dce56264-257c-43d6-9341-dad18acc3a06)

- **About (Hakkında) Sayfası**
  > ![About](https://github.com/user-attachments/assets/dcd215f5-4ed1-4c53-b4df-13b01ba91130)

- **İletişim (Contact) Sayfası**
  > ![Contact](https://github.com/user-attachments/assets/384f2b23-28d5-46a1-a800-16b3fc76c8b0)

---

## 📬 İletişim

Benimle her zaman iletişime geçebilirsiniz:

[![Web Sitem](https://img.shields.io/badge/Web%20Site-1976d2?style=for-the-badge&logo=google-chrome&logoColor=white)](https://osmandemir2533.github.io/)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-0a66c2?style=for-the-badge&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/osmandemir2533/)

---

> Proje, modern web standartlarına uygun olarak geliştirilmiştir.  
> Hem güvenli hem de kullanıcı dostu bir dosya yönetim deneyimi sunar.
