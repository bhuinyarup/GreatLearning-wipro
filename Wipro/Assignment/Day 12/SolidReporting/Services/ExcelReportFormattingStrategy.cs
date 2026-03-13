using SolidReporting.Abstractions;

namespace SolidReporting.Services;

public sealed class ExcelReportFormattingStrategy : IReportFormattingStrategy
{
    public string Format(IReportContent report)
    {
        return $"[EXCEL],{report.Title},{report.CreatedOn:O},{report.Content}";
    }
}
