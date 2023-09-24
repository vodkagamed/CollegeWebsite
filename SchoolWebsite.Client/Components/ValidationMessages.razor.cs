using Microsoft.AspNetCore.Components;
<<<<<<< HEAD
=======
using SchoolWebsite.Shared;
<<<<<<< HEAD
>>>>>>> 37fa884f27cdf29ecd9e93280e5635e06b8bb4fe
=======
>>>>>>> 37fa884f27cdf29ecd9e93280e5635e06b8bb4fe
namespace SchoolWebsite.Client.Components;
public partial class ValidationMessages
{

    [Parameter]
    public string Message { get; set; } = string.Empty;
    [Parameter]
    public string AlertClass { get; set; } = string.Empty;
    [Inject]
    public LogService _logService { get; set; } = new();
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
                        Message = $"{Content} deleted successfully.";
                        break;
                    case "PUT":
                        Message = $"{Content} modified successfully.";
                        break;
                    case "POST":
                        Message = $"{Content} created successfully.";
                        break;
                    default:
                        Message = "Unsupported HTTP method.";
                        AlertClass = "alert-danger";
                        success = false;
                        break;
                }
                if (Message == string.Empty)
                   await _logService.InformationAsync($"{Content} retrieved successfully");
                
                else
                    await _logService.InformationAsync(Message);

                return success;
            }
            else
            {
                Message = $"Error: {response.StatusCode}";
                AlertClass = "alert-danger";
                await _logService.ErrorAsync(Message);
                return false;
            }
        }
        catch (HttpRequestException ex)
        {
            string exeption = $"HTTP request error: {ex.Message}";
            Console.WriteLine(exeption);
            await _logService.DebugAsync(exeption);
            Message = "An error occurred.";
            AlertClass = "alert-danger";
            return false;
        }
        catch (Exception ex)
        {
            string exeption = $"An error occurred: {ex.Message}";
            Console.WriteLine(exeption);
            await _logService.CriticalAsync(exeption);
            Message = "An error occurred.";
            AlertClass = "alert-danger";
            return false;
        }
    }
}
