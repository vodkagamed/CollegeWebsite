using System.ComponentModel.DataAnnotations;

namespace SchoolWebsite.shared;

public class Student
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    [StringLength(50, ErrorMessage = "Name must not exceed 50 characters.")]
    [RegularExpression(@"^[a-zA-Z\s\-']{2,50}$", ErrorMessage = "Use a valid name")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Phone is required.")]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "Phone must be a 11-digit number.")]
    public string? Phone { get; set; }

    [Required(ErrorMessage = "Age is required.")]
    [Range(13, 150, ErrorMessage = "Age must be between 13 and 150.")]
    public string? Age { get; set; }
    public int CollegeId { get; set; }
    public College? College { get; set; }
    public List<Course>? Courses { get; set; } = new();
}
