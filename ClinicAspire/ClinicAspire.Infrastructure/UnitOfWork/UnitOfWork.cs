using Application.Services.Interfaces.IRepository;
using Application.UnitOfWork;
using Persistance.Context;

namespace Persistance.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public IDiseaseRepository DiseaseRepository { get; }
        public IDoctorRepository DoctorRepository { get; }
        public IPatientRepository PatientRepository { get; }

        private readonly ClinicContextDB сlinicContextDB;

        public UnitOfWork(IDiseaseRepository diseaseRepository, IDoctorRepository doctorRepository, IPatientRepository patientRepository)
        {
            DiseaseRepository = diseaseRepository;
            DoctorRepository = doctorRepository;
            PatientRepository = patientRepository;
        }

        public async Task SaveChangesAsync()
        {
            await сlinicContextDB.SaveChangesAsync();
        }
    }
}