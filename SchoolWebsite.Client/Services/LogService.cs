namespace SchoolWebsite.Client.Services;

public class LogService
{
    private readonly HttpClient _httpClient;

    public LogService(HttpClient httpClient)
    {
        this._httpClient = httpClient;
    }
    public async Task<HttpResponseMessage> InformationAsync(string message) =>
            await _httpClient.PostAsJsonAsync($"/api/Log/{LogType.Information}", message);
    public async Task<HttpResponseMessage> DebugAsync(string message) =>
        await _httpClient.PostAsJsonAsync($"/api/Log/{LogType.Debug}", message);
    public async Task<HttpResponseMessage> ErrorAsync(string message) =>
        await _httpClient.PostAsJsonAsync($"/api/Log/{LogType.Error}", message);
        public async Task<HttpResponseMessage> CriticalAsync(string message) =>
        await _httpClient.PostAsJsonAsync($"/api/Log/{LogType.Critical}", message);

    public async Task<HttpResponseMessage> GetAllAsync() =>
        await _httpClient.GetAsync($"/api/Log");
}
