using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using UniversityStudentApi1.Data;
using UniversityStudentApi1.Models;
using UniversityStudentApi1.Repositories.Abstract;

namespace UniversityStudentApi1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
       
        public StudentsController(IStudentRepository studentRepository)
        {
            
            _studentRepository = studentRepository;
        }
       //.

       




        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var students = await _studentRepository.GetAllAsync();
            return Ok(students);
        }

        [HttpGet("{StudentId}")]
        public async Task<IActionResult> Get(int StudentId)
        {
            var student = await _studentRepository.GetByIdAsync(StudentId);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _studentRepository.AddAsync(student);
            return CreatedAtAction(nameof(Get), new { id = student.StudentId }, student);
        }

        [HttpPut("{StudentId}")]
        public async Task<IActionResult> Put(int StudentId, [FromBody] Student student)
        {
            if (StudentId != student.StudentId)
            {
                return BadRequest("Student ID mismatch.");
            }

            // ModelState'i kontrol et
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Öğrenci olup olmadığını kontrol et
            var existingStudent = await _studentRepository.GetByIdAsync(StudentId);
            if (existingStudent == null)
            {
                return NotFound();
            }

            // Öğrenci güncellemesini yap
            await _studentRepository.UpdateAsync(student);
            return NoContent();
        }


        [HttpDelete("{StudentId}")]
        public async Task<IActionResult> Delete(int StudentId)
        {
            await _studentRepository.DeleteAsync(StudentId);
            return NoContent();
        }
    }
}
