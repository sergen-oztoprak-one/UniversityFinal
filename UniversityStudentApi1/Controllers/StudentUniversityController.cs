using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityStudentApi1.Data;
using UniversityStudentApi1.Models;

namespace UniversityStudentApi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentUniversityController : Controller
    {
        private readonly ApplicationDbContext _context;
        public StudentUniversityController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> AddStudentToUniversity(int studentId, int universityId)
        {
            var student = await _context.Students.FindAsync(studentId);
            var university = await _context.Universities.FindAsync(universityId);
            if (student == null || university == null)
            {
                return NotFound("Student or University not found.");
            }
            var studentUniversity = new StudentUniversity
            {
                StudentId = studentId,
                UniversityId = universityId
            };
            _context.StudentUniversities.Add(studentUniversity);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetStudentsByUniversity(int universityId)
        {
            var students = await _context.StudentUniversities
                .Where(su => su.UniversityId == universityId)
                .Select(su => su.Student)
                .ToListAsync();
            if (students == null || !students.Any())
            {
                return NotFound("Bu üniversiteye kayıtlı öğrenci bulunamadı.");
            }
            return Ok(students);
        }
    }
}
