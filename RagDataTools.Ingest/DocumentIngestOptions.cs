using RagDataTools.Chunkers.Infrastructure;
using RagDataTools.Chunkers.Strategies.IndexesExtractors;

namespace RagDataTools.Ingest;

/// <summary>
/// Options used when ingesting a single document.
/// </summary>
public sealed record DocumentIngestOptions
{
    /// <summary>
    /// Default maximum PDF size in bytes.
    /// </summary>
    public const long DefaultMaxPdfBytes = 50L * 1024 * 1024;

    /// <summary>
    /// Default maximum Word size in bytes.
    /// </summary>
    public const long DefaultMaxWordBytes = 25L * 1024 * 1024;

    /// <summary>
    /// Default maximum HTML size in bytes.
    /// </summary>
    public const long DefaultMaxHtmlBytes = 10L * 1024 * 1024;

    /// <summary>
    /// Maximum words per chunk passed to the chunking pipeline.
    /// </summary>
    public int ChunkWordsCount { get; init; } = 120;

    /// <summary>
    /// Primitive extractor used by the chunking pipeline.
    /// </summary>
    public IPrimitivesIndexesExtractor IndexesExtractor { get; init; } = PrimitivesExtractors.SentencesExtractor;

    /// <summary>
    /// Chunk overlap percentage in the range 0.0 to 1.0.
    /// </summary>
    public double OverlapPercentage { get; init; } = 0.0;

    /// <summary>
    /// Maximum allowed PDF size in bytes.
    /// </summary>
    public long MaxPdfBytes { get; init; } = DefaultMaxPdfBytes;

    /// <summary>
    /// Maximum allowed Word size in bytes.
    /// </summary>
    public long MaxWordBytes { get; init; } = DefaultMaxWordBytes;

    /// <summary>
    /// Maximum allowed HTML size in bytes.
    /// </summary>
    public long MaxHtmlBytes { get; init; } = DefaultMaxHtmlBytes;

    internal long GetMaxBytes(DocumentFormat format)
    {
        return format switch
        {
            DocumentFormat.Pdf => MaxPdfBytes,
            DocumentFormat.Word => MaxWordBytes,
            DocumentFormat.Html => MaxHtmlBytes,
            _ => throw new NotSupportedException($"Unsupported document format: {format}."),
        };
    }
}
