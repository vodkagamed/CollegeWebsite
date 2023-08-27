using Microsoft.VisualBasic;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace SchoolWebsite.shared
{
    public static class Log
    {
        public static async Task Information(string data) => await WriteLog(LogType.Information, data);
        public static async Task Debug(string data) => await WriteLog(LogType.Debug, data);
        public static async Task Error(string data) => await WriteLog(LogType.Error, data);
        public static async Task Critical(string data) => await WriteLog(LogType.Critical, data);
        public static async Task <List<List<LogContent>>> GetAllLogsAsync(LogType logType)
        {
            string allocationPath = $"App_Data\\Loggers\\{logType}";
            List<List<LogContent>> logFile = new();

            if (!Directory.Exists(allocationPath))
                return logFile;

            List<LogContent> contents = new();
            string searchPattern = $"{logType}log_*.json";
            string[] matchingFiles = Directory.GetFiles(allocationPath, searchPattern);
            foreach (string file in matchingFiles)
            {
                foreach (string line in await File.ReadAllLinesAsync(file))
                {
                    LogContent content = JsonSerializer.Deserialize<LogContent>(line);
                    contents.Add(content);
                }
                logFile.Add(contents);
            }
            return logFile;
        }
        private static async Task WriteLog(LogType logType, string data)
        {
            string allocationPath = $"App_Data\\Loggers\\{logType}";

            if (!Directory.Exists(allocationPath))
                Directory.CreateDirectory(allocationPath);

            string logPath = Path.Combine(allocationPath,
                $"{logType}log_{getFileCounter(logType)}.json");

            if (File.Exists(logPath))
            {
                FileInfo fileInfo = new FileInfo(logPath);
                if (fileInfo.Length > 4 * 1024)
                   UpdateFileCounter(logType);
            }

            LogContent content = new LogContent { Data = data, Date = DateTime.Now, Type = logType };
            string jsonContent = JsonSerializer.Serialize(content);
            await File.AppendAllTextAsync(logPath, jsonContent + Environment.NewLine);
        }

        private static int[] counters = { 0, 0, 0, 0 };


        private static string getFileCounter(LogType type)
        {
            string counterFilePath = Path.Combine("App_Data", "Loggers", "counterFile.txt"); // Update the path as needed

            if (File.Exists(counterFilePath))
                counters = Array.ConvertAll(File.ReadAllLines(counterFilePath), int.Parse);
            else
                File.WriteAllLines(counterFilePath, Array.ConvertAll(counters, c => c.ToString()));
            return counters[(int)type].ToString();
        }

        private static void UpdateFileCounter(LogType type)
        {
            string counterFilePath = Path.Combine("App_Data", "Loggers", "counterFile.txt"); // Update the path as needed

            counters[(int)type]++;
            File.WriteAllLines(counterFilePath, Array.ConvertAll(counters, c => c.ToString()));
        }
    }
}
