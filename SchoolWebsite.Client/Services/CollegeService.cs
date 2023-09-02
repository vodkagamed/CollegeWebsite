namespace SchoolWebsite.Client.Services
{
    public class CollegeService
    {
        private readonly HttpClient _httpClient;
        public CollegeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<HttpResponseMessage> CreateCollege(College createdCollege)
        {
            return await _httpClient.PostAsJsonAsync("/api/College", createdCollege);
        }
        public async Task<HttpResponseMessage> GetColleges()
        {
            return await _httpClient.GetAsync("/api/College");
        }
        public async Task<HttpResponseMessage> GetCollegeById(int collegeId)
        {
            return await _httpClient.GetAsync($"/api/College/{collegeId}");
        }
        public async Task<HttpResponseMessage> EditCollege(int collegeId, College editedCollege)
        {
            return await _httpClient.PutAsJsonAsync($"api/College/{collegeId}", editedCollege);
        }

        public async Task<HttpResponseMessage> DeleteCollege(int collegeId)
        {
            return await _httpClient.DeleteAsync($"/api/College/{collegeId}");
        }
    }
}
