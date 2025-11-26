namespace Application.DTO.Info
{
    public class PatientInfoDto
    {
        public int Id { get; set; }

        public string FullName { get; set; } = "";

        public DoctorInfoDto? AssignedDoctor { get; set; }

        public List<DiseaseInfoDto> DiagnosedDiseases { get; set; } = new();
    }
}
