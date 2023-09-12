using SchoolWebsite.shared.Models;

namespace SchoolWebsite.Client.Services
{
    public class CourseService : GenericService<Course>
    {
        public CourseService(HttpClient httpClient) : base(httpClient)
        {
            EndPoint = "Course";
        }
    }
}
