using Microsoft.AspNetCore.Mvc;
using test1template.Models;
using test1template.Services;

namespace test1template.Controllers
{
    [ApiController]
    [Route("api/sample")]
    public class SampleController : ControllerBase
    {
        private readonly ITestingService _service;

        public SampleController(ITestingService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSample(int id)
        {
            var result = await _service.GetSample(id);
            return result is null ? NotFound("Not found.") : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddSample([FromBody] SampleDTO dto)
        {
            try
            {
                var result = await _service.AddSample(dto);
                return Ok(result);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSample(int id)
        {
            try
            {
                var result = await _service.DeleteSample(id);
                return Ok(result);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}