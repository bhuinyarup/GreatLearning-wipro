namespace DesignPatternsDemo.Factory;

public sealed class DocumentFactory
{
    public IDocument Create(string documentType)
    {
        return documentType.ToLowerInvariant() switch
        {
            "pdf" => new PdfDocument(),
            "word" => new WordDocument(),
            _ => throw new ArgumentException($"Unsupported document type: {documentType}", nameof(documentType))
        };
    }
}
