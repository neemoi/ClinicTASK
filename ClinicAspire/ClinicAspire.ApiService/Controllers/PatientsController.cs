using Application.Services.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ClinicAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _service;

        public PatientsController(IPatientService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _service.GetAll();
               
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAll Patients: {ex.Message}");
                
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var patient = await _service.Get(id);
                
                if (patient == null) return NotFound($"Patient with Id {id} not found");
                
                return Ok(patient);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Get Patient: {ex.Message}");
                
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
