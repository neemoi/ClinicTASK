using Application.Services.Interfaces.IRepository;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistance.Context;

namespace Persistance.Repository
{
    public class DiseaseRepository : IDiseaseRepository
    {
        private readonly ClinicContextDB _db;

        public DiseaseRepository(ClinicContextDB db) => _db = db;

        public async Task<List<Disease>> GetAllAsync()
        {
            try
            {
                return await _db.Diseases.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DiseaseRepository.GetAllAsync: {ex.Message}");
                throw new ApplicationException("Unable to fetch diseases from database", ex);
            }
        }

        public async Task<Disease?> GetByIdAsync(int id)
        {
            try
            {
                return await _db.Diseases.FirstOrDefaultAsync(d => d.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DiseaseRepository.GetByIdAsync: {ex.Message}");
                throw new ApplicationException($"Unable to fetch disease with Id {id}", ex);
            }
        }

        public async Task<Disease?> UpdateAsync(Disease disease)
        {
            try
            {
                var entity = await _db.Diseases.FirstOrDefaultAsync(d => d.Id == disease.Id);
                if (entity == null) return null;

                entity.Name = disease.Name;
                entity.Description = disease.Description;

                await _db.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DiseaseRepository.UpdateAsync: {ex.Message}");
                throw new ApplicationException($"Unable to update disease with Id {disease.Id}", ex);
            }
        }
    }
}