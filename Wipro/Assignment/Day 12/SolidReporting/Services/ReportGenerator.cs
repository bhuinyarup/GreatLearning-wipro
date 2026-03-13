using SolidReporting.Abstractions;
using SolidReporting.Models;

namespace SolidReporting.Services;

public sealed class ReportGenerator : IReportGenerator
{
    public Report Generate(string reportType, string sourceData)
    {
        return reportType.ToLowerInvariant() switch
        {
            "sales" => new SalesReport("Sales Summary", sourceData),
            "inventory" => new InventoryReport("Inventory Snapshot", sourceData),
            _ => throw new ArgumentException($"Unsupported report type: {reportType}", nameof(reportType))
        };
    }
}
