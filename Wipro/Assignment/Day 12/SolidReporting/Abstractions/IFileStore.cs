namespace SolidReporting.Abstractions;

public interface IFileStore
{
    void SaveText(string filePath, string content);
}
