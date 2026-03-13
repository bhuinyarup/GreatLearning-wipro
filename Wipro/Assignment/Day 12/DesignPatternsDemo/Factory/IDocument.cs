namespace DesignPatternsDemo.Factory;

public interface IDocument
{
    string Type { get; }
    string Render();
}
