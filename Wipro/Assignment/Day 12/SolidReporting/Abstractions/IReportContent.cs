namespace SolidReporting.Abstractions;

public interface IReportContent
{
    string Title { get; }
    string Content { get; }
    DateTime CreatedOn { get; }
}
