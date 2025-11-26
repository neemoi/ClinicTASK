using Application.DTO.Other;
using Application.Services.Interfaces.IServices;
using Application.UnitOfWork;
using AutoMapper;

namespace Application.Services.Implementations
{
    public class DiseaseService : IDiseaseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DiseaseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<DiseaseDto>> GetAll()
        {
            try
            {
                var data = await _unitOfWork.DiseaseRepository.GetAllAsync();
                return _mapper.Map<List<DiseaseDto>>(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DiseaseService.GetAll: {ex.Message}");
                throw new ApplicationException("Unable to fetch diseases", ex);
            }
        }

        public async Task<DiseaseDto?> GetById(int id)
        {
            try
            {
                var disease = await _unitOfWork.DiseaseRepository.GetByIdAsync(id);
                if (disease == null) return null;
                return _mapper.Map<DiseaseDto>(disease);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DiseaseService.GetById: {ex.Message}");
                throw new ApplicationException($"Unable to fetch disease with Id {id}", ex);
            }
        }

        public async Task<DiseaseDto?> Update(DiseaseDto dto)
        {
            try
            {
                var disease = await _unitOfWork.DiseaseRepository.GetByIdAsync(dto.Id);
                if (disease == null) return null;

                disease.Name = dto.Name;
                disease.Description = dto.Description;

                var updated = await _unitOfWork.DiseaseRepository.UpdateAsync(disease);
                return _mapper.Map<DiseaseDto>(updated);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DiseaseService.Update: {ex.Message}");
                throw new ApplicationException($"Unable to update disease with Id {dto.Id}", ex);
            }
        }
    }
}
