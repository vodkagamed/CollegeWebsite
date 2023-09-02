namespace SchoolApi.Controllers.DTOs.Outcoming
{
    public class CollegeResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<int> StudentIds { get; set; } = new List<int>();
        public List<string?> StudentNames { get; set; } = new List<string?>();
        public List<int> TeacherIds { get; set; } = new List<int>();
        public List<string?> TeacherNames { get; set; } = new List<string?>();
        public List<int> CourseIds { get; set; } = new List<int>();
        public List<string?> CourseNames { get; set; } = new List<string?>();
    }
}
