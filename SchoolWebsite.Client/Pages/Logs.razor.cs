﻿using Microsoft.AspNetCore.Components;

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
        protected override async Task OnInitializedAsync()
        {
            await LoadLogFiles(null);
            dates = LoadDates();
            selectedDate = dates[0];
        }

        private async Task LoadLogFiles(ChangeEventArgs e)
        {
            if (e != null && Enum.TryParse<LogType>(e.Value?.ToString(), out var logType))
                selectedLogType = logType;

            loadingLogFiles = true;
            logFiles = await Log.GetAllLogsAsync(selectedLogType);
            customlogFiles = logFiles;
            loadingLogFiles = false;
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
                .OrderByDescending(date=>date)
                .ToList();

            return allDates;
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
                    .Select(logFile => logFile.Where(log => log.Date.Date == selectedDate.Date).ToList())
                    .ToList();
            }
        }


    }
}
