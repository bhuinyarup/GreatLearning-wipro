using SolidReporting.Abstractions;

namespace SolidReporting.Services;

public sealed class ReportFormatter
{
    private readonly IReportFormattingStrategy _strategy;

    public ReportFormatter(IReportFormattingStrategy strategy)
    {
        _strategy = strategy;
    }

    public string Format(IReportContent report)
    {
        return _strategy.Format(report);
    }
}
