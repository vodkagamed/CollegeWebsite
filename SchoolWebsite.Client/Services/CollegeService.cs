using SchoolWebsite.shared.Models;
using System.Net.Http;

namespace SchoolWebsite.Client.Services
{
    public class CollegeService : GenericService<College>
    {
        private readonly HttpClient _httpClient;
        public CollegeService(HttpClient httpClient) : base(httpClient)
        {
            Route = "College";
            _httpClient = httpClient;
        }
        public async Task<HttpResponseMessage> Create(College createdCollege)
        {
            return await _httpClient.PostAsJsonAsync("/api/College", createdCollege);
        }
    }
}
