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

## 📝 İş Kuralları

1. **Kontenjan Kontrolü:** Kontenjanı dolan derslere başvuru yapılamaz
2. **Tekil Başvuru:** Bir öğrenci aynı derse birden fazla başvuru yapamaz
3. **Durum Yönetimi:** Başvuru durumları (Başvuruldu/Onaylandı/Reddedildi)