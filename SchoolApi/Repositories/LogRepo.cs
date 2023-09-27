using Microsoft.Extensions.FileSystemGlobbing.Internal;
using SchoolWebsite.shared;
using System.Collections.Concurrent;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace SchoolApi.Repos;

public class LogRepo
{
    private readonly ConcurrentQueue<(LogType LogType, string Data)> logQueue = new();
    private int CurrentFileCounter = 1;
    public LogRepo() => DequeueLog();

    public void Information(string data) => EnqueueLog(LogType.Information, data);
    public void Debug(string data) => EnqueueLog(LogType.Debug, data);
    public void Error(string data) => EnqueueLog(LogType.Error, data);
    public void Critical(string data) => EnqueueLog(LogType.Critical, data);

    private void EnqueueLog(LogType logType, string data)
        => logQueue.Enqueue((logType, data));

    private async Task DequeueLog()
    {
        while (true)
        {
            if (logQueue.TryDequeue(out var logData))
                await WriteLogToFile(logData.LogType, logData.Data);
            else
               await SleepLog();
        }
    }
    private async Task SleepLog() => await Task.Delay(6000);

    private async Task WriteLogToFile(LogType logType, string data)
    {
        data = ValidateData(data);
        string allocationPath = $"App_Data\\Loggers\\{logType}";

        if (!Directory.Exists(allocationPath))
            Directory.CreateDirectory(allocationPath);

        CurrentFileCounter = GetCurrentCounter(allocationPath, logType);

        string logPath = Path.Combine(allocationPath,
            $"{logType}log_{CurrentFileCounter}.json");

        if (File.Exists(logPath))
        {
            FileInfo fileInfo = new(logPath);
            if (fileInfo.Length > 2 * 1024)
            {
                CurrentFileCounter++;
                logPath = Path.Combine(allocationPath,
                    $"{logType}log_{CurrentFileCounter}.json");
            }
        }
        LogContent content = new() { Data = data, Date = DateTime.Now, Type = logType };
        string jsonContent = JsonSerializer.Serialize(content);
        await File.AppendAllTextAsync(logPath, jsonContent + Environment.NewLine);

    }

    private static int GetCurrentCounter(string allocationPath, LogType logType)
    {
        DirectoryInfo dirInfo = new(allocationPath);

        try
        {
            string lastFile = dirInfo
            .GetFiles($"{logType}log_*.json")
            .Last().Name;

            int underscore = lastFile.LastIndexOf('_');
            int dot = lastFile.LastIndexOf('.');

            return int.Parse(lastFile[(underscore + 1)..dot]);
        }
        catch
        {
            return 1;
        }
    }

    public async Task<List<List<LogContent>>> GetAllLogsAsync(string logType)
    {
        string allocationPath = $"App_Data\\Loggers\\{logType}";
        List<List<LogContent>> logFolder = new();

        if (!Directory.Exists(allocationPath))
            return logFolder;

        string[] matchingFiles = Directory.GetFiles(allocationPath);
        foreach (string file in matchingFiles)
        {
            List<LogContent> LogFile = new();
            foreach (string line in await File.ReadAllLinesAsync(file))
            {
                try
                {
                    LogContent content = JsonSerializer.Deserialize<LogContent>(line);
                    LogFile.Add(content);
                }
                catch (Exception)
                {

                    throw;
                }
            }
            logFolder.Add(LogFile);
        }

        return logFolder;
    }

    private string ValidateData(string data) => Regex.Replace(data, @"[^a-zA-Z0-9]+", " ");
}