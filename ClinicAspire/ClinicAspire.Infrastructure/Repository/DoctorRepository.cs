using Application.Services.Interfaces.IRepository;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistance.Context;

namespace Persistance.Repository
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ClinicContextDB _db;

        public DoctorRepository(ClinicContextDB db) => _db = db;

        public async Task<List<Doctor>> GetAllAsync()
        {
            try
            {
                return await _db.Doctors.Include(d => d.Patients).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DoctorRepository.GetAllAsync: {ex.Message}");
                throw new ApplicationException("Unable to fetch doctors", ex);
            }
        }

        public async Task<List<Doctor>> GetBySpecialtyAsync(string specialty)
        {
            try
            {
                return await _db.Doctors
                    .Where(d => d.Specialty == specialty)
                    .Include(d => d.Patients)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DoctorRepository.GetBySpecialtyAsync: {ex.Message}");
                throw new ApplicationException($"Unable to fetch doctors with specialty {specialty}", ex);
            }
        }
    }
}
