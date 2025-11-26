using Application.Services.Interfaces.IRepository;

namespace Application.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IDiseaseRepository DiseaseRepository { get; }
        
        public IDoctorRepository DoctorRepository { get; }
        
        public IPatientRepository PatientRepository { get; }

        Task SaveChangesAsync();
    }
}