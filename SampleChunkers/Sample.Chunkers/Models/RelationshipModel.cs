using Sample.Chunkers.Enums;

namespace Sample.Chunkers.Models;

public class RelationshipModel
{
    public int FirstChunkIndex { get; set; }

    public int SecondChunkIndex { get; set; }

    public RelationshipType RelationshipType { get; set; }
}
