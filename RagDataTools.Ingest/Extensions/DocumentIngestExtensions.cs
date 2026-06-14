using RagDataTools.Ingest;
using RagDataTools.Chunkers.Models;

namespace RagDataTools.Ingest.Extensions;

/// <summary>
/// Convenience entry points for single-document ingest.
/// </summary>
public static class DocumentIngestExtensions
{
    /// <summary>
    /// Ingests a document from disk using the default service.
    /// </summary>
    public static Task<ChunkModel[]> IngestAsync(string path, DocumentFormat format, DocumentIngestOptions? options = null, CancellationToken cancellationToken = default)
    {
        return DocumentIngestService.Default.IngestAsync(path, format, options, cancellationToken);
    }

    /// <summary>
    /// Ingests a document from a stream using the default service.
    /// </summary>
    public static Task<ChunkModel[]> IngestAsync(Stream source, DocumentFormat format, DocumentIngestOptions? options = null, CancellationToken cancellationToken = default)
    {
        return DocumentIngestService.Default.IngestAsync(source, format, options, cancellationToken);
    }
}
