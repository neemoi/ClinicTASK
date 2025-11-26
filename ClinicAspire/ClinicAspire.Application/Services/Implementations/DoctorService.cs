using Application.DTO.Other;
using Application.Services.Interfaces.IRepository;
using Application.Services.Interfaces.IServices;
using Application.UnitOfWork;
using AutoMapper;

namespace Application.Services.Implementations
{
    public class DoctorService : IDoctorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DoctorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<DoctorDto>> GetAll()
        {
            try
            {
                var data = await _unitOfWork.DoctorRepository.GetAllAsync();
                return _mapper.Map<List<DoctorDto>>(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DoctorService.GetAll: {ex.Message}");
                throw new ApplicationException("Unable to get doctors", ex);
            }
        }

        public async Task<List<DoctorDto>> GetBySpecialty(string specialty)
        {
            try
            {
                var data = await _unitOfWork.DoctorRepository.GetBySpecialtyAsync(specialty);
                return _mapper.Map<List<DoctorDto>>(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DoctorService.GetBySpecialty: {ex.Message}");
                throw new ApplicationException($"Unable to get doctors with specialty {specialty}", ex);
            }
        }
    }
}
