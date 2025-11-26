using Application.Mapping;
using Application.Services.Implementations;
using Application.Services.Interfaces.IRepository;
using Application.Services.Interfaces.IServices;
using Application.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Persistance.Context;
using Persistance.Repository;
using Persistance.UnitOfWork;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddDbContext<ClinicContextDB>(options =>
              options.UseMySql(
                  builder.Configuration.GetConnectionString("DefaultConnection"),
                  new MySqlServerVersion(new Version(8, 0, 21))
              ));


        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowLocalhostReact", policy =>
            {
                policy.WithOrigins("http://localhost:3000") 
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });

        builder.Services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
            cfg.AddProfile<MappingProfileDoctor>();
            cfg.AddProfile<MappingProfilePatientInfo>();
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

        builder.Services.AddScoped<IPatientRepository, PatientRepository>();
        builder.Services.AddScoped<IPatientService, PatientService>();

        builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
        builder.Services.AddScoped<IDoctorService, DoctorService>();

        builder.Services.AddScoped<IDiseaseRepository, DiseaseRepository>();
        builder.Services.AddScoped<IDiseaseService, DiseaseService>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Clinic API V1");
            });
        }

        app.UseCors("AllowLocalhostReact");
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}