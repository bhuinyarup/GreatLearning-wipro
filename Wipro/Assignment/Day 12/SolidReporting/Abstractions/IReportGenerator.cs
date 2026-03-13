using SolidReporting.Models;

namespace SolidReporting.Abstractions;

public interface IReportGenerator
{
    Report Generate(string reportType, string sourceData);
}
