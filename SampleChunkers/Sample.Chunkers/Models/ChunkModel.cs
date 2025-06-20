using Sample.Chunkers.Enums;

namespace Sample.Chunkers.Models;

public class ChunkModel
{
    public int Index { get; set; }

    public required ChunkType ChunkType { get; set; }

    public required string RawContent { get; set; }

    public required Dictionary<string, object> Data { get; set; }

    public required Dictionary<ChunkType, List<int>> RelatedChunksIndexes { get; set; }
}
