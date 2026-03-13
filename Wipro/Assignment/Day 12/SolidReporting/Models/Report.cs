using SolidReporting.Abstractions;

namespace SolidReporting.Models;

public abstract class Report : IReportContent, ISummarizableReport
{
    protected Report(string title, string content)
    {
        Title = title;
        Content = content;
        CreatedOn = DateTime.UtcNow;
    }

    public string Title { get; }
    public string Content { get; }
    public DateTime CreatedOn { get; }

    public virtual string GetSummary()
    {
        var shortContent = Content.Length <= 30 ? Content : Content[..30] + "...";
        return $"{Title}: {shortContent}";
    }
}
