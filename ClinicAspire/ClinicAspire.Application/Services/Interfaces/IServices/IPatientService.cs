using Application.DTO.Info;
using Domain.Models;

namespace Application.Services.Interfaces.IServices
{
    public interface IPatientService
    {
        Task<List<PatientInfoDto>> GetAll();
        
        Task<PatientInfoDto?> Get(int id);
    }
}
