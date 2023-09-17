using Microsoft.AspNetCore.Components;

namespace SchoolWebsite.Client.Services;

public class LogService
{
    private readonly HttpClient _httpClient = new() { BaseAddress = new Uri("https://localhost:7104") };
    public async Task<HttpResponseMessage> InformationAsync(string message) =>
            await _httpClient.PostAsJsonAsync($"/api/Log/{LogType.Information}", message);
    public async Task<HttpResponseMessage> DebugAsync(string message) =>
        await _httpClient.PostAsJsonAsync($"/api/Log/Debug/{LogType.Debug}", message);
    public async Task<HttpResponseMessage> ErrorAsync(string message) =>
        await _httpClient.PostAsJsonAsync($"/api/Log/{LogType.Error}", message);
        public async Task<HttpResponseMessage> CriticalAsync(string message) =>
        await _httpClient.PostAsJsonAsync($"/api/Log/{LogType.Critical}", message);
    public async Task<HttpResponseMessage> GetAllAsync(string logType) =>
        await _httpClient.GetAsync($"/api/Log/{logType}");
}
