using RagDataTools.Chunkers.Models;

namespace RagDataTools.KeywordExtraction;

/// <summary>
/// Contract for a pluggable keyword extraction backend.
/// </summary>
public interface IKeywordExtractionBackend
{
    /// <summary>
    /// Extracts keywords for a single chunk.
    /// </summary>
    Task<string[]> ExtractAsync(ChunkModel chunk, KeywordExtractionOptions options, CancellationToken cancellationToken = default);
}
