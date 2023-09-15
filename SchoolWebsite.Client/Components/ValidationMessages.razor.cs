using Microsoft.AspNetCore.Components;
using SchoolWebsite.Shared;

namespace SchoolWebsite.Client.Components;
public partial class ValidationMessages
{
    [Parameter]
    public string message { get; set; } = string.Empty;
    [Parameter]
    public string AlertClass { get; set; } = string.Empty;
    [Inject]
    public LogService _logService { get; set; } = default!;
    public async Task<bool> PerformHttpRequest(HttpMethod httpMethod, HttpResponseMessage response,string Content)
    {
        try
        {
            if (response.IsSuccessStatusCode)
            {
                AlertClass = "alert-success";
                bool success = true;
                switch (httpMethod.Method)
                {
                    case "GET":
                        break; // No need to set the message for GET
                    case "DELETE":
                        message = $"{Content} deleted successfully.";
                        break;
                    case "PUT":
                        message = $"{Content} modified successfully.";
                        break;
                    case "POST":
                        message = $"{Content} created successfully.";
                        break;
                    default:
                        message = "Unsupported HTTP method.";
                        AlertClass = "alert-danger";
                        success = false;
                        break;
                }
                if (message == string.Empty)
                   await _logService.InformationAsync($"{Content} retrieved successfully");
                else
                    await _logService.InformationAsync(message);

                return success;
            }
            else
            {
                message = $"Error: {response.StatusCode}";
                AlertClass = "alert-danger";
                await _logService.ErrorAsync(message);
                return false;
            }
        }
        catch (HttpRequestException ex)
        {
            string exeption = $"HTTP request error: {ex.Message}";
            Console.WriteLine(exeption);
            await _logService.DebugAsync(exeption);
            message = "An error occurred.";
            AlertClass = "alert-danger";
            return false;
        }
        catch (Exception ex)
        {
            string exeption = $"An error occurred: {ex.Message}";
            Console.WriteLine(exeption);
            await _logService.CriticalAsync(exeption);
            message = "An error occurred.";
            AlertClass = "alert-danger";
            return false;
        }
    }
}
