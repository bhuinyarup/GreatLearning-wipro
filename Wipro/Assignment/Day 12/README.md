# Day 12 Assignment - SOLID Principles and Design Patterns

## Project Structure

- `SolidReporting`: Refactored reporting system that demonstrates all SOLID principles.
- `DesignPatternsDemo`: Implementations of Singleton, Factory, and Observer patterns.
- `Day12Assignments.Tests`: Unit tests for both assignments.

## SOLID Mapping

- **SRP**: `ReportGenerator` handles report creation and `ReportSaver` handles persistence.
- **OCP**: `ReportFormatter` uses `IReportFormattingStrategy` and supports new formats by adding new strategies.
- **LSP**: `SalesReport` and `InventoryReport` can be used through base `Report` type.
- **ISP**: `IReportContent` and `ISummarizableReport` split responsibilities.
- **DIP**: `ReportService` depends on abstractions (`IReportGenerator`, `IReportFormattingStrategy`, `IReportSaver`).

## Design Patterns Included

- **Singleton**: `Logger`
- **Factory**: `DocumentFactory`
- **Observer**: `WeatherStation` + `WeatherDisplay`

## Run Tests

```bash
dotnet test Day12Assignments.slnx
```
