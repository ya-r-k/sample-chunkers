using RagDataTools.Ingest;

namespace RagDataTools.Ingest.Conversion;

/// <summary>
/// Converts a single document stream into Markdown page segments.
/// </summary>
public interface IDocumentToMarkdownConverter
{
    /// <summary>
    /// Gets the supported source format.
    /// </summary>
    DocumentFormat Format { get; }

    /// <summary>
    /// Converts the document into one or more Markdown segments.
    /// </summary>
    Task<IReadOnlyList<MarkdownPageSegment>> ConvertAsync(Stream document, CancellationToken cancellationToken = default);
}
