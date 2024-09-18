namespace UniversityStudentApi1.Data
{
    using Microsoft.EntityFrameworkCore;
    using UniversityStudentApi1.Models;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<Address> Address { get; set; } 
        public DbSet<StudentUniversity> StudentUniversities { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentUniversity>()
                .HasKey(su => new { su.StudentId, su.UniversityId });
            modelBuilder.Entity<StudentUniversity>()
                .HasOne(su => su.Student)
                .WithMany()
                .HasForeignKey(su => su.StudentId);
            modelBuilder.Entity<StudentUniversity>()
                .HasOne(su => su.University)
                .WithMany()
                .HasForeignKey(su => su.UniversityId);
            modelBuilder.Entity<Student>()
               .HasKey(s => s.StudentId); 
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Address)
                .WithOne()
                .HasForeignKey<Student>(s => s.AddressId);
            base.OnModelCreating(modelBuilder);
        }       
    }
}
