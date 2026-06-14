using RagDataTools.Chunkers.Models;

namespace RagDataTools.KeywordExtraction;

/// <summary>
/// Contract for extracting keywords from one or more chunks.
/// </summary>
public interface IKeywordExtractor
{
    /// <summary>
    /// Extracts keywords from the provided chunks and stores them in chunk metadata.
    /// </summary>
    Task<ChunkModel[]> ExtractAsync(ChunkModel[] chunks, KeywordExtractionOptions? options = null, CancellationToken cancellationToken = default);
}
