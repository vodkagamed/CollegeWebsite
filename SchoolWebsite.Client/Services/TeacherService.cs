using SchoolWebsite.shared.Models;

namespace SchoolWebsite.Client.Services
{
    public class TeacherService
    {
        private readonly HttpClient _httpClient;
        public TeacherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<HttpResponseMessage> CreateTeacher(Teacher createdTeacher, int collegeId)
        {
            return await _httpClient.PostAsJsonAsync($"/api/Teacher/{collegeId}", createdTeacher);
        }
        public async Task<HttpResponseMessage> GetTeachers()
        {
            return await _httpClient.GetAsync("/api/Teacher");
        }
        public async Task<HttpResponseMessage> GetTeacherById(int teacherId)
        {
            return await _httpClient.GetAsync($"/api/Teacher/{teacherId}");
        }
        public async Task<HttpResponseMessage> EditTeacher(int teacherId, Teacher editedTeacher)
        {
            return await _httpClient.PutAsJsonAsync($"api/Teacher/{teacherId}", editedTeacher);
        }

        public async Task<HttpResponseMessage> DeleteTeacher(int teacherId)
        {
            return await _httpClient.DeleteAsync($"/api/Teacher/{teacherId}");
        }
    }
}
