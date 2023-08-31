namespace SchoolWebsite.shared
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CollegeId { get; set; }
        public int SubjectId { get; set; }
        public College? College { get; set; }
        public Subject? Subject { get; set; }
    }
}
