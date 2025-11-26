using Application.DTO.Other;
using AutoMapper;
using Domain.Models;


namespace Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Disease, DiseaseDto>().ReverseMap();
        }
    }
}
