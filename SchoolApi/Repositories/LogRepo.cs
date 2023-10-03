﻿namespace SchoolApi.Repos;
public class LogRepo
{
    private readonly ConcurrentQueue<(string subscriberName, LogType LogType, string Data)> logQueue = new();
    private int CurrentFileCounter = 1;
    public LogRepo() => DequeueAndWriteLog();

    public void Information(string subscriberName, string data)
        => EnqueueLog(subscriberName, LogType.Information, data);
    public void Debug(string subscriberName, string data)
        => EnqueueLog(subscriberName, LogType.Debug, data);
    public void Error(string subscriberName, string data)
        => EnqueueLog(subscriberName, LogType.Error, data);
    public void Critical(string subscriberName, string data)
        => EnqueueLog(subscriberName, LogType.Critical, data);

    private void EnqueueLog(string subscriberName, LogType logType, string data)
        => logQueue.Enqueue((subscriberName, logType, data));

    private async Task DequeueAndWriteLog()
    {
        while (true)
        {
            if (logQueue.TryDequeue(out var logData))
                await WriteLogToFile(logData.subscriberName, logData.LogType, logData.Data);
            else
                await SleepLog();
        }
    }
    private static async Task SleepLog() => await Task.Delay(6000);

    private async Task WriteLogToFile(string SubscriberName, LogType logType, string data)
    {
        data = ValidateData(data);
        string allocationPath = $"App_Data\\Loggers\\{SubscriberName}\\{logType}";

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

        using (StreamWriter streamWriter = new (logPath, true))
        {
            await streamWriter.WriteLineAsync(jsonContent);
        }
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

    public async Task<List<List<LogContent>>> GetAllLogsAsync(string subscriberName, string logType)
    {
        string allocationPath = $"App_Data\\Loggers\\{subscriberName}\\{logType}";
        List<List<LogContent>> logFolder = new();

        if (!Directory.Exists(allocationPath))
            return logFolder;

        string[] matchingFiles = Directory.GetFiles(allocationPath);
        foreach (string file in matchingFiles)
        {
            List<LogContent> LogFile = new();
            using (StreamReader streamReader = new (file))
            {
                while (!streamReader.EndOfStream)
                {
                    string line = await streamReader.ReadLineAsync();
                    LogContent content = JsonSerializer.Deserialize<LogContent>(line);
                    LogFile.Add(content);

                }
            }
            logFolder.Add(LogFile);
        }

        return logFolder;
    }

    private string ValidateData(string data) => Regex.Replace(data, @"[^a-zA-Z0-9]+", " ");
}