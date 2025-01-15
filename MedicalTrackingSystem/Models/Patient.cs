using System.Collections.Generic;
namespace MedicalTrackingSystem.Models
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int Age { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public ICollection<Appointment> Appointments { get; set; } // Navigation Property
        public ICollection<Allergy> Allergies { get; set; }
        public ICollection<TestResults> TestResults { get; set; }
    }
}