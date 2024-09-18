namespace UniversityStudentApi1.Repositories.Concrete
{
    using Repositories.Abstract;
    using UniversityStudentApi1.Data;
    using UniversityStudentApi1.Models;
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
