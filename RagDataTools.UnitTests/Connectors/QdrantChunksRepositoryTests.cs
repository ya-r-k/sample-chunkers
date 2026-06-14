using FluentAssertions;
using RagDataTools.Chunkers.Models;
using RagDataTools.Chunkers.Models.Enums;
using RagDataTools.Connectors.Qdrant.Repositories;

namespace RagDataTools.UnitTests.Connectors;

public class QdrantChunksRepositoryTests
{
    [Test]
    public async Task AddAsync_WhenClientMissing_ShouldThrowClearError()
    {
        var repository = new QdrantChunksRepository();

        var act = async () => await repository.AddAsync("doc-1", new ChunkModel
        {
            Index = 1,
            ChunkType = ChunkType.TextChunk,
            RawContent = "alpha",
            Data = new Dictionary<string, object>(),
            RelatedChunksIndexes = new Dictionary<RelationshipType, List<int>>(),
        });

        var exception = await act.Should().ThrowAsync<InvalidOperationException>();

        exception.Which.Message.Should().Contain("Qdrant client is not configured");
    }

    [Test]
    public void PayloadBuilder_ShouldIncludeScopeOrderAndHeadingMetadata()
    {
        var chunk = new ChunkModel
        {
            Index = 7,
            ChunkType = ChunkType.Topic,
            RawContent = "Heading",
            Data = new Dictionary<string, object>
            {
                [ChunkMetadataKeys.PageNumber] = 3,
                [ChunkMetadataKeys.HeadingLevel] = 2,
                [ChunkMetadataKeys.Keywords] = new[] { "alpha", "beta" },
            },
            RelatedChunksIndexes = new Dictionary<RelationshipType, List<int>>(),
        };

        var payload = QdrantChunkPayloadBuilder.BuildPayload("doc-7", chunk);

        payload[ChunkMetadataKeys.ScopeId].Should().NotBeNull();
        payload[ChunkMetadataKeys.Order].Should().NotBeNull();
        payload[ChunkMetadataKeys.PageNumber].Should().NotBeNull();
        payload[ChunkMetadataKeys.HeadingLevel].Should().NotBeNull();
        payload[ChunkMetadataKeys.Keywords].Should().NotBeNull();
    }
}
