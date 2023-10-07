using SchoolWebsite.shared;

namespace SchoolApi.Repos;
public class LogRepo
{
    public struct LogData
    {
        public string subscriberName;
        public LogType logType;
        public string data;
        public DateTime date;
    }
    private readonly ConcurrentQueue<LogData> logQueue = new();
    private int CurrentFileCounter = 1;
    public LogRepo() => DequeueAndWriteLog();

    public void Information(string subscriberName, string data)
        => EnqueueLog(new LogData { subscriberName = subscriberName, logType = LogType.Information, data = data, date=DateTime.Now });
    public void Debug(string subscriberName, string data)
        => EnqueueLog(new LogData { subscriberName = subscriberName, logType = LogType.Debug, data = data,
            date = DateTime.Now });
    public void Error(string subscriberName, string data)
        => EnqueueLog(new LogData { subscriberName = subscriberName, logType = LogType.Error, data = data,
            date = DateTime.Now });
    public void Critical(string subscriberName, string data)
        => EnqueueLog(new LogData { subscriberName = subscriberName, logType = LogType.Critical, data = data,
            date = DateTime.Now });

    private void EnqueueLog(LogData logData) => logQueue.Enqueue(logData);

    private async Task DequeueAndWriteLog()
    {

        while (true)
        {
            if (logQueue.TryDequeue(out LogData logData))
                await WriteLogToFile(logData);
            else
                await SleepLog();
        }
    }
    private static async Task SleepLog() => await Task.Delay(6000);

    private async Task WriteLogToFile(LogData logData)
    {
        logData.data = ValidateData(logData.data);
        string allocationPath = $"App_Data\\Loggers\\{logData.subscriberName}\\{logData.logType}";

        if (!Directory.Exists(allocationPath))
            Directory.CreateDirectory(allocationPath);

        CurrentFileCounter = GetCurrentCounter(allocationPath, logData.logType);

        string logPath = Path.Combine(allocationPath,
        $"{logData.logType}log_{CurrentFileCounter}.json");

        if (File.Exists(logPath))
        {
            FileInfo fileInfo = new(logPath);
            if (fileInfo.Length > 2 * 1024)
            {
                CurrentFileCounter++;
                logPath = Path.Combine(allocationPath,
                $"{logData.logType}log_{CurrentFileCounter}.json");
            }
        }

        LogContent content = new() { Data = logData.data, Date = logData.date, Type = logData.logType };
        string jsonContent = JsonSerializer.Serialize(content);

        using (StreamWriter streamWriter = new(logPath, true))
        {
            await streamWriter.WriteLineAsync(jsonContent);
        }
    }
    private string ValidateData(string data) => Regex.Replace(data, @"[^a-zA-Z0-9]+", " ");

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
            using (StreamReader streamReader = new(file))
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

}