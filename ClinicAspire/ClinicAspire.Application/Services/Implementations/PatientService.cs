using Application.DTO.Info;
using Application.Services.Interfaces.IServices;
using Application.UnitOfWork;
using AutoMapper;

public class PatientService : IPatientService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PatientService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<PatientInfoDto>> GetAll()
    {
        try
        {
            var data = await _unitOfWork.PatientRepository.GetAllAsync();
            return _mapper.Map<List<PatientInfoDto>>(data);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in PatientService.GetAll: {ex.Message}");
            throw new ApplicationException("Unable to get patients", ex);
        }
    }

    public async Task<PatientInfoDto?> Get(int id)
    {
        try
        {
            var patient = await _unitOfWork.PatientRepository.GetByIdAsync(id);
            if (patient == null) return null;
            return _mapper.Map<PatientInfoDto>(patient);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in PatientService.Get: {ex.Message}");
            throw new ApplicationException($"Unable to get patient with Id {id}", ex);
        }
    }
}