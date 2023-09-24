using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace SchoolWebsite.Client.Pages;
public partial class Logs
{
    private List<List<LogContent>> AllLogFiles = new();
    private List<List<LogContent>> customlogFiles = new();
    private List<DateTime> dates = new();
    private bool loadingLogFiles = false;
    private DateTime selectedDate;
    private LogType selectedLogType = LogType.Information;
    private string alertType = "info";
    [Inject]
    LogService _logService { get; set; }
    protected override async Task OnInitializedAsync()
    {
        await GetLogsByType(null);
        LoadDates();
        selectedDate = dates[0];
    }

    private async Task GetLogsByType(ChangeEventArgs e)
    {
        if (e != null && Enum.TryParse<LogType>(e.Value?.ToString(), out var logType))
            selectedLogType = logType;

        loadingLogFiles = true;
        HttpResponseMessage response = await _logService.GetAllAsync(selectedLogType.ToString());
        if (response.IsSuccessStatusCode)
            AllLogFiles = await response.Content.ReadFromJsonAsync<List<List<LogContent>>>() ?? new();
        customlogFiles = AllLogFiles;
        loadingLogFiles = false;

        if (selectedLogType == LogType.Error) alertType = "warning";
        else if (selectedLogType == LogType.Critical) alertType = "danger";
        else alertType = "info";

        LoadDates();
        await InvokeAsync(StateHasChanged);
    }
    private void GetLogsByDate(ChangeEventArgs e)
    {
        if (e.Value.ToString() == "All")
            customlogFiles = AllLogFiles;
        else
        {
            DateTime.TryParse(e.Value.ToString(), out DateTime date);
            selectedDate = date;
            customlogFiles = AllLogFiles
                .Select
                (
                    logFile => logFile
                    .Where(log => log.Date.Date == selectedDate.Date)
                    .OrderDescending()
                    .ToList()
                )
                .ToList();
        }
        InvokeAsync(StateHasChanged);
    }

    private int currentIndex = 0;

    private void NavigateToLog(int newIndex)
    {
        if (newIndex >= 0 && newIndex < AllLogFiles.Count)
            currentIndex = newIndex;
    }

    private void LoadDates()
    {
        var allDates = customlogFiles
            .SelectMany(logFile => logFile.Select(logContent => logContent.Date.Date))
            .Distinct()
            .OrderByDescending(date => date)
            .ToList();

        dates = allDates;
        InvokeAsync(StateHasChanged);

        currentIndex = 0;
    }



}
