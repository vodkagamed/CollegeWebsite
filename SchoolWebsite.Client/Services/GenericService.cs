using SchoolWebsite.shared.Models;

namespace SchoolWebsite.Client.Services
{
    public class GenericService<T>
    {
        private readonly HttpClient _httpClient;
        public string EndPoint { get; set; }
        public GenericService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            EndPoint = string.Empty;
        }

        public async virtual Task<HttpResponseMessage> Create(int collegeId, T createdOjb) =>
            await _httpClient.PostAsJsonAsync($"/api/{EndPoint}/{collegeId}", createdOjb);

        public async Task<HttpResponseMessage> Get() =>
            await _httpClient.GetAsync($"/api/{EndPoint}");

        public async Task<HttpResponseMessage> GetById(int objId) =>
            await _httpClient.GetAsync($"/api/{EndPoint}/{objId}");

        public async Task<HttpResponseMessage> Edit(int objId, T editedObj) =>
            await _httpClient.PutAsJsonAsync($"/api/{EndPoint}/{objId}", editedObj);

        public async Task<HttpResponseMessage> Delete(int objId) =>
            await _httpClient.DeleteAsync($"/api/{EndPoint}/{objId}");
    }
}
