namespace SchoolWebsite.shared
{
    public class College
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Teacher>? Teachers { get; set; }
        public List<Student>? Students { get; set; }
        public List<Subject>? Subjects { get; set; }
    }
}
