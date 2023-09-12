using SchoolWebsite.shared.Models;

namespace SchoolWebsite.Client.Services
{
    public class StudentService:GenericService<Student>
    {
        private readonly HttpClient _httpClient;
        public StudentService(HttpClient httpClient):base(httpClient)
        {
            _httpClient = httpClient;
            EndPoint = "Student";
        }
        public async Task<HttpResponseMessage> EnrollCourse(int studentId,int courseId)
        {
            return await _httpClient.PostAsJsonAsync($"/api/Student/{studentId}/Course/Enroll",courseId);
        }
        public async Task<HttpResponseMessage> CancelCourse(int studentId, int courseId)
        {
            return await _httpClient.PostAsJsonAsync($"/api/Student/{studentId}/Course/Cancel", courseId);
        }
    }
}
