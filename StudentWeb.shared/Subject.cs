using System.ComponentModel.DataAnnotations;

namespace SchoolWebsite.shared
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CollegeId { get; set; }
        public College? College { get; set; }
        public List<Student>? Students { get; set; }
        public List<Teacher>? Teachers { get; set; }
    }
}