namespace SolidReporting.Abstractions;

public interface IReportFormattingStrategy
{
    string Format(IReportContent report);
}
