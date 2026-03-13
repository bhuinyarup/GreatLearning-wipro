namespace SolidReporting.Models;

public sealed class InventoryReport : Report
{
    public InventoryReport(string title, string content) : base(title, content)
    {
    }

    public override string GetSummary()
    {
        return $"Inventory report '{Title}' prepared with {Content.Length} characters.";
    }
}
