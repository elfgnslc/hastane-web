using Microsoft.AspNetCore.Mvc;
using MedicalTrackingSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace MedicalTrackingSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Temel Sayfa Yönlendirmeleri
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Title"] = "Hakkında";
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Home()
        {
            return View();
        }

        public IActionResult HospitalPage()
        {
            return RedirectToAction("Home", "Hospital");
        }

        public IActionResult PatientLogPage()
        {
            return RedirectToAction("PatientLogin", "PatientLog");
        }

        public IActionResult Hospital()
        {
            return View();
        }

        public IActionResult PatientLog()
        {
            return View();
        }

        public IActionResult AddPatient()
        {
            return View();
        }

        public IActionResult PatientLogin()
        {
            return View();
        }

        public IActionResult PatientRegister()
        {
            return View();
        }

        public IActionResult AddDoctor()
        {
            return View();
        }

        public IActionResult Appointment()
        {
            return View();
        }

        public IActionResult ViewAppointment()
        {
            return View();
        }

        // Hasta Kaydı
        [HttpPost]
        public async Task<IActionResult> PatientRegister(User patient)
        {
            if (!ModelState.IsValid)
            {
                return View(patient);
            }

            Patient? existingPatient = await _context.Patients
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Email == patient.Email);

            if (existingPatient != null)
            {
                ModelState.AddModelError("Email", "Bu email adresi zaten kayıtlı.");
                return View(patient);
            }

            try
            {
                User newUser = new()
                {
                    Email = patient.Email,
                    Password = HashPassword(patient.Password),
                    Role = "patient"
                };

                Patient newPatient = new()
                {
                    Email = patient.Email,
                    FirstName = patient.UserName,
                    LastName = patient.Password
                };

                _ = await _context.Users.AddAsync(newUser);
                _ = await _context.Patients.AddAsync(newPatient);
                _ = await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Kayıt başarılı. Giriş yapabilirsiniz.";
                return RedirectToAction("PatientLogin");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Kayıt işlemi sırasında hata: {ex.Message}");
                return View(patient);
            }
        }

        // Giriş İşlemi
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            string hashedPassword = HashPassword(password);
            User? user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == hashedPassword);

            if (user == null)
            {
                TempData["ErrorMessage"] = "Geçersiz giriş bilgileri.";
                return View();
            }

            return user.Role switch
            {
                "hospital" => RedirectToAction("Home"),
                "patient" => RedirectToAction("PatientLogin"),
                _ => View()
            };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDoctor([FromForm] Doctor doctor)
        {
            try
            {
                // ModelState'den ilişkisel alanları kaldır (Eğer validasyon sorunlarına neden oluyorsa)
                _ = ModelState.Remove("Appointments");

                // ModelState geçerli değilse, formu yeniden göster
                if (!ModelState.IsValid)
                {
                    return View(doctor);
                }

                // Yeni doktor nesnesini oluştur
                Doctor newDoctor = new()
                {
                    FirstName = doctor.FirstName,
                    LastName = doctor.LastName,
                    Specialty = doctor.Specialty,
                    Hospital = doctor.Hospital,
                    Fee = doctor.Fee,
                    Appointments = new List<Appointment>() // Varsayılan olarak boş bir liste tanımla
                };

                // Doktoru veritabanına ekle
                _ = await _context.Doctors.AddAsync(newDoctor);
                _ = await _context.SaveChangesAsync();

                // Başarı mesajını TempData'ya ekle
                TempData["SuccessMessage"] = "Doktor başarıyla eklendi.";
                return RedirectToAction(nameof(ViewDoctors));
            }
            catch (Exception ex)
            {
                // Hata durumunda ModelState'e hata mesajı ekle ve formu yeniden göster
                ModelState.AddModelError("", $"Doktor eklenirken hata: {ex.Message}");
                return View(doctor);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAppointment([FromForm] Appointment appointment)
        {
            try
            {
                // İlişkisel özelliklerin ModelState validasyonunu kaldır
                _ = ModelState.Remove("Doctor");
                _ = ModelState.Remove("Patient");

                // ModelState kontrolü
                if (!ModelState.IsValid)
                {
                    ViewData["Doctors"] = await _context.Doctors.ToListAsync();
                    ViewData["Patients"] = await _context.Patients.ToListAsync();
                    return View(appointment);
                }

                // Randevu zaman dilimi kontrolü
                bool isTimeSlotAvailable = !await _context.Appointments
                    .AnyAsync(a => a.DoctorId == appointment.DoctorId && a.Date == appointment.Date);

                if (!isTimeSlotAvailable)
                {
                    ModelState.AddModelError("", "Bu zaman dilimi için randevu dolu.");
                    ViewData["Doctors"] = await _context.Doctors.ToListAsync();
                    ViewData["Patients"] = await _context.Patients.ToListAsync();
                    return View(appointment);
                }

                // Yeni randevu oluştur
                Appointment newAppointment = new()
                {
                    PatientId = appointment.PatientId,
                    DoctorId = appointment.DoctorId,
                    Date = appointment.Date ?? DateTime.Now // Tarih boşsa varsayılan olarak şu anki zamanı kullan
                };

                // Randevuyu veritabanına ekle
                _ = await _context.Appointments.AddAsync(newAppointment);
                _ = await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Randevu başarıyla oluşturuldu.";
                return RedirectToAction(nameof(ViewAppointments));
            }
            catch (Exception ex)
            {
                // Hata durumunda hata mesajını ekle ve formu yeniden göster
                ModelState.AddModelError("", $"Randevu eklenirken hata: {ex.Message}");
                ViewData["Doctors"] = await _context.Doctors.ToListAsync();
                ViewData["Patients"] = await _context.Patients.ToListAsync();
                return View(appointment);
            }
        }

        public async Task<IActionResult> ViewDoctors()
        {
            List<Doctor> doctors = await _context.Doctors.ToListAsync();
            return View(doctors);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPatient([FromForm] Patient patient)
        {
            try
            {
                // Model validation kontrolünü kaldır
                _ = ModelState.Remove("Allergies");
                _ = ModelState.Remove("TestResults");
                _ = ModelState.Remove("Appointments");

                if (!ModelState.IsValid)
                {
                    return View(patient);
                }

                Patient newPatient = new()
                {
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    Email = patient.Email,
                    Age = patient.Age,
                    Gender = patient.Gender,
                    Allergies = new List<Allergy>(),
                    TestResults = new List<TestResults>(),
                    Appointments = new List<Appointment>()
                };

                _ = await _context.Patients.AddAsync(newPatient);
                _ = await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Hasta başarıyla eklendi.";
                return RedirectToAction(nameof(ViewPatients));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Hasta eklenirken hata: {ex.Message}");
                return View(patient);
            }
        }

        public async Task<IActionResult> ViewPatients()
        {
            List<Patient> patients = await _context.Patients.ToListAsync();
            return View(patients);
        }

        // Randevu İşlemleri
        public async Task<IActionResult> AddAppointment()
        {
            ViewData["Doctors"] = await _context.Doctors.ToListAsync();
            ViewData["Patients"] = await _context.Patients.ToListAsync();
            return View();
        }

        public async Task<IActionResult> ViewAppointments()
        {
            List<Appointment> appointments = await _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .ToListAsync();
            return View(appointments);
        }

        // Tahlil Sonuçları
        public async Task<IActionResult> AddtestResults()
        {
            ViewData["Patients"] = await _context.Patients.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddtestResults([FromForm] TestResults testResults)
        {
            try
            {
                // Model validation kontrollerini kaldır
                _ = ModelState.Remove("Patient");

                if (!ModelState.IsValid)
                {
                    ViewData["Patients"] = await _context.Patients.ToListAsync();
                    string errors = string.Join("; ", ModelState.Values
                        .SelectMany(x => x.Errors)
                        .Select(x => x.ErrorMessage));
                    Console.WriteLine($"Model hatalar: {errors}");
                    return View(testResults);
                }

                // Debug için gelen verileri kontrol et
                Console.WriteLine($"Gelen test sonucu bilgileri: PatientId={testResults.PatientId}, TestName={testResults.TestName}, Result={testResults.Result}");

                TestResults newTestResult = new()
                {
                    PatientId = testResults.PatientId,
                    TestName = testResults.TestName,
                    Result = testResults.Result,
                };

                _ = await _context.TestResults.AddAsync(newTestResult);
                _ = await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Test sonucu başarıyla eklendi.";
                return RedirectToAction(nameof(ViewtestResults));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                ModelState.AddModelError("", $"Test sonucu eklenirken hata: {ex.Message}");
                ViewData["Patients"] = await _context.Patients.ToListAsync();
                return View(testResults);
            }
        }


        public async Task<IActionResult> ViewtestResults()
        {
            ViewData["Patients"] = await _context.Patients.ToListAsync();
            List<TestResults> results = await _context.TestResults
                .Include(t => t.Patient)
                .ToListAsync();
            return View(results);
        }

        // Alerji İşlemleri
        public async Task<IActionResult> AddAllerrgy()
        {
            ViewData["Patients"] = await _context.Patients.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAllergy([FromForm] Allergy allergy)
        {
            try
            {
                // İlişkisel özelliklerin ModelState validasyonunu kaldır
                _ = ModelState.Remove("Patient");

                // ModelState kontrolü
                if (!ModelState.IsValid)
                {
                    ViewData["Patients"] = await _context.Patients.ToListAsync();
                    return View(allergy);
                }

                // Yeni alerji kaydı oluştur
                Allergy newAllergy = new()
                {
                    PatientId = allergy.PatientId,
                    Name = allergy.Name,
                    Allergen = allergy.Allergen,
                    Reaction = allergy.Reaction // Reaksiyon bilgisi ekleniyor
                };

                // Veritabanına kaydet
                _ = await _context.Allergies.AddAsync(newAllergy);
                _ = await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Alerji kaydı başarıyla eklendi.";
                return RedirectToAction(nameof(ViewAllergyResults));
            }
            catch (Exception ex)
            {
                // Hata durumunda hata mesajını ekle ve formu yeniden göster
                ModelState.AddModelError("", $"Alerji eklenirken hata: {ex.Message}");
                ViewData["Patients"] = await _context.Patients.ToListAsync();
                return View(allergy);
            }
        }

        public async Task<IActionResult> ViewAllergyResults()
        {
            ViewData["Patients"] = await _context.Patients.ToListAsync();
            List<Allergy> allergies = await _context.Allergies
                .Include(a => a.Patient)
                .ToListAsync();
            return View(allergies);
        }

        // Yardımcı Metodlar
        private static string HashPassword(string password)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashedBytes = SHA256.HashData(passwordBytes);
            return Convert.ToBase64String(hashedBytes);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            Microsoft.AspNetCore.Diagnostics.IExceptionHandlerPathFeature exceptionHandlerPathFeature =
                HttpContext.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerPathFeature>();

            ErrorViewModel viewModel = new()
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                ErrorMessage = exceptionHandlerPathFeature?.Error?.Message
            };

            return View(viewModel);
        }
    }
}