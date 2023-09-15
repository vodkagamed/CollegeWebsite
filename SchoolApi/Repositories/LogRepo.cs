using SchoolWebsite.shared;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolApi.Repos;

public class LogRepo
{
    private readonly ConcurrentQueue<(LogType LogType, string Data)> logQueue = new();
    private int CurrentCounter = 1;
    public LogRepo() => DequeueLog();

    public void Information(string data) => EnqueueLog(LogType.Information, data);
    public void Debug(string data) => EnqueueLog(LogType.Debug, data);
    public void Error(string data) => EnqueueLog(LogType.Error, data);
    public void Critical(string data) => EnqueueLog(LogType.Critical, data);

    private void EnqueueLog(LogType logType, string data) =>  logQueue.Enqueue((logType, data));

    private async Task DequeueLog()
    {
        while (true)
        {
            if (logQueue.TryDequeue(out var logData))
            {
                await WriteLogToFile(logData.LogType, logData.Data);
            }
            else
            {
                await Task.Delay(6000);
            }
        }
    }


    private async Task WriteLogToFile(LogType logType, string data)
    {
        string allocationPath = $"App_Data\\Loggers\\{logType}";

        if (!Directory.Exists(allocationPath))
            Directory.CreateDirectory(allocationPath);

        CurrentCounter = GetCurrentCounter(allocationPath, logType);

        string logPath = Path.Combine(allocationPath,
            $"{logType}log_{CurrentCounter}.json");

        if (File.Exists(logPath))
        {
            FileInfo fileInfo = new(logPath);
            if (fileInfo.Length > 2 * 1024)
                CurrentCounter++;
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
            .OrderByDescending(file => file.CreationTime)
            .First().Name;

            int underscore = lastFile.LastIndexOf('_');
            int dot = lastFile.LastIndexOf('.');
            Console.WriteLine(lastFile.Length);
            string CounterString = lastFile.Substring(underscore + 1, dot - underscore - 1);
            return int.Parse(CounterString);
        }
        catch
        {

            return 1;
        }
    }

    public async Task<List<List<LogContent>>> GetAllLogsAsync(LogType logType)
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


}
    //private static Dictionary<string, int> counters = new()
    //{
    //    {"Infromation",0 },
    //    {"Debug",0 },
    //    {"Error",0 },
    //    {"Critical",0 }
    //};

    //private static string GetFileCounter(LogType type)
    //{
    //    string counterFilePath = Path.Combine("App_Data", "Loggers", "counterFile.txt");

    //    if (File.Exists(counterFilePath))
    //        counters = File.ReadAllText(counterFilePath).ToDictionary<string,int>;
    //    else
    //        File.WriteAllLines(counterFilePath, Array.ConvertAll(counters, c => c.ToString()));
    //    return counters[(int)type].ToString();
    //}

    //private static void UpdateFileCounter(LogType type)
    //{
    //    string counterFilePath = Path.Combine("App_Data", "Loggers", "counterFile.txt");

    //    counters[(int)type]++;
    //    File.WriteAllLines(counterFilePath, Array.ConvertAll(counters, c => c.ToString()));
    //}

