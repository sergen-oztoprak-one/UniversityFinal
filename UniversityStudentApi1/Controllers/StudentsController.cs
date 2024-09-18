using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
            [HttpPut("{studentId}")]
            public async Task<IActionResult> Put(int studentId, [FromBody] Student student)
            {
              if (!ModelState.IsValid)
              {
                  return BadRequest(ModelState);
              }
                var existingStudent = await _studentRepository.GetByIdAsync(studentId); 
                if (existingStudent == null)
                {
                    return NotFound();
                }
                existingStudent.Name = student.Name;    
                existingStudent.Age = student.Age;
                existingStudent.Gender = student.Gender;
                existingStudent.Address = student.Address;
                existingStudent.AddressId = student.AddressId;
                existingStudent.Email = student.Email;
                existingStudent.Phone = student.Phone;
                existingStudent.Courses = student.Courses;
                existingStudent.GPA = student.GPA;
                existingStudent.Image = student.Image;
                await _studentRepository.UpdateAsync(existingStudent);
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
