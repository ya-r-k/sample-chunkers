namespace RagDataTools.Chunkers.Models;

/// <summary>
/// Convenience helpers for working with chunk metadata.
/// </summary>
public static class ChunkModelMetadataExtensions
{
    public static int? GetPageNumber(this ChunkModel chunk)
    {
        if (chunk.Data.TryGetValue(ChunkMetadataKeys.PageNumber, out var value))
        {
            return value switch
            {
                int number => number,
                long number => checked((int)number),
                short number => number,
                _ when int.TryParse(value.ToString(), out var parsed) => parsed,
                _ => null,
            };
        }

        return null;
    }

    public static void SetPageNumber(this ChunkModel chunk, int? pageNumber)
    {
        if (pageNumber is null)
        {
            chunk.Data.Remove(ChunkMetadataKeys.PageNumber);
            return;
        }

        chunk.Data[ChunkMetadataKeys.PageNumber] = pageNumber.Value;
    }

    public static string[] GetKeywords(this ChunkModel chunk)
    {
        if (!chunk.Data.TryGetValue(ChunkMetadataKeys.Keywords, out var value))
        {
            return [];
        }

        return value switch
        {
            string[] keywords => keywords,
            IEnumerable<string> keywords => keywords.ToArray(),
            _ when value is not null => value.ToString() is { Length: > 0 } text ? [text] : [],
            _ => [],
        };
    }

    public static void SetKeywords(this ChunkModel chunk, params string[] keywords)
    {
        chunk.Data[ChunkMetadataKeys.Keywords] = keywords ?? [];
    }

    public static int? GetHeadingLevel(this ChunkModel chunk)
    {
        if (chunk.Data.TryGetValue(ChunkMetadataKeys.HeadingLevel, out var value))
        {
            return value switch
            {
                int number => number,
                long number => checked((int)number),
                short number => number,
                _ when int.TryParse(value.ToString(), out var parsed) => parsed,
                _ => null,
            };
        }

        return null;
    }

    public static void SetHeadingLevel(this ChunkModel chunk, int? headingLevel)
    {
        if (headingLevel is null)
        {
            chunk.Data.Remove(ChunkMetadataKeys.HeadingLevel);
            return;
        }

        chunk.Data[ChunkMetadataKeys.HeadingLevel] = headingLevel.Value;
    }
}
