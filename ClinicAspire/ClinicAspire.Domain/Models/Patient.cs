namespace Domain.Models
{
    public class Patient
    {
        public int Id { get; set; }
       
        public string FullName { get; set; } = "";

        public int DoctorId { get; set; }
        
        public Doctor? Doctor { get; set; }

        public List<PatientDisease> PatientDiseases { get; set; } = new();
    }
}
