using System.ComponentModel.DataAnnotations;

namespace SchoolWebsite.shared
{
    public class Course
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name must not exceed 50 characters.")]
        [RegularExpression(@"^[a-zA-Z\s\-']{2,50}$", ErrorMessage = "Use a valid name")]
        public string? Name { get; set; }
        public int CollegeId { get; set; }
        public College? College { get; set; }
        public List<Student>? Students { get; set; } 
        public List<Teacher>? Teachers { get; set; }
    }
}