using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityStudentApi1.Models
{
    public class University
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [JsonProperty("state-province")]
        public string? StateProvince { get; set; }
        public List<string> Domains { get; set; }
        public string Country { get; set; }
        [JsonProperty("alpha_two_code")]
        public string AlphaTwoCode  { get; set; }
        [Required]
        public string Name { get; set; }
        [JsonProperty("web_pages")]
        public List<string>? WebPages { get; set; }
    }
}
