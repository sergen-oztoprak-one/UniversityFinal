namespace UniversityStudentApi1.Models
{
    public class StudentUniversity
    {
        public int StudentUniversityId { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int UniversityId { get; set; }
        public University University { get; set; }
    }
}
