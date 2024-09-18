    using Microsoft.AspNetCore.Mvc;
using UniversityStudentApi1.Models;
using UniversityStudentApi1.Repositories.Abstract;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Repositories.Abstract;

namespace UniversityStudentApi1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UniversitiesController : ControllerBase
    {
        private readonly IUniversityRepository _universityRepository;
        public UniversitiesController(IUniversityRepository universityRepository)
        {
            _universityRepository = universityRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var universities = await _universityRepository.GetAllAsync();
                if (universities == null || !universities.Any())
                {
                    return NotFound("No universities found.");
                }
                return Ok(universities);
            }
            catch (Exception ex)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("{Name}")]
        public async Task<IActionResult> Get(string Name)
        {
            var university = await _universityRepository.GetByNameAsync(Name);
            if (university == null)
            {
                return NotFound();
            }
            return Ok(university);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] University university)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _universityRepository.AddAsync(university);
            return CreatedAtAction(nameof(Get), new { name = university.Name }, university);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] University university)
        {
            if (id != university.Id)
            {
                return BadRequest();
            }
            await _universityRepository.UpdateAsync(university);
            return NoContent();
        }
        [HttpDelete("{Name}")]
        public async Task<IActionResult> Delete(string Name)
        {
            await _universityRepository.DeleteAsync(Name);
            return NoContent();
        }
    }
}
