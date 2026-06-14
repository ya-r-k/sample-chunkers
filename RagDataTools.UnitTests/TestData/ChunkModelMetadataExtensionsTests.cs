using FluentAssertions;
using RagDataTools.Chunkers.Models;
using RagDataTools.Chunkers.Models.Enums;

namespace RagDataTools.UnitTests.TestData;

public class ChunkModelMetadataExtensionsTests
{
    [Test]
    public void MetadataHelpers_ShouldRoundTripPageKeywordsAndHeading()
    {
        var chunk = new ChunkModel
        {
            Index = 1,
            ChunkType = ChunkType.TextChunk,
            RawContent = "alpha",
            Data = new Dictionary<string, object>(),
            RelatedChunksIndexes = new Dictionary<RelationshipType, List<int>>(),
        };

        chunk.SetPageNumber(4);
        chunk.SetKeywords("alpha", "beta");
        chunk.SetHeadingLevel(2);

        chunk.GetPageNumber().Should().Be(4);
        chunk.GetKeywords().Should().BeEquivalentTo(["alpha", "beta"]);
        chunk.GetHeadingLevel().Should().Be(2);
    }
}
