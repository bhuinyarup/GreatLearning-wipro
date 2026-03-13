using SolidReporting.Abstractions;

namespace SolidReporting.Services;

public sealed class ReportSaver : IReportSaver
{
    private readonly IFileStore _fileStore;

    public ReportSaver(IFileStore fileStore)
    {
        _fileStore = fileStore;
    }

    public void Save(string filePath, string formattedReport)
    {
        _fileStore.SaveText(filePath, formattedReport);
    }
}
