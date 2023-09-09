using Microsoft.AspNetCore.Components;
using SchoolWebsite.Shared;

namespace SchoolWebsite.Client.Components;
public partial class ValidationMessages
{
    [Parameter]
    public string message { get; set; } = string.Empty;
    [Parameter]
    public string alertClass { get; set; } = string.Empty;
    public async Task<bool> PerformHttpRequest(HttpMethod httpMethod, HttpResponseMessage response,string Content)
    {
        try
        {
            if (response.IsSuccessStatusCode)
            {
                alertClass = "alert-success";
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
                        alertClass = "alert-danger";
                        success = false;
                        break;
                }
                if (message == string.Empty)
                    await Log.Information("Data retrieved successfully");
                else
                    await Log.Information(message);

                return success;
            }
            else
            {
                message = $"Error: {response.StatusCode}";
                alertClass = "alert-danger";
                await Log.Error(message);
                return false;
            }
        }
        catch (HttpRequestException ex)
        {
            string exeption = $"HTTP request error: {ex.Message}";
            Console.WriteLine(exeption);
            await Log.Debug(exeption);
            message = "An error occurred.";
            alertClass = "alert-danger";
            return false;
        }
        catch (Exception ex)
        {
            string exeption = $"An error occurred: {ex.Message}";
            Console.WriteLine(exeption);
            await Log.Critical(exeption);
            message = "An error occurred.";
            alertClass = "alert-danger";
            return false;
        }
    }
}
