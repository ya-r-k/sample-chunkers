using RagDataTools.Chunkers.Models;
using RagDataTools.Connectors.Interfaces;

namespace RagDataTools.Connectors.Extensions;

/// <summary>
/// Convenience facade for persisting chunks with a scope id.
/// </summary>
public static class ChunksRepositoryExtensions
{
    /// <summary>
    /// Saves chunks under a caller-provided scope id.
    /// </summary>
    public static Task SaveAsync(this IChunksRepository<string, string> repository, string scopeId, params ChunkModel[] chunks)
        => repository.AddAsync(scopeId, chunks);
}
