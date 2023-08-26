using Microsoft.AspNetCore.Components;
using Microsoft.VisualBasic;
using SchoolWebsite.shared;
using System.Security.Cryptography.X509Certificates;

namespace SchoolWebsite.Client.Pages
{
    public partial class Logs
    {
        private List<string> logs = new();
        private bool loadingLogs = false;

        private LogType selectedLogType = LogType.Information;
        protected override async Task OnInitializedAsync()
        {
            await LoadLogs(null);
        }

        private async Task LoadLogs(ChangeEventArgs e)
        {
            if (e != null && Enum.TryParse<LogType>(e.Value?.ToString(), out var logType))
               selectedLogType = logType;
            
            loadingLogs = true;
            logs = await Log.GetAllLogsAsync(selectedLogType);
            loadingLogs = false;
        }

        private int currentIndex = 0;

        private void NavigateToLog(int newIndex)
        {
            if (newIndex >= 0 && newIndex < logs.Count)
                currentIndex = newIndex;
        }
    }
}
