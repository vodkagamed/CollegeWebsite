using SchoolWebsite.shared;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolWebsite.Shared
{
    public static class Log
    {
        private static readonly ConcurrentQueue<(LogType LogType, string Data)> logQueue = new();
        static Log() => StartLoggingTask();

        private static async Task WriteLog(LogType logType, string data)
        {
                logQueue.Enqueue((logType, data));
                StartLoggingTask();
        }
        private static void StartLoggingTask()
        {
            Task.Run(async () =>
            {
                while (logQueue.TryDequeue(out var logData))
                    await WriteLogToFile(logData.LogType, logData.Data);    
            });
        }

        public static async Task Information(string data) => await WriteLog(LogType.Information, data);
        public static async Task Debug(string data) => await WriteLog(LogType.Debug, data);
        public static async Task Error(string data) => await WriteLog(LogType.Error, data);
        public static async Task Critical(string data) => await WriteLog(LogType.Critical, data);

        public static async Task<List<List<LogContent>>> GetAllLogsAsync(LogType logType)
        {
            string allocationPath = $"App_Data\\Loggers\\{logType}";
            List<List<LogContent>> logFile = new();

            if (!Directory.Exists(allocationPath))
                return logFile;

            string searchPattern = $"{logType}log_*.json";
            string[] matchingFiles = Directory.GetFiles(allocationPath, searchPattern);
            foreach (string file in matchingFiles)
            {
                List<LogContent> contents = new List<LogContent>();
                foreach (string line in await File.ReadAllLinesAsync(file))
                {
                    LogContent content = JsonSerializer.Deserialize<LogContent>(line);
                    contents.Add(content);
                }
                logFile.Add(contents);
            }
            return logFile;
        }


        private static async Task WriteLogToFile(LogType logType, string data)
        {
            string allocationPath = $"App_Data\\Loggers\\{logType}";

            if (!Directory.Exists(allocationPath))
                Directory.CreateDirectory(allocationPath);

            string logPath = Path.Combine(allocationPath,
                $"{logType}log_{GetFileCounter(logType)}.json");

            if (File.Exists(logPath))
            {
                FileInfo fileInfo = new(logPath);
                if (fileInfo.Length > 2 * 1024)
                    UpdateFileCounter(logType);
            }

            LogContent content = new() { Data = data, Date = DateTime.Now, Type = logType };
            string jsonContent = JsonSerializer.Serialize(content);
            await File.AppendAllTextAsync(logPath, jsonContent + Environment.NewLine);
        }

        private static int[] counters = { 0, 0, 0, 0 };

        private static string GetFileCounter(LogType type)
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
