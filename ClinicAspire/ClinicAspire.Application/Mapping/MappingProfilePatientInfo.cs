using Application.DTO.Info;
using AutoMapper;
using Domain.Models;

namespace Application.Mapping
{
    public class MappingProfilePatientInfo : Profile
    {
        public MappingProfilePatientInfo()
        {
            CreateMap<Patient, PatientInfoDto>()
             .ForMember(dest => dest.DiagnosedDiseases, opt => opt.MapFrom(src =>
                 src.PatientDiseases.Select(pd => pd.Disease)))
             .ForMember(dest => dest.AssignedDoctor, opt => opt.MapFrom(src => src.Doctor));

            CreateMap<Doctor, DoctorInfoDto>();
            CreateMap<Disease, DiseaseInfoDto>();
        }
    }
}
