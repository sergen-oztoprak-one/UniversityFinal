using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Repositories.Abstract;
using UniversityStudentApi1.Models;
using UniversityStudentApi1.Repositories.Abstract;
using UniversityStudentApi1.Repositories.Concrete;

namespace UniversityStudentApi1.Data
{
        public class DataService
        {
            private readonly IStudentRepository _studentRepository;
            private readonly IUniversityRepository _universityRepository;
            private readonly HttpClient _httpClient;
            public DataService(IStudentRepository studentRepository, IUniversityRepository universityRepository, HttpClient httpClient)
            {
                _studentRepository = studentRepository;
                _universityRepository = universityRepository;
                _httpClient = httpClient;
            }
            public async Task FetchAndStoreStudentDataAsync()
            {
                var response = await _httpClient.GetStringAsync("https://freetestapi.com/api/v1/students");
                var students = JsonConvert.DeserializeObject<List<Student>>(response);
                foreach (var student in students)
                {
                    await _studentRepository.AddAsync(student);
                }
            }
            public async Task FetchAndStoreUniversityDataAsync()
            {
                var response = await _httpClient.GetStringAsync("http://universities.hipolabs.com/search?country=United+States");
                var universities = JsonConvert.DeserializeObject<List<University>>(response);
                foreach (var university in universities)
                {
                    await _universityRepository.AddAsync(university);
                }
            }
        }
    }
