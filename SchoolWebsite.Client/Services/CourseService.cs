namespace SchoolWebsite.Client.Services
{
    public class CourseService
    {
        private readonly HttpClient _httpClient;
        public CourseService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<HttpResponseMessage> CreateCourse(Course createdCourse,int collegeId)
        {
            return await _httpClient.PostAsJsonAsync($"/api/Course/{collegeId}", createdCourse);
        }
        public async Task<HttpResponseMessage> GetCourses()
        {
            return await _httpClient.GetAsync("/api/Course");
        }
        public async Task<HttpResponseMessage> GetCourseById(int courseId)
        {
            return await _httpClient.GetAsync($"/api/Course/{courseId}");
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
