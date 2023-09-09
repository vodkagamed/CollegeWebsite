namespace SchoolWebsite.shared.Models.Create
{
    public class TeacherCreateDTO
    {
        public int Id { get; set; }
        public int collegeId { get; set; }
        public int courseId { get; set; }
        public string? Name { get; set; }
    }
}
