using SchoolWebsite.shared.Entities;
using System.ComponentModel.DataAnnotations;

namespace SchoolWebsite.shared.Models
{
    public class Teacher:BaseEentity
    {
        public Guid CollegeId { get; set; }
        public College? College { get; set; }

        public Guid CourseId { get; set; }
        public Course? Course { get; set; }
    }
}
