namespace SolidReporting.Models;

public sealed class SalesReport : Report
{
    public SalesReport(string title, string content) : base(title, content)
    {
    }

    public override string GetSummary()
    {
        return $"Sales report '{Title}' has {Content.Length} characters of sales data.";
    }
}
