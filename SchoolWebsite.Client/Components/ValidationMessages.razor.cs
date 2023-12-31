﻿using Microsoft.AspNetCore.Components;

namespace SchoolWebsite.Client.Components;
public partial class ValidationMessages
{

    [Parameter]
    public string Message { get; set; } = string.Empty;
    [Parameter]
    public string Verb { get; set; } = string.Empty;
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
                        break;
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
                if (Verb != string.Empty)
                {
                    Message = $"{Content} {Verb} successfully.";
                    Verb = string.Empty;
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
