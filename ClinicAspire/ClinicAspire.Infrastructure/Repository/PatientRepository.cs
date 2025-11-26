using Application.Services.Interfaces.IRepository;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistance.Context;

namespace Persistance.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ClinicContextDB _db;

        public PatientRepository(ClinicContextDB db) => _db = db;

        public async Task<List<Patient>> GetAllAsync()
        {
            try
            {
                return await _db.Patients
                    .Include(p => p.Doctor)
                    .Include(p => p.PatientDiseases)
                        .ThenInclude(pd => pd.Disease)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in PatientRepository.GetAllAsync: {ex.Message}");
                throw new ApplicationException("Error fetching patients", ex);
            }
        }

        public async Task<Patient?> GetByIdAsync(int id)
        {
            try
            {
                return await _db.Patients
                    .Include(p => p.Doctor)
                    .Include(p => p.PatientDiseases)
                        .ThenInclude(pd => pd.Disease)
                    .FirstOrDefaultAsync(p => p.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in PatientRepository.GetByIdAsync: {ex.Message}");
                throw new ApplicationException($"Error fetching patient with Id {id}", ex);
            }
        }
    }
}
