using SolidReporting.Abstractions;

namespace SolidReporting.Services;

public sealed class ReportService
{
    private readonly IReportGenerator _reportGenerator;
    private readonly IReportFormattingStrategy _formattingStrategy;
    private readonly IReportSaver _reportSaver;

    public ReportService(
        IReportGenerator reportGenerator,
        IReportFormattingStrategy formattingStrategy,
        IReportSaver reportSaver)
    {
        _reportGenerator = reportGenerator;
        _formattingStrategy = formattingStrategy;
        _reportSaver = reportSaver;
    }

    public string CreateAndSaveReport(string reportType, string sourceData, string outputPath)
    {
        var report = _reportGenerator.Generate(reportType, sourceData);
        var formatted = _formattingStrategy.Format(report);
        _reportSaver.Save(outputPath, formatted);
        return formatted;
    }
}
