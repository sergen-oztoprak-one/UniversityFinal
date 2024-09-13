using UniversityStudentApi1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Abstract
{
    public interface IUniversityRepository
    {
        Task<IEnumerable<University>> GetAllAsync();
        Task<University> GetByNameAsync(string name);
        Task AddAsync(University university);
        Task UpdateAsync(University university);
        Task DeleteAsync(string name);
    }
}
