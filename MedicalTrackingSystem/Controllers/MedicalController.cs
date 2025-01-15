using Microsoft.AspNetCore.Mvc;
using MedicalTrackingSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace MedicalTrackingSystem.Controllers
{
    public class MedicalController : Controller
    {
        private readonly ApplicationDbContext _context;

        // DbContext'i constructor'da enjekte ediyoruz
        public MedicalController(ApplicationDbContext context)
        {
            _context = context;
        }
        // Add Doctor
        public IActionResult AddDoctor()
        {
            return View();
        }
        public IActionResult Home()
        {
            return View(); // Home.cshtml dosyasını döner
        }

        [HttpPost]
        public async Task<IActionResult> AddDoctor(Doctor doctor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Doctor objesini veritabanına ekliyoruz
                    _ = _context.Doctors.Add(doctor);
                    _ = await _context.SaveChangesAsync();  // Değişiklikleri kaydediyoruz

                    // Yeni eklenen doktorla birlikte doktorlar listesini tekrar alıyoruz
                    List<Doctor> doctors = await _context.Doctors.ToListAsync();
                    return View("ViewDoctors", doctors);  // Yeni doktorlar listesiyle ViewDoctors sayfasına yönlendiriyoruz
                }
            }
            catch (Exception ex)
            {
                // Hata durumunda log mesajı
                Console.WriteLine($"Error adding doctor: {ex.Message}");
            }
            return View(doctor);
        }

        // Add Patient
        public IActionResult AddPatient()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPatient(Patient patient)
        {
            if (ModelState.IsValid)
            {
                // Patient objesini veritabanına ekliyoruz
                _ = _context.Patients.Add(patient);
                _ = await _context.SaveChangesAsync();

                // Yeni eklenen hasta ile birlikte hastalar listesini tekrar alıyoruz
                List<Patient> patients = await _context.Patients.ToListAsync();
                return View("ViewPatients", patients);  // Yeni hastalar listesiyle ViewPatients sayfasına yönlendiriyoruz
            }
            return View(patient);
        }

        // Add Appointment
        public IActionResult AddAppointment()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAppointment(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                // Appointment objesini veritabanına ekliyoruz
                _ = _context.Appointments.Add(appointment);
                _ = await _context.SaveChangesAsync();

                // Yeni eklenen randevu ile birlikte randevular listesini tekrar alıyoruz
                List<Appointment> appointments = await _context.Appointments.ToListAsync();
                return View("ViewAppointments", appointments);  // Yeni randevular listesiyle ViewAppointments sayfasına yönlendiriyoruz
            }
            return View(appointment);
        }

        // Add Allergy
        public IActionResult AddAllergy()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAllergy(Allergy allergy)
        {
            if (ModelState.IsValid)
            {
                // Allergy objesini veritabanına ekliyoruz
                _ = _context.Allergies.Add(allergy);
                _ = await _context.SaveChangesAsync();

                // Yeni eklenen alerji ile birlikte alerjiler listesini tekrar alıyoruz
                List<Allergy> allergies = await _context.Allergies.ToListAsync();
                return View("ViewAllergies", allergies);  // Yeni alerjiler listesiyle ViewAllergies sayfasına yönlendiriyoruz
            }
            return View(allergy);
        }

        // Add Test Result
        public IActionResult AddTestResult()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTestResult(TestResults testResult)
        {
            if (ModelState.IsValid)
            {
                // TestResults objesini veritabanına ekliyoruz
                _ = _context.TestResults.Add(testResult);
                _ = await _context.SaveChangesAsync();

                // Yeni eklenen test sonucu ile birlikte test sonuçları listesini tekrar alıyoruz
                List<TestResults> testResults = await _context.TestResults.ToListAsync();
                return View("ViewTestResults", testResults);  // Yeni test sonuçları listesiyle ViewTestResults sayfasına yönlendiriyoruz
            }
            return View(testResult);
        }

        // View Doctors - Doktorları Görüntüleme
        public async Task<IActionResult> ViewDoctors()
        {
            List<Doctor> doctors = await _context.Doctors.ToListAsync();  // Doktorları veritabanından alıyoruz
            return View(doctors);  // Doktorları View'a gönderiyoruz
        }

        // View Patients - Hastaları Görüntüleme
        public async Task<IActionResult> ViewPatients()
        {
            List<Patient> patients = await _context.Patients.ToListAsync();  // Hastaları veritabanından alıyoruz
            return View(patients);  // Hastaları View'a gönderiyoruz
        }

        // View Appointments - Randevuları Görüntüleme
        public async Task<IActionResult> ViewAppointments()
        {
            List<Appointment> appointments = await _context.Appointments.ToListAsync();  // Randevuları veritabanından alıyoruz
            return View(appointments);  // Randevuları View'a gönderiyoruz
        }

        // View Allergies - Alerjileri Görüntüleme
        public async Task<IActionResult> ViewAllergies()
        {
            List<Allergy> allergies = await _context.Allergies.ToListAsync();  // Alerjileri veritabanından alıyoruz
            return View(allergies);  // Alerjileri View'a gönderiyoruz
        }

        // View Test Results - Test Sonuçlarını Görüntüleme
        public async Task<IActionResult> ViewTestResults()
        {
            List<TestResults> testResults = await _context.TestResults.ToListAsync();  // Test sonuçlarını veritabanından alıyoruz
            return View(testResults);  // Test sonuçlarını View'a gönderiyoruz
        }
    }
}
