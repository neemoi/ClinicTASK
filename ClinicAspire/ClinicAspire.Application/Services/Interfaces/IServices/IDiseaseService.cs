using Application.DTO.Other;
using Domain.Models;

namespace Application.Services.Interfaces.IServices
{
    public interface IDiseaseService
    {
        Task<List<DiseaseDto>> GetAll();

        Task<DiseaseDto?> Update(DiseaseDto dto);
    }
}
