using MedicalTrackingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace MedicalTrackingSystem.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Hasta Kayıt (Registration) işlemi
        [HttpPost]
        public IActionResult PatientRegister(string username, string password, string confirmPassword, string email)
        {
            // Şifre doğrulama
            if (password != confirmPassword)
            {
                TempData["ErrorMessage"] = "Şifreler uyuşmuyor!";
                return RedirectToAction("Register"); // Register sayfasına geri dön
            }

            // Kullanıcı adı ve e-posta kontrolü
            User existingUser = _context.Users
                .FirstOrDefault(u => u.UserName == username || u.Email == email);
            if (existingUser != null)
            {
                TempData["ErrorMessage"] = "Bu kullanıcı adı veya e-posta zaten mevcut!";
                return RedirectToAction("Register"); // Register sayfasına geri dön
            }

            // Yeni hasta oluştur
            User patient = new()
            {
                UserName = username,
                Password = password, // Gerçek uygulamada şifreyi hash'lemek gereklidir
                Email = email,
                Role = "Patient" // Hasta rolü
            };

            // Veritabanına kaydet
            _ = _context.Users.Add(patient);
            _ = _context.SaveChanges();

            TempData["SuccessMessage"] = "Başarıyla kaydoldunuz!";
            return RedirectToAction("Index", "Home"); // Ana sayfaya yönlendir
        }

        // Hastane Girişi
        [HttpPost]
        public IActionResult HospitalLogin(string username, string password)
        {
            // Veritabanından kullanıcı doğrulama
            User user = _context.Users
                .FirstOrDefault(u => u.UserName == username && u.Password == password && u.Role == "Hospital");

            if (user != null)
            {
                // Eğer giriş başarılıysa Hospital sayfasına yönlendir
                return RedirectToAction("Home", "Hospital");  // Hastane anasayfasına yönlendiriyoruz
            }

            // Giriş başarısızsa hata mesajı ve geri yönlendirme
            TempData["ErrorMessage"] = "Hatalı kullanıcı adı veya şifre!";
            return RedirectToAction("Home", "Hospital");  // Hastane anasayfasına yönlendiriyoruz
        }

        // Hasta Girişi
        [HttpPost]
        public IActionResult PatientLogin(string username, string password)
        {
            // Veritabanından kullanıcı doğrulama
            User patient = _context.Users
                .FirstOrDefault(u => u.UserName == username && u.Password == password && u.Role == "Patient");

            if (patient != null)
            {
                // Eğer giriş başarılıysa PatientLog sayfasına yönlendir
                return RedirectToAction("PatientLog", "PatientLogin"); // Medical/PatientLog sayfasına yönlendir
            }

            // Giriş başarısızsa hata mesajı ve geri yönlendirme
            TempData["ErrorMessage"] = "Hatalı kullanıcı adı veya şifre!";
            return RedirectToAction("PatientDashboard", "PatientLog");  // Hasta sayfasına yönlendiriyoruz
        }
    }
}
