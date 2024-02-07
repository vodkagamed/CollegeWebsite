using SchoolWebsite.shared.Entities;
using System.ComponentModel.DataAnnotations;

namespace SchoolWebsite.shared.Models;

public class Student : BaseEentity
{
    public Guid CollegeId { get; set; }

    public string? Phone { get; set; }
    public string? Age { get; set; }

    public College? College { get; set; }

    public List<Course>? Courses { get; set; } = new();
}
