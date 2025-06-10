using Sample.Chunkers.Enums;
using Sample.Chunkers.Models;

namespace Sample.Chunkers.Extensions;

public static class ChunksExtensions
{
    private static readonly Dictionary<ChunkType, string> RelatedChunkRelationshipType = new()
    {
        [ChunkType.Title] = "STARTS_WITH",
        [ChunkType.CodeBlock] = "RELATED_CODE_BLOCK",
        [ChunkType.ImageLink] = "RELATED_IMAGE",
        [ChunkType.Table] = "RELATED_TABLE",
        [ChunkType.ExternalLink] = "ADDITIONAL_LINK",
    };

    private static readonly ChunkType[] RelatedChunkTypeSequence = new[]
    {
        ChunkType.Title,
        ChunkType.CodeBlock,
        ChunkType.ImageLink,
        ChunkType.Table,
        ChunkType.ExternalLink,
    };

    public static List<RelationshipModel> BuildRelationsGraph(this Dictionary<ChunkType, List<ChunkModel>> chunks)
    {
        var result = new List<RelationshipModel>();

        if (chunks.TryGetValue(ChunkType.Text, out var headersChunks))
        {
            result.AddRange(headersChunks.BuildSequenceRelations());
        }

        if (chunks.TryGetValue(ChunkType.Title, out var textChunks))
        {
            result.AddRange(textChunks.BuildSequenceRelations("HAS_NEXT_TOPIC"));
        }

        foreach (var value in chunks.Values)
        {
            foreach (var chunk in value)
            {
                result.AddRange(chunk.BuildRelationshipsForRelatedChunks());
            }
        }

        return result;
    }

    private static List<RelationshipModel> BuildSequenceRelations(this List<ChunkModel> sequenceChunks, string relationshipType = "HAS_NEXT_CHUNK")
    {
        if (sequenceChunks.Count == 0)
        {
            return [];
        }

        var result = new List<RelationshipModel>();

        for (var i = 1; i < sequenceChunks.Count; i++)
        {
            if (sequenceChunks[i].Index - sequenceChunks[i - 1].Index == 1)
            {
                result.Add(new RelationshipModel
                {
                    FirstChunkIndex = sequenceChunks[i - 1].Index,
                    SecondChunkIndex = sequenceChunks[i].Index,
                    RelationshipType = relationshipType,
                });
            }
        }

        return result;
    }

    private static List<RelationshipModel> BuildRelationshipsForRelatedChunks(this ChunkModel firstChunk)
    {
        if (firstChunk.RelatedChunksIndexes.Count == 0)
        {
            return [];
        }

        var result = new List<RelationshipModel>();

        foreach (var chunkType in RelatedChunkTypeSequence)
        {
            var isCurrentChunkFirst = chunkType != ChunkType.Title;

            if (firstChunk.RelatedChunksIndexes.TryGetValue(chunkType, out var indexes))
            {
                result.AddRange(indexes.Select(x => new RelationshipModel
                {
                    FirstChunkIndex = isCurrentChunkFirst ? firstChunk.Index : x,
                    SecondChunkIndex = isCurrentChunkFirst ? x : firstChunk.Index,
                    RelationshipType = RelatedChunkRelationshipType[chunkType],
                }));
            }
        }

        return result;
    }
}
