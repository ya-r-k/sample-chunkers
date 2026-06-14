using Qdrant.Client;
using Qdrant.Client.Grpc;
using RagDataTools.Chunkers.Models;
using RagDataTools.Connectors.Interfaces;

namespace RagDataTools.Connectors.Qdrant.Repositories;

public sealed class QdrantChunksRepository : IChunksRepository<string, string>
{
    private const string DefaultCollectionName = "chunks";
    private readonly QdrantClient? client;

    public QdrantChunksRepository(QdrantClient? client = null, string? collectionName = null)
    {
        this.client = client;
        CollectionName = string.IsNullOrWhiteSpace(collectionName) ? DefaultCollectionName : collectionName;
    }

    public string CollectionName { get; }

    public Task AddAsync(string[] flags, params ChunkModel[] chunks)
    {
        var scopeId = ComposeScopeId(flags);
        return AddAsync(scopeId, chunks);
    }

    public async Task AddAsync(string scopeId, params ChunkModel[] chunks)
    {
        EnsureClient();
        ArgumentException.ThrowIfNullOrWhiteSpace(scopeId);

        await client!.DeleteAsync(CollectionName, Conditions.MatchText(ChunkMetadataKeys.ScopeId, scopeId), true);

        var points = chunks
            .Select(chunk =>
            {
                var point = new PointStruct
                {
                    Id = QdrantChunkPayloadBuilder.BuildPointId(scopeId, chunk),
                    Vectors = new float[] { 0f },
                };

                foreach (var entry in QdrantChunkPayloadBuilder.BuildPayload(scopeId, chunk))
                {
                    point.Payload.Add(entry.Key, entry.Value);
                }

                return point;
            })
            .ToArray();

        if (points.Length == 0)
        {
            return;
        }

        await client.UpsertAsync(CollectionName, points, true);
    }

    public Task<IDictionary<int, string>> GetIndexesIdsPairsByFlagAsync(string flag)
        => throw new NotSupportedException("Reading persisted Qdrant chunk ids is not supported in this connector version.");

    public Task<IDictionary<int, string>> GetIndexesIdsPairsByScopeIdAsync(string scopeId)
        => throw new NotSupportedException("Reading persisted Qdrant chunk ids is not supported in this connector version.");

    public Task RemoveFlagFromAllDataAsync(string flag)
        => RemoveScopeAsync(flag);

    public async Task RemoveScopeAsync(string scopeId)
    {
        EnsureClient();
        ArgumentException.ThrowIfNullOrWhiteSpace(scopeId);

        await client!.DeleteAsync(CollectionName, Conditions.MatchText(ChunkMetadataKeys.ScopeId, scopeId), true);
    }

    private void EnsureClient()
    {
        if (client is null)
        {
            throw new InvalidOperationException("Qdrant client is not configured.");
        }
    }

    private static string ComposeScopeId(IEnumerable<string> flags)
    {
        var values = flags.Where(flag => !string.IsNullOrWhiteSpace(flag)).ToArray();
        return values.Length == 0 ? throw new ArgumentException("At least one scope flag is required.", nameof(flags))
            : values.Length == 1 ? values[0] : string.Join(":", values);
    }
}
