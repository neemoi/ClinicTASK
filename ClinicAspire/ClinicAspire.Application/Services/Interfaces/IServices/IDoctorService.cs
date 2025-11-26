using Application.DTO.Other;

namespace Application.Services.Interfaces.IServices
{
    public interface IDoctorService
    {
        Task<List<DoctorDto>> GetAll();
        
        Task<List<DoctorDto>> GetBySpecialty(string specialty);
    }
}
