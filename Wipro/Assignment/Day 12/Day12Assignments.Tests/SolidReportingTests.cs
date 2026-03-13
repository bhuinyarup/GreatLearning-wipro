using SolidReporting.Abstractions;
using SolidReporting.Models;
using SolidReporting.Services;

namespace Day12Assignments.Tests;

public class SolidReportingTests
{
    [Fact]
    public void ReportGenerator_ReturnsExpectedReportType()
    {
        IReportGenerator generator = new ReportGenerator();

        var sales = generator.Generate("sales", "sales-data");
        var inventory = generator.Generate("inventory", "inventory-data");

        Assert.IsType<SalesReport>(sales);
        Assert.IsType<InventoryReport>(inventory);
    }

    [Fact]
    public void ReportFormatter_UsesPdfAndExcelStrategiesWithoutModification()
    {
        var report = new SalesReport("Test", "Content");
        var pdfFormatter = new ReportFormatter(new PdfReportFormattingStrategy());
        var excelFormatter = new ReportFormatter(new ExcelReportFormattingStrategy());

        var pdf = pdfFormatter.Format(report);
        var excel = excelFormatter.Format(report);

        Assert.StartsWith("[PDF]", pdf);
        Assert.StartsWith("[EXCEL]", excel);
    }

    [Fact]
    public void ReportHierarchy_SatisfiesLspThroughBaseTypeUsage()
    {
        Report sales = new SalesReport("Sales", "A");
        Report inventory = new InventoryReport("Inventory", "B");

        var summaries = new[] { sales.GetSummary(), inventory.GetSummary() };

        Assert.All(summaries, summary => Assert.False(string.IsNullOrWhiteSpace(summary)));
    }

    [Fact]
    public void InterfaceSegregation_AllowsFocusedContracts()
    {
        var report = new SalesReport("Sales", "Body");

        IReportContent content = report;
        ISummarizableReport summary = report;

        Assert.Equal("Sales", content.Title);
        Assert.Contains("Sales report", summary.GetSummary());
    }

    [Fact]
    public void ReportService_DependsOnAbstractionsAndSavesFormattedOutput()
    {
        var fakeStore = new InMemoryFileStore();
        IReportSaver saver = new ReportSaver(fakeStore);
        var service = new ReportService(
            new ReportGenerator(),
            new PdfReportFormattingStrategy(),
            saver);

        var output = service.CreateAndSaveReport("sales", "demo", "report.txt");

        Assert.Equal(output, fakeStore.SavedContent);
        Assert.Equal("report.txt", fakeStore.SavedPath);
    }

    private sealed class InMemoryFileStore : IFileStore
    {
        public string? SavedPath { get; private set; }
        public string? SavedContent { get; private set; }

        public void SaveText(string filePath, string content)
        {
            SavedPath = filePath;
            SavedContent = content;
        }
    }
}
