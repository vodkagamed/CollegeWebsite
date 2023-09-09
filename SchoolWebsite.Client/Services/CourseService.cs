using SchoolWebsite.shared.Models;

namespace SchoolWebsite.Client.Services
{
    public class CourseService
    {
        private readonly HttpClient _httpClient;
        public CourseService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<HttpResponseMessage> CreateCourse(int collegeId,Course createdCourse)
        {
            return await _httpClient.PostAsJsonAsync($"/api/Course/{collegeId}", createdCourse);
        }
        public async Task<HttpResponseMessage> GetCourses(int collId)
        {
            return await _httpClient.GetAsync($"/api/Course/{collId}");
        }
        public async Task<HttpResponseMessage> GetCourseById(int courseId)
        {
            return await _httpClient.GetAsync($"/api/Course/byId/{courseId}");
        }
        public async Task<HttpResponseMessage> EditCourse(int courseId, Course editedCourse)
        {
            return await _httpClient.PutAsJsonAsync($"api/Course/{courseId}", editedCourse);
        }

        public async Task<HttpResponseMessage> DeleteCourse(int courseId)
        {
            return await _httpClient.DeleteAsync($"/api/Course/{courseId}");
        }
    }
}
