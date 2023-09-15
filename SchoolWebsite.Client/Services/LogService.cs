namespace SchoolWebsite.Client.Services;

public class LogService
{
    private readonly HttpClient _httpClient;

    public LogService(HttpClient httpClient)
    {
        this._httpClient = httpClient;
    }
    public async virtual Task<HttpResponseMessage> Information(string message) =>
            await _httpClient.PostAsJsonAsync($"/api/Log/{LogType.Information}", message);
    public async virtual Task<HttpResponseMessage> Debug(string message) =>
        await _httpClient.PostAsJsonAsync($"/api/Log/{LogType.Debug}", message);
    public async virtual Task<HttpResponseMessage> Error(string message) =>
        await _httpClient.PostAsJsonAsync($"/api/Log/{LogType.Error}", message);
        public async virtual Task<HttpResponseMessage> Critical(string message) =>
        await _httpClient.PostAsJsonAsync($"/api/Log/{LogType.Critical}", message);

    public async Task<HttpResponseMessage> Get() =>
        await _httpClient.GetAsync($"/api/Log");
}
