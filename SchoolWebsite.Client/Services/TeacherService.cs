using SchoolWebsite.shared.Models;

namespace SchoolWebsite.Client.Services
{
    public class TeacherService : GenericService<Teacher>
    {
        public TeacherService(HttpClient httpClient) : base(httpClient)
        {
            EndPoint = "Teacher";
        }
    }
}
