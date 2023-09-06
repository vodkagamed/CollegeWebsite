using System.ComponentModel.DataAnnotations;

namespace SchoolWebsite.shared
{
    public class College
    {
        public int Id { get; set; }
        [StringLength(50, ErrorMessage = "Name must not exceed 50 characters.")]
        [RegularExpression(@"^[a-zA-Z\s\-']{2,50}$", ErrorMessage = "Use a valid name")]
        public string? Name { get; set; }
        public List<Teacher>? Teachers { get; set; } = new();
        public List<Student>? Students { get; set; } = new();
        public List<Course>? Courses { get; set; } = new();
    }
}
