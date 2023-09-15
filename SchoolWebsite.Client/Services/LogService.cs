using Microsoft.AspNetCore.Components;

namespace SchoolWebsite.Client.Services;

public class LogService
{
    private readonly HttpClient _httpClient;

    public LogService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<HttpResponseMessage> InformationAsync(string message) =>
            await _httpClient.PostAsJsonAsync($"/api/Log/Information", message);
    public async Task<HttpResponseMessage> DebugAsync(string message) =>
        await _httpClient.PostAsJsonAsync($"/api/Log/Debug", message);
    public async Task<HttpResponseMessage> ErrorAsync(string message) =>
        await _httpClient.PostAsJsonAsync($"/api/Log/Error", message);
        public async Task<HttpResponseMessage> CriticalAsync(string message) =>
        await _httpClient.PostAsJsonAsync($"/api/Log/Critical", message);

    public async Task<HttpResponseMessage> GetAllAsync(string logType) =>
        await _httpClient.GetAsync($"/api/Log/{logType}");
}
