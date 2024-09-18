using UniversityStudentApi1.Models;
using Repositories.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniversityStudentApi1.Data;

namespace Repositories.Concrete
{
    public class UniversityRepository : IUniversityRepository
    {
        private readonly ApplicationDbContext _context;
        public UniversityRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<University>> GetAllAsync()
        {
            return await _context.Set<University>().ToListAsync();
        }
        public async Task<University> GetByNameAsync(string name)
        {
            return await _context.Set<University>().FirstOrDefaultAsync(u => u.Name == name);
        }
        public async Task AddAsync(University university)
        {
            await _context.Set<University>().AddAsync(university);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(University university)
        {
            _context.Set<University>().Update(university);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(string name)
        {
            var university = await _context.Set<University>().FirstOrDefaultAsync(u => u.Name == name);
            if (university != null)
            {
                _context.Set<University>().Remove(university);
                await _context.SaveChangesAsync();
            }
        }
    }
}
