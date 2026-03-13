namespace DesignPatternsDemo.Singleton;

public sealed class Logger
{
    private static readonly Lazy<Logger> LazyInstance = new(() => new Logger());
    private readonly List<string> _messages = new();

    private Logger()
    {
    }

    public static Logger Instance => LazyInstance.Value;

    public IReadOnlyList<string> Messages => _messages.AsReadOnly();

    public void Log(string message)
    {
        _messages.Add(message);
    }

    public void Clear()
    {
        _messages.Clear();
    }
}
