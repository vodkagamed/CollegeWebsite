using SchoolWebsite.shared.Entities;
using System.ComponentModel.DataAnnotations;

namespace SchoolWebsite.shared.Models
{
    public class College : BaseEentity
    {
        public List<Teacher>? Teachers { get; set; } = new();
        public List<Student>? Students { get; set; } = new();
        public List<Course>? Courses { get; set; } = new();
    }
}
