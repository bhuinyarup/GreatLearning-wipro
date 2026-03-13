using SolidReporting.Abstractions;

namespace SolidReporting.Services;

public sealed class PdfReportFormattingStrategy : IReportFormattingStrategy
{
    public string Format(IReportContent report)
    {
        return $"[PDF]\nTitle: {report.Title}\nCreatedOn: {report.CreatedOn:O}\nBody: {report.Content}";
    }
}
