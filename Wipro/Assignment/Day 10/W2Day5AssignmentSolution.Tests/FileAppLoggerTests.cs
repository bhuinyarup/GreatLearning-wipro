using W2Day5AssignmentSolution.Logging;

namespace W2Day5AssignmentSolution.Tests;

public sealed class FileAppLoggerTests
{
    [Fact]
    public void Logger_Should_Write_Info_And_Error_To_File()
    {
        var logPath = Path.Combine(Path.GetTempPath(), $"w2-day5-{Guid.NewGuid():N}.log");

        try
        {
            var logger = new FileAppLogger(logPath);

            logger.Info("Operation completed");
            logger.Error("Operation failed", new InvalidOperationException("boom"));

            var content = File.ReadAllText(logPath);
            Assert.Contains("[INFO]", content);
            Assert.Contains("Operation completed", content);
            Assert.Contains("[ERROR]", content);
            Assert.Contains("Operation failed", content);
            Assert.Contains("boom", content);
        }
        finally
        {
            if (File.Exists(logPath))
            {
                File.Delete(logPath);
            }
        }
    }
}
