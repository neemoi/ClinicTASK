using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistance.Context
{
    public class ClinicContextDB : DbContext
    {
        public ClinicContextDB(DbContextOptions<ClinicContextDB> options)
          : base(options) { }

        public DbSet<Patient>? Patients { get; set; }
        public DbSet<Doctor>? Doctors { get; set; }
        public DbSet<Disease>? Diseases { get; set; }
        public DbSet<PatientDisease>? PatientDiseases { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PatientDisease>()
                .HasKey(p => new { p.PatientId, p.DiseaseId });

            modelBuilder.Entity<PatientDisease>()
                .HasOne(p => p.Patient)
                .WithMany(p => p.PatientDiseases)
                .HasForeignKey(p => p.PatientId);

            modelBuilder.Entity<PatientDisease>()
                .HasOne(d => d.Disease)
                .WithMany(d => d.PatientDiseases)
                .HasForeignKey(d => d.DiseaseId);
        }
    }
}