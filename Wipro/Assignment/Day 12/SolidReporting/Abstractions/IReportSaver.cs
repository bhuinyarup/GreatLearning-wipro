namespace SolidReporting.Abstractions;

public interface IReportSaver
{
    void Save(string filePath, string formattedReport);
}
