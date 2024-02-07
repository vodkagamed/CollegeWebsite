using SchoolWebsite.shared.Entities;

namespace SchoolWebsite.shared.Models
{
    public class Course : BaseEentity
    {
        public Guid CollegeId { get; set; }
        public College? College { get; set; }
        public List<Student>? Students { get; set; }
        public List<Teacher>? Teachers { get; set; }
    }
}