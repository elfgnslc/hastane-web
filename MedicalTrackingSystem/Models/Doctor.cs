using System.Collections.Generic;

namespace MedicalTrackingSystem.Models
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Specialty { get; set; }
        public string Hospital { get; set; }
        public decimal Fee { get; set; }
        public ICollection<Appointment> Appointments { get; set; } // Navigation Property

    }
}