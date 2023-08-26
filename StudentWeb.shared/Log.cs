namespace SchoolWebsite.shared
{
    public static class Log
    {
        public static async Task Information(string data) => await WriteLog(LogType.Information, data);
        public static async Task Debug(string data) => await WriteLog(LogType.Debug, data);
        public static async Task Error(string data) => await WriteLog(LogType.Error, data);
        public static async Task Critical(string data) => await WriteLog(LogType.Critical, data);
        public static async Task<List<string>> GetAllLogsAsync(LogType logType)
        {
            string allocationPath = $"App_Data\\Loggers\\{logType}";
            List<string> logContents = new();

            if (!Directory.Exists(allocationPath))
                return logContents;

            string searchPattern = $"{logType}log_*.txt";
            string[] matchingFiles = Directory.GetFiles(allocationPath, searchPattern);
            foreach (string filePath in matchingFiles)
            {
                string content = await File.ReadAllTextAsync(filePath);
                logContents.Add(content);
            }
            return logContents;
        }
        private static string lineSeparator = "=================================================";
        private static async Task WriteLog(LogType logType, string data)
        {
            string allocationPath = $"App_Data\\Loggers\\{logType}";

            if (!Directory.Exists(allocationPath))
                Directory.CreateDirectory(allocationPath);

            string logPath = Path.Combine(allocationPath,
                $"{logType}log_{getFileCounter(logType)}.txt");

            if (File.Exists(logPath))
            {
                FileInfo fileInfo = new FileInfo(logPath);
                if (fileInfo.Length > 4 * 1024)
                   UpdateFileCounter(logType);
            }

            string content = $"Log date: {DateTime.Now}\nLog:{data}\n{lineSeparator}\n\n";
            await File.AppendAllTextAsync(logPath, content);
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
