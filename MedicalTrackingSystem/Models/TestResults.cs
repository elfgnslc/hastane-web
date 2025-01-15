namespace MedicalTrackingSystem.Models
{
    public class TestResults
    {
        public int TestResultsId { get; set; }
        public string TestName { get; set; }
        public string Result { get; set; }

        public int PatientId { get; set; } // Add this line if it's missing
        public Patient Patient { get; set; }


    }

}