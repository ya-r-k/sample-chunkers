namespace Sample.Chunkers.Enums;

public enum RelationshipType
{
    Unknown = 0,
    StartsWith = 1,
    RelatedCodeBlock = 2,
    RelatedImage = 3,
    RelatedTable = 4,
    AdditionalLink = 5,
    HasNextTopic = 6,
    HasFirstSubtopic = 7,
    HasNextChunk = 8,
    LoadedFrom = 9,
}
