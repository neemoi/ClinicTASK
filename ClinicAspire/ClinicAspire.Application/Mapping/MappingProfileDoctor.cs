using Application.DTO.Other;
using AutoMapper;
using Domain.Models;

namespace Application.Mapping
{
    public class MappingProfileDoctor : Profile
    {
        public MappingProfileDoctor()
        {
            CreateMap<Doctor, DoctorDto>()
                .ForMember(dest => dest.Patients, opt => opt.MapFrom(src => src.Patients));

            CreateMap<Patient, PatientDoctorDto>();
        }
    }
}
