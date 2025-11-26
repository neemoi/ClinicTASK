using Domain.Models;

namespace Application.Services.Interfaces.IRepository
{
    public interface IDiseaseRepository
    {
        Task<List<Disease>> GetAllAsync();

        Task<Disease?> GetByIdAsync(int id);

        Task<Disease?> UpdateAsync(Disease disease);
    }
}
