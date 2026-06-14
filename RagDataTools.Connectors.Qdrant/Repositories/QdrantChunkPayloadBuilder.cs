using System.Security.Cryptography;
using System.Text;
using Qdrant.Client.Grpc;
using RagDataTools.Chunkers.Models;

namespace RagDataTools.Connectors.Qdrant.Repositories;

/// <summary>
/// Builds stable Qdrant point ids and payloads for chunk persistence.
/// </summary>
public static class QdrantChunkPayloadBuilder
{
    public static ulong BuildPointId(string scopeId, ChunkModel chunk)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes($"{scopeId}:{chunk.Index}"));
        return BitConverter.ToUInt64(bytes, 0);
    }

    public static Dictionary<string, Value> BuildPayload(string scopeId, ChunkModel chunk)
    {
        var payload = new Dictionary<string, Value>
        {
            [ChunkMetadataKeys.ScopeId] = scopeId,
            [ChunkMetadataKeys.Order] = (long)chunk.Index,
            ["chunk_type"] = chunk.ChunkType.ToString(),
            ["raw_content"] = chunk.RawContent,
            ["data_json"] = System.Text.Json.JsonSerializer.Serialize(chunk.Data),
            ["relationships_json"] = System.Text.Json.JsonSerializer.Serialize(chunk.RelatedChunksIndexes),
        };

        if (chunk.GetPageNumber() is int pageNumber)
        {
            payload[ChunkMetadataKeys.PageNumber] = (long)pageNumber;
        }

        if (chunk.GetHeadingLevel() is int headingLevel)
        {
            payload[ChunkMetadataKeys.HeadingLevel] = (long)headingLevel;
        }

        var keywords = chunk.GetKeywords();
        if (keywords.Length > 0)
        {
            payload[ChunkMetadataKeys.Keywords] = keywords;
        }

        if (chunk.Data.TryGetValue(ChunkMetadataKeys.HeadingText, out var headingText) && headingText is not null)
        {
            payload[ChunkMetadataKeys.HeadingText] = headingText.ToString() ?? string.Empty;
        }

        return payload;
    }
}
