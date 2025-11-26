using Application.Services.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ClinicAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _service;

        public DoctorsController(IDoctorService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAll());
        }

        [HttpGet("specialty/{specialty}")]
        public async Task<IActionResult> GetBySpecialty(string specialty)
        {
            var doctors = await _service.GetBySpecialty(specialty);
            
            return Ok(doctors);
        }
    }
}
