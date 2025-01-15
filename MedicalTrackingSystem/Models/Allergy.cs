namespace MedicalTrackingSystem.Models
{
    public class Allergy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public string Allergen { get; set; }
        public string Reaction { get; set; }
    }

}