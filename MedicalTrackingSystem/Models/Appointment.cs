
namespace MedicalTrackingSystem.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int DoctorId { get; set; }  // Foreign Key
        public int PatientId { get; set; }  // Foreign Key
        public System.DateTime? Date { get; set; }  // DateTime? nullable, boş olabilir.

        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
    }
}
