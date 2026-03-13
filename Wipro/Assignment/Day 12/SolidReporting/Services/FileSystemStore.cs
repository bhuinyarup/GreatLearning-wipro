using SolidReporting.Abstractions;

namespace SolidReporting.Services;

public sealed class FileSystemStore : IFileStore
{
    public void SaveText(string filePath, string content)
    {
        File.WriteAllText(filePath, content);
    }
}
