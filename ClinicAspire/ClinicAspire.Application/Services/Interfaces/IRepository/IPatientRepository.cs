using Domain.Models;

namespace Application.Services.Interfaces.IRepository
{
    public interface IPatientRepository
    {
        Task<List<Patient>> GetAllAsync();
        
        Task<Patient?> GetByIdAsync(int id);
    }
}
