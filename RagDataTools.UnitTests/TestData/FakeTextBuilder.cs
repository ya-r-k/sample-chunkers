using RagDataTools.Chunkers.Models;
using RagDataTools.Chunkers.Models.Enums;

namespace RagDataTools.UnitTests.TestData;

public static class FakeTextBuilder
{
    public static string PlainText =>
        "Alpha beta gamma. Delta epsilon zeta. Eta theta iota.";

    public static string MarkdownText =>
        """
        # Heading 1

        First paragraph.

        ## Heading 2

        Second paragraph with a [link](https://example.com).
        """;

    public static ChunkModel[] PlainTextChunks =>
    [
        new ChunkModel
        {
            Index = 0,
            ChunkType = ChunkType.TextChunk,
            RawContent = "Alpha beta gamma.",
            Data = new Dictionary<string, object>(),
            RelatedChunksIndexes = new Dictionary<RelationshipType, List<int>>(),
        },
        new ChunkModel
        {
            Index = 1,
            ChunkType = ChunkType.TextChunk,
            RawContent = "Delta epsilon zeta.",
            Data = new Dictionary<string, object>(),
            RelatedChunksIndexes = new Dictionary<RelationshipType, List<int>>(),
        },
    ];

    public static ChunkModel[] MarkdownChunks =>
    [
        new ChunkModel
        {
            Index = 0,
            ChunkType = ChunkType.Topic,
            RawContent = "# Heading 1",
            Data = new Dictionary<string, object>
            {
                ["name"] = "Heading 1",
                ["level"] = 1,
            },
            RelatedChunksIndexes = new Dictionary<RelationshipType, List<int>>(),
        },
        new ChunkModel
        {
            Index = 1,
            ChunkType = ChunkType.TextChunk,
            RawContent = "First paragraph.",
            Data = new Dictionary<string, object>(),
            RelatedChunksIndexes = new Dictionary<RelationshipType, List<int>>(),
        },
    ];
}
