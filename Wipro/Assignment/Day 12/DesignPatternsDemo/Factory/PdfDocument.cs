namespace DesignPatternsDemo.Factory;

public sealed class PdfDocument : IDocument
{
    public string Type => "PDF";

    public string Render()
    {
        return "Rendering PDF document.";
    }
}
