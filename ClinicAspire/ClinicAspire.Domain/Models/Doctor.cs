namespace Domain.Models
{
    public class Doctor
    {
        public int Id { get; set; }
      
        public string FullName { get; set; } = "";
        
        public string Specialty { get; set; } = "";

        public List<Patient> Patients { get; set; } = new();
    }

}
