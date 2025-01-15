using Microsoft.EntityFrameworkCore;
using MedicalTrackingSystem.Models;
using System.Collections.Generic;

namespace MedicalTrackingSystem.Controllers
{
    // DbContext sınıfı, SQLite veritabanı ile iletişimi sağlar
    public class ApplicationDbContext : DbContext
    {
        // DbContext'e gelen konfigürasyonları başlatıyoruz
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        // Veritabanı yapılandırmasını burada yapıyoruz
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // SQLite veritabanını kullanıyoruz
            _ = optionsBuilder.UseSqlite("Data Source=medical_tracking.db");
        }

        // Model sınıflarınız için DbSet tanımlamaları
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Allergy> Allergies { get; set; }
        public DbSet<TestResults> TestResults { get; set; }
        public DbSet<User> Users { get; set; }
        public IEnumerable<object> PatientLog { get; internal set; }


        // Model yapılandırmalarını burada yapıyoruz
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Appointment ilişkileri
            _ = modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            _ = modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Allergy ilişkileri
            _ = modelBuilder.Entity<Allergy>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Allergies)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            // TestResults ilişkileri
            _ = modelBuilder.Entity<TestResults>()
                .HasOne(tr => tr.Patient)
                .WithMany(p => p.TestResults)
                .HasForeignKey(tr => tr.PatientId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
