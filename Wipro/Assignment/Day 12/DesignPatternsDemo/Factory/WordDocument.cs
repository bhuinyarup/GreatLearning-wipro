namespace DesignPatternsDemo.Factory;

public sealed class WordDocument : IDocument
{
    public string Type => "WORD";

    public string Render()
    {
        return "Rendering Word document.";
    }
}
