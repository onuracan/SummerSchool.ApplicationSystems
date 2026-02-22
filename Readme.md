# Yaz Okulu Ders Başvuru ve Kontenjan Takip Modülü

Yaz okulu kapsamında açılan derslere öğrencilerin başvuru yapabildiği ve yöneticilerin başvuruları yönetebildiği web uygulamasıdır.

## 🎯 Proje Amacı

- .NET 8, Web API, Entity Framework Core ve MSSQL ile backend geliştirme
- ASP.NET MVC (Razor) ile kullanıcı dostu web arayüzü
- GSM + SMS doğrulama ile authentication (Sadece bir sabit kod vardır.)
- Kontenjan takibi ve durum yönetimi

## 🛠️ Teknolojiler

### Backend
- .NET 8.0
- ASP.NET Core Web API
- Entity Framework Core
- MSSQL Server
- AutoMapper
- FluentValidation
- Serilog (Logging)
- JWT Authentication
- SOAP Web Service Client

### Frontend
- ASP.NET Core MVC (Razor Pages)
- Bootstrap 4
- jQuery
- DataTables

### DevOps
- Docker

## 📐 Mimari Yapı

Proje katmanlı mimari ile geliştirilmiştir.

## 🗄️ Veritabanı Modeli

### Student (Öğrenci)
- FirstName, LastName
- IdentityNumber, SchoolNumber
- Department, Faculty
- PhoneNumber, EMail
- CountryCode

### Course (Ders)
- Code, Name
- Department, Faculty
- Quota (Kontenjan)

### CourseApplication (Başvuru)
- StudentId, CourseId
- ApplicationStatus (1: Başvuruldu, 2: Onaylandı, 3: Reddedildi)
- RejectDescription
- UpdatedUser, UpdatedDate

### OtpVerification (SMS Doğrulama)
- PhoneNumber
- Code, ExpirationTime

## 🚀 Kurulum

### Gereksinimler
- .NET 8 SDK
- SQL Server 2019+
- Docker (opsiyonel)

### Adım 1: Veritabanı Bağlantısını Yapılandırın
WebApi appsettings.json
Mvc appsettings.json

### Adım 2: Veritabanını migrate edin.
- Update-Database
# veya
- dotnet ef database update --startup-project ...

### Adım 3: Uygulamayı Çalıştırın
- dotnet run
- Web API
- Mvc

# 🔐 Giriş Bilgileri

### Admin Girişi
- **Kullanıcı Adı:** `admin`
- **Şifre:** `adminhalic`
- **URL:** `/Admin/Auth/Login`

### Öğrenci Girişi
- GSM numarası ile giriş yapılır
- SMS kodu: `147852` (sabit)
- **URL:** `/Auth/Login`

## ✨ Özellikler

### Öğrenci Özellikleri
✅ GSM + SMS OTP ile giriş  
✅ Yaz okulu derslerini listeleme  
✅ Kontenjanı dolu olmayan derslere başvuru  
✅ Başvurularını görüntüleme  
✅ Başvuru durumunu takip etme  
✅ Bir derse sadece 1 kez başvuru yapabilme  

### Yönetici Özellikleri
✅ Kullanıcı adı/şifre ile giriş  
✅ Ders bazlı başvuru listesi  
✅ Başvuruları onaylama/reddetme  
✅ Kontenjan takibi 

### Bonus Özellikler
✅ Serilog ile loglama  
✅ Docker desteği  
✅ SOAP Web Service entegrasyonu (Ülke bilgileri)  

## 🐳 Docker ile Çalıştırma

### Ön Koşullar
- Docker Desktop yüklü olmalı
- Docker Compose yüklü olmalı (Docker Desktop ile gelir)

### Hızlı Başlangıç

**Tüm servisleri başlatmak için:**
```bash
# Build ve başlat (ilk kez)
docker-compose up -d --build

# Sadece başlat (build edilmişse)
docker-compose up -d

# Logları izle
docker-compose logs -f

# Belirli bir servisin logunu izle
docker-compose logs -f mvc
docker-compose logs -f webapi
docker-compose logs -f sqlserver
```

**Durdurma ve temizleme:**
```bash
# Servisleri durdur
docker-compose stop

# Servisleri durdur ve sil
docker-compose down

# Servisleri durdur, sil ve volume'leri temizle (VERİTABANI SİLİNİR!)
docker-compose down -v
```

### Erişim URL'leri

- **MVC Web:** http://localhost:8080
- **Web API:** http://localhost:8081
- **Swagger:** http://localhost:8081/swagger
- **SQL Server:** localhost:1433
  - Kullanıcı: `sa`
  - Şifre: `YourStrong@Password`

### Veritabanı Migration

Container'lar çalıştıktan sonra migration uygulamak için:

```bash
# WebApi container'ına bağlan
docker exec -it summerschool-api /bin/bash

# Migration'ı uygula
dotnet ef database update

# Container'dan çık
exit
```

**Veya doğrudan:**
```bash
docker exec summerschool-api dotnet ef database update
```

### Sorun Giderme

**Container durumunu kontrol et:**
```bash
docker-compose ps
docker-compose logs
```

**SQL Server bağlantı testi:**
```bash
docker exec -it summerschool-sqlserver /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P YourStrong@Password -Q "SELECT @@VERSION"
```

**Container'ları yeniden build et:**
```bash
docker-compose build --no-cache
docker-compose up -d
```

**Volume'leri listele:**
```bash
docker volume ls
```

**Volume'leri temizle (DİKKAT: Veri kaybı!):**
```bash
docker-compose down -v
docker volume prune
```

### Production Deployment

Production ortamı için `docker-compose.override.yml` dosyası oluşturun:

```yaml
version: '3.8'

services:
  webapi:
    environment:
      - ASPNETCORE_ENVIRONMENT=Production
      - ConnectionStrings__DefaultConnection=Server=your-prod-server;...

  mvc:
    environment:
      - ASPNETCORE_ENVIRONMENT=Production
      - ApiUrl=https://your-api-domain.com
```

Ardından:
```bash
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
```

## 📝 İş Kuralları

1. **Kontenjan Kontrolü:** Kontenjanı dolan derslere başvuru yapılamaz
2. **Tekil Başvuru:** Bir öğrenci aynı derse birden fazla başvuru yapamaz
3. **Durum Yönetimi:** Başvuru durumları (Başvuruldu/Onaylandı/Reddedildi)