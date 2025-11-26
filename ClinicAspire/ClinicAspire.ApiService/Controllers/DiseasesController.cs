using Application.DTO.Other;
using Application.Services.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ClinicAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiseasesController : ControllerBase
    {
        private readonly IDiseaseService _service;

        public DiseasesController(IDiseaseService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var diseases = await _service.GetAll();
                return Ok(diseases);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DiseasesController.GetAll: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] DiseaseDto dto)
        {
            try
            {
                var updated = await _service.Update(dto);
                if (updated == null) return NotFound();
                return Ok(updated);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DiseasesController.Update: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
