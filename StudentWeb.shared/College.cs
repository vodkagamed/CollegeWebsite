namespace SchoolWebsite.shared
{
    public class College
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Teacher>? Teachers { get; set; } = new();
        public List<Student>? Students { get; set; } = new();
        public List<Course>? Subjects { get; set; } = new();
    }
}
