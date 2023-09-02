using Microsoft.AspNetCore.Components;

namespace SchoolWebsite.Client.Pages
{
    public partial class Logs
    {
        private List<List<LogContent>> logFiles = new();
        private List<List<LogContent>> customlogFiles = new();
        private List<DateTime> dates = new();
        private bool loadingLogFiles = false;
        private DateTime selectedDate;
        private LogType selectedLogType = LogType.Information;
        private string alertType = "info";
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
            logFiles = await Log.GetAllLogsAsync(selectedLogType);
            customlogFiles = logFiles;
            loadingLogFiles = false;

            if (selectedLogType == LogType.Error) alertType = "warning";
            else if (selectedLogType == LogType.Critical) alertType = "danger";
            else alertType = "info";
            LoadDates();
            InvokeAsync(StateHasChanged);
        }
        private void GetLogsByDate(ChangeEventArgs e)
        {
            if (e.Value.ToString() == "All")
                customlogFiles = logFiles;
            else
            {
                DateTime.TryParse(e.Value.ToString(), out DateTime date);
                selectedDate = date;
                customlogFiles = logFiles
                    .Select(logFile => logFile
                    .Where(log => log.Date.Date == selectedDate.Date)
                    .ToList())
                    .ToList();
            }
            InvokeAsync(StateHasChanged);
        }

        private int currentIndex = 0;

        private void NavigateToLog(int newIndex)
        {
            if (newIndex >= 0 && newIndex < logFiles.Count)
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
        }



    }
}
