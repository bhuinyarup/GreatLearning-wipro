using System.Text;

namespace W2Day5AssignmentSolution.Logging;

public sealed class FileAppLogger : IAppLogger
{
    private readonly string _logFilePath;
    private readonly object _sync = new();

    public FileAppLogger(string logFilePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(logFilePath);
        _logFilePath = logFilePath;

        var directory = Path.GetDirectoryName(logFilePath);
        if (!string.IsNullOrWhiteSpace(directory))
        {
            Directory.CreateDirectory(directory);
        }
    }

    public void Info(string message)
    {
        Write("INFO", message);
    }

    public void Error(string message, Exception exception)
    {
        var safeMessage = $"{message}{Environment.NewLine}{exception.Message}{Environment.NewLine}{exception.StackTrace}";
        Write("ERROR", safeMessage);
    }

    private void Write(string level, string message)
    {
        lock (_sync)
        {
            var line = $"{DateTimeOffset.UtcNow:O} [{level}] {message}";
            File.AppendAllText(_logFilePath, line + Environment.NewLine, Encoding.UTF8);
        }
    }
}
