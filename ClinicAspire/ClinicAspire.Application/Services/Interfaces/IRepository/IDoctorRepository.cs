using Domain.Models;

namespace Application.Services.Interfaces.IRepository
{
    public interface IDoctorRepository
    {
        Task<List<Doctor>> GetBySpecialtyAsync(string specialty);
        
        Task<List<Doctor>> GetAllAsync();
    }
}
