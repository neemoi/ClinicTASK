namespace Application.DTO.Other
{
    public class DoctorDto
    {
        public int Id { get; set; }

        public string FullName { get; set; } = "";

        public string Specialty { get; set; } = "";

        public List<PatientDoctorDto> Patients { get; set; } = new();
    }
}
