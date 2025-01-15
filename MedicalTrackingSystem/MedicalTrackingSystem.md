# Medikal Takip Sistemi

Bu proje, hasta randevularını, doktor bilgilerini, test sonuçlarını ve alerji kayıtlarını yönetmek için geliştirilmiş bir web tabanlı uygulamadır.

## 🚀 Özellikler

- 👨‍⚕️ Doktor yönetimi
- 👥 Hasta kayıt ve takibi
- 📅 Randevu sistemi
- 🏥 Hastane yönetimi
- 🔬 Test sonuçları takibi
- ⚕️ Alerji kayıtları
- 🔐 Kullanıcı kimlik doğrulama ve yetkilendirme

## 🛠️ Teknolojiler

- ASP.NET Core 7.0
- Entity Framework Core
- SQLite Veritabanı
- Docker
- Bootstrap
- HTML/CSS/JavaScript

## 📋 Gereksinimler

- .NET 7.0 SDK
- Docker Desktop
- Visual Studio 2022 veya Visual Studio Code
- Git

## 🔧 Kurulum

### Yerel Geliştirme Ortamı İçin

1. Projeyi klonlayın:
```bash
git clone [proje-repo-url]
```

2. Proje dizinine gidin:
```bash
cd MedicalTrackingSystem
```

3. Bağımlılıkları yükleyin:
```bash
dotnet restore
```

4. Uygulamayı çalıştırın:
```bash
dotnet run
```

### Docker ile Çalıştırma

1. Docker image'ını oluşturun:
```bash
docker build -t medical-tracking-system .
```

2. Container'ı çalıştırın:
```bash
docker run -d -p 8080:80 --name medical-tracking medical-tracking-system
```

3. Tarayıcınızdan uygulamaya erişin:
```
http://localhost:8080
```

## 📝 Docker Komutları

### Temel Komutlar
- Container'ı başlatma: `docker start medical-tracking`
- Container'ı durdurma: `docker stop medical-tracking`
- Container loglarını görüntüleme: `docker logs medical-tracking`
- Çalışan container'ları listeleme: `docker ps`
- Tüm container'ları listeleme: `docker ps -a`

### Container'ı Silme ve Yeniden Oluşturma
```bash
docker rm -f medical-tracking
docker run -d -p 8080:80 --name medical-tracking medical-tracking-system
```

## 👥 Kullanıcı Rolleri

### Hasta
- Randevu oluşturma
- Test sonuçlarını görüntüleme
- Kişisel bilgileri güncelleme

### Doktor
- Hasta bilgilerini görüntüleme
- Test sonuçları ekleme
- Randevu takvimini yönetme

### Hastane Yönetimi
- Doktor ekleme/düzenleme
- Hasta kayıtlarını yönetme
- Randevu sistemini yönetme

## 🔒 Güvenlik

- SHA256 şifreleme
- Anti-forgery token koruması
- Rol tabanlı yetkilendirme
- Güvenli oturum yönetimi

## 🗄️ Veritabanı Yapısı

Uygulama SQLite veritabanı kullanmaktadır ve aşağıdaki ana tabloları içerir:
- Users
- Patients
- Doctors
- Appointments
- TestResults
- Allergies

## 📫 İletişim

-Elif Müberra GÜNEŞLİCE
-elfgnslc@gmail.com
## 📄 Lisans

Bu proje [lisans adı] altında lisanslanmıştır - detaylar için LICENSE dosyasına bakınız.
-yok
---

# Çalıştırma Kılavuzu

## 1. İlk Kurulum

### Docker Kullanarak
1. Docker Desktop'ı başlatın
2. Terminal veya Command Prompt'u açın
3. Proje dizinine gidin
4. Image'ı oluşturun:
```bash
docker build -t medical-tracking-system .
```
5. Container'ı başlatın:
```bash
docker run -d -p 8080:80 --name medical-tracking medical-tracking-system
```

### Yerel Geliştirme Ortamında
1. Visual Studio veya VS Code'u açın
2. Projeyi açın
3. Terminal'de şu komutu çalıştırın:
```bash
dotnet run
```

## 2. Sonraki Kullanımlar

### Docker ile:
1. Docker Desktop'ı başlatın
2. Mevcut container'ı başlatın:
```bash
docker start medical-tracking
```

### Yerel Geliştirme Ortamında:
1. Terminal'de proje dizinine gidin
2. `dotnet run` komutunu çalıştırın

## 3. Uygulamaya Erişim

- Docker kullanıyorsanız: `http://localhost:8080`
- Yerel geliştirme ortamında: `http://localhost:5000`

## 4. Test Kullanıcıları

### Hasta Girişi
- Email: `hasta@test.com`
- Şifre: `test123`

### Doktor Girişi
- Email: `doktor@test.com`
- Şifre: `test123`

## 5. Sorun Giderme

### Docker Sorunları
1. Container'ı yeniden başlatın:
```bash
docker restart medical-tracking
```

2. Logları kontrol edin:
```bash
docker logs medical-tracking
```

3. Container'ı silip yeniden oluşturun:
```bash
docker rm -f medical-tracking
docker run -d -p 8080:80 --name medical-tracking medical-tracking-system
```

### Veritabanı Sorunları
- Uygulama ilk çalıştığında veritabanı otomatik olarak oluşturulur
- Sorun yaşanması durumunda `data` klasörünü silip uygulamayı yeniden başlatın

### Bağlantı Sorunları
- Portların kullanılabilir olduğundan emin olun
- Firewall ayarlarını kontrol edin
- Docker Desktop'ın çalıştığından emin olun

## 6. Güncelleme

1. En son değişiklikleri çekin:
```bash
git pull origin main
```

2. Docker image'ını yeniden oluşturun:
```bash
docker build -t medical-tracking-system .
```

3. Eski container'ı silip yenisini oluşturun:
```bash
docker rm -f medical-tracking
docker run -d -p 8080:80 --name medical-tracking medical-tracking-system
```
