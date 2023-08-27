using Microsoft.AspNetCore.Components;
using Microsoft.VisualBasic;
using SchoolWebsite.shared;
using System.Security.Cryptography.X509Certificates;

namespace SchoolWebsite.Client.Pages
{
    public partial class Logs
    {
        private List<List<LogContent>> logFiles = new();
        private List<LogContent> logs = new();
        private List<DateTime> dates = new();
        private bool loadingLogs = false;

        private LogType selectedLogType = LogType.Information;
        protected override async Task OnInitializedAsync()
        {
            await LoadLogs(null);
            dates = LoadDates();
        }

        private async Task LoadLogs(ChangeEventArgs e)
        {
            if (e != null && Enum.TryParse<LogType>(e.Value?.ToString(), out var logType))
                selectedLogType = logType;

            loadingLogs = true;
            logFiles = await Log.GetAllLogsAsync(selectedLogType);
            loadingLogs = false;
        }

        private int currentIndex = 0;

        private void NavigateToLog(int newIndex)
        {
            if (newIndex >= 0 && newIndex < logFiles.Count)
                currentIndex = newIndex;
        }

        private List<DateTime> LoadDates()
        {
            var allDates = logFiles
                .SelectMany(logFile => logFile.Select(logContent => logContent.Date.Date))
                .Distinct()
                .OrderBy(date => date)
                .ToList();

            return allDates;
        }

    }
}
