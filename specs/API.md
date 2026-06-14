# API Reference

## Overview

The library exposes extension methods for working with strings and chunk collections. The main namespaces are:
- `Sample.Chunkers.Extensions`
- `Sample.Chunkers.Enums`
- `Sample.Chunkers.Models`

## Namespaces

### Sample.Chunkers.Extensions

Contains extension methods for working with text and chunks.

### Sample.Chunkers.Models

Contains the data models: `ChunkModel`, `RelationshipModel`.

### Sample.Chunkers.Enums

Contains the enums: `ChunkType`, `RelationshipType`, `SemanticsType`.

---

## SimpleTextChunkerExtensions

Basic operations for plain text.

### GetWords

Splits text into words.

```csharp
public static Span<string> GetWords(this string text)
```

**Parameters:**
- `text` - text to split

**Returns:**
- `Span<string>` - array of words

**Example:**
```csharp
var text = "Hello world example";
var words = text.GetWords(); // ["Hello", "world", "example"]
```

**Notes:**
- Splits on spaces
- Removes empty entries

---

### ExtractSentenceStartIndices

Finds the start indexes of sentences in the word array.

```csharp
public static int[] ExtractSentenceStartIndices(this string text)
```

**Parameters:**
- `text` - text to analyze

**Returns:**
- `int[]` - indexes of the words where sentences begin

**Example:**
```csharp
var text = "First sentence. Second sentence! Third?";
var indices = text.ExtractSentenceStartIndices(); // [0, 2, 4]
```

**Notes:**
- Uses regex to find sentence boundaries: `.`, `!`, `?`, `:\n`
- The indexes point into the word array

---

### ExtractParagraphStartIndexes

Finds the start indexes of paragraphs in the word array.

```csharp
public static int[] ExtractParagraphStartIndexes(this string text)
```

**Parameters:**
- `text` - text to analyze

**Returns:**
- `int[]` - indexes of the words where paragraphs begin

**Example:**
```csharp
var text = "First paragraph.\n\nSecond paragraph.";
var indices = text.ExtractParagraphStartIndexes(); // [0, 3]
```

**Notes:**
- Splits the text on `\n ` (newline plus space)

---

### PreprocessNaturalTextForChunking

Prepares text for chunking.

```csharp
public static string PreprocessNaturalTextForChunking(this string text)
```

**Parameters:**
- `text` - text to preprocess

**Returns:**
- `string` - cleaned text

**Operations:**
- Removes leading and trailing spaces
- Replaces non-breaking spaces (`\u00A0`) with normal spaces
- Normalizes line endings (`\r\n` -> `\n`)
- Replaces em dash (`\u2014`) with hyphen (`-`)
- Removes repeated spaces

**Example:**
```csharp
var text = "Text   with\n\r\nmultiple   spaces";
var cleaned = text.PreprocessNaturalTextForChunking();
```

---

### PreprocessNaturalTextsForChunking

Prepares an array of texts for chunking.

```csharp
public static string[] PreprocessNaturalTextsForChunking(this string[] texts)
```

**Parameters:**
- `texts` - array of texts

**Returns:**
- `string[]` - array of cleaned texts

**Example:**
```csharp
var texts = new[] { "Text 1", "Text  2" };
var cleaned = texts.PreprocessNaturalTextsForChunking();
```

---

### ExtractSemanticChunksFromText

Extracts semantic chunks from text.

```csharp
public static string[] ExtractSemanticChunksFromText(
    this string text,
    int chunkWordsCount,
    SemanticsType semanticsType,
    double overlapPercentage = 0.0
)
```

**Parameters:**
- `text` - text to split
- `chunkWordsCount` - maximum number of words per chunk
- `semanticsType` - semantics type (`Sentence` or `Paragraph`)
- `overlapPercentage` - overlap between chunks (`0.0` to `1.0`, default `0.0`)

**Returns:**
- `string[]` - array of text chunks

**Example:**
```csharp
var text = "Long text with multiple sentences. Second sentence. Third sentence.";
var chunks = text.ExtractSemanticChunksFromText(
    chunkWordsCount: 10,
    semanticsType: SemanticsType.Sentence,
    overlapPercentage: 0.3
);
```

**Notes:**
- Chunks do not split sentences or paragraphs
- Overlap uses the nearest semantic boundary
- If the text is smaller than `chunkWordsCount`, a single chunk is returned

---

## ComplexDataChunkerExtensions

Structured element extraction and deep text processing.

### ExtractSemanticChunksDeeply (collection)

Processes a document collection with automatic indexing.

```csharp
public static Dictionary<T, Dictionary<ChunkType, List<ChunkModel>>> ExtractSemanticChunksDeeply<T>(
    this Dictionary<T, string> texts,
    int chunkWordsCount,
    SemanticsType semanticsType,
    double overlapPercentage = 0.0,
    bool withTables = true,
    bool withInfoBlocks = true,
    bool withCodeBlocks = true,
    bool withImages = true,
    bool withLinks = true
) where T : unmanaged
```

**Parameters:**
- `texts` - document dictionary (`key -> text`)
- `chunkWordsCount` - maximum words in a text chunk
- `semanticsType` - semantics type
- `overlapPercentage` - overlap ratio (`0.0` to `1.0`)
- `withTables` - extract tables
- `withInfoBlocks` - extract info blocks
- `withCodeBlocks` - extract code blocks
- `withImages` - extract images
- `withLinks` - extract links

**Returns:**
- `Dictionary<T, Dictionary<ChunkType, List<ChunkModel>>>` - document -> chunk type -> chunk list

**Example:**
```csharp
var documents = new Dictionary<int, string>
{
    [0] = "# Doc 1\n\nText...",
    [1] = "# Doc 2\n\nText..."
};

var chunks = documents.ExtractSemanticChunksDeeply(
    chunkWordsCount: 100,
    semanticsType: SemanticsType.Sentence,
    overlapPercentage: 0.5
);

// chunks[0][ChunkType.TextChunk] - text chunks from the first document
// chunks[1][ChunkType.CodeBlock] - code blocks from the second document
```

**Notes:**
- Chunk indexes accumulate across documents
- The key type must be `unmanaged`

---

### ExtractSemanticChunksDeeply (single text)

Extracts all chunk types from one text.

```csharp
public static Dictionary<ChunkType, List<ChunkModel>> ExtractSemanticChunksDeeply(
    this string text,
    int chunkWordsCount,
    SemanticsType semanticsType,
    double overlapPercentage = 0.0,
    bool withTables = true,
    bool withInfoBlocks = true,
    bool withCodeBlocks = true,
    bool withImages = true,
    bool withLinks = true,
    int lastUsedIndex = 0
)
```

**Parameters:**
- `text` - text to process
- `chunkWordsCount` - maximum words in a text chunk
- `semanticsType` - semantics type
- `overlapPercentage` - overlap ratio (`0.0` to `1.0`)
- `withTables` - extract tables
- `withInfoBlocks` - extract info blocks
- `withCodeBlocks` - extract code blocks
- `withImages` - extract images
- `withLinks` - extract links
- `lastUsedIndex` - last used index for continuing numbering

**Returns:**
- `Dictionary<ChunkType, List<ChunkModel>>` - chunk type -> chunk list

**Example:**
```csharp
var markdown = @"# Title

Text paragraph.

```csharp
var code = ""example"";
```

<table>
    <tr><td>Cell</td></tr>
</table>";

var chunks = markdown.ExtractSemanticChunksDeeply(
    chunkWordsCount: 50,
    semanticsType: SemanticsType.Paragraph
);

var topics = chunks[ChunkType.Topic];
var codeBlocks = chunks[ChunkType.CodeBlock];
var tables = chunks[ChunkType.Table];
var textChunks = chunks[ChunkType.TextChunk];
```

**Processing order:**
1. Extract structured elements
2. Replace them with placeholders
3. Preprocess the text
4. Extract text chunks
5. Detect links back to the extracted elements

---

### RetrieveChunksFromText

Extracts only structured elements without text chunks.

```csharp
public static Dictionary<ChunkType, List<ChunkModel>> RetrieveChunksFromText(
    this string text,
    bool withTables,
    bool withInfoBlocks,
    bool withCodeBlocks,
    bool withImages,
    bool withLinks,
    int lastUsedIndex = 0
)
```

**Parameters:**
- `text` - text to process
- `withTables` - extract tables
- `withInfoBlocks` - extract info blocks
- `withCodeBlocks` - extract code blocks
- `withImages` - extract images
- `withLinks` - extract links
- `lastUsedIndex` - last used index

**Returns:**
- `Dictionary<ChunkType, List<ChunkModel>>`

**Example:**
```csharp
var text = "```csharp\ncode\n```\n\n![img](url.jpg)";
var chunks = text.RetrieveChunksFromText(
    withCodeBlocks: true,
    withImages: true,
    withTables: false,
    withInfoBlocks: false,
    withLinks: false
);

// chunks contains only CodeBlock and ImageLink
// TextChunk is absent
```

**Notes:**
- Used when only structured elements are needed
- No text chunks are extracted

---

## ChunksExtensions

Operations on chunk collections: relation graph building and duplicate detection.

### BuildRelationsGraph (collection)

Builds a relation graph for a document collection.

```csharp
public static RelationshipModel[] BuildRelationsGraph<T>(
    this Dictionary<T, Dictionary<ChunkType, List<ChunkModel>>> chunks
) where T : unmanaged
```

**Parameters:**
- `chunks` - document -> chunk type -> chunk list

**Returns:**
- `RelationshipModel[]`

**Example:**
```csharp
var chunks = documents.ExtractSemanticChunksDeeply(...);
var relationships = chunks.BuildRelationsGraph();
```

---

### BuildRelationsGraph (single document)

Builds a relation graph for one document.

```csharp
public static RelationshipModel[] BuildRelationsGraph(
    this Dictionary<ChunkType, List<ChunkModel>> chunks
)
```

**Parameters:**
- `chunks` - chunk type -> chunk list

**Returns:**
- `RelationshipModel[]`

**Relationship types:**
- `HasNextChunk`
- `HasNextTopic`
- `HasFirstSubtopic`
- `RelatedCodeBlock`
- `RelatedTable`
- `RelatedImage`
- `RelatedInfoBlock`
- `AdditionalLink`
- `StartsWith`

---

### FindRepeatedChunksWithUrls

Finds duplicate chunks with the same URL.

```csharp
public static Dictionary<int, int> FindRepeatedChunksWithUrls<T>(
    this Dictionary<T, Dictionary<ChunkType, List<ChunkModel>>> chunks
) where T : unmanaged
```

**Parameters:**
- `chunks` - document -> chunk type -> chunk list

**Returns:**
- `Dictionary<int, int>` - `duplicate_index -> unique_index`

**Example:**
```csharp
var chunks = documents.ExtractSemanticChunksDeeply(...);
var duplicates = chunks.FindRepeatedChunksWithUrls();

// duplicates[10] = 5 means:
// chunk 10 is a duplicate of chunk 5
```

**Notes:**
- Works only with `ImageLink` and `AdditionalLink`
- The first chunk with a given URL is treated as unique
- All later chunks with the same URL are treated as duplicates

---

## Data models

### ChunkModel

Represents one chunk of text or one structured element.

```csharp
public record ChunkModel
{
    public int Index { get; set; }
    public required ChunkType ChunkType { get; set; }
    public required string RawContent { get; set; }
    public required Dictionary<string, object> Data { get; set; }
    public required Dictionary<ChunkType, List<int>> RelatedChunksIndexes { get; set; }
}
```

**Properties:**
- `Index` - unique numeric chunk identifier
- `ChunkType` - chunk type
- `RawContent` - raw chunk content
- `Data` - additional metadata
- `RelatedChunksIndexes` - relation indexes grouped by chunk type

### RelationshipModel

Represents a relationship between two chunks.

```csharp
public record RelationshipModel
{
    public int FirstChunkIndex { get; set; }
    public int SecondChunkIndex { get; set; }
    public RelationshipType RelationshipType { get; set; }
}
```

**Properties:**
- `FirstChunkIndex` - first chunk index
- `SecondChunkIndex` - second chunk index
- `RelationshipType` - relationship type

---

## Enums

### ChunkType

```csharp
public enum ChunkType
{
    TextChunk = 0,
    Table = 1,
    CodeBlock = 2,
    MathBlock = 3,
    InfoBlock = 4,
    ImageLink = 5,
    Topic = 6,
    AdditionalLink = 7
}
```

### RelationshipType

```csharp
public enum RelationshipType
{
    Unknown = 0,
    StartsWith = 1,
    RelatedCodeBlock = 2,
    RelatedImage = 3,
    RelatedTable = 4,
    RelatedInfoBlock = 5,
    AdditionalLink = 6,
    HasNextTopic = 7,
    HasFirstSubtopic = 8,
    HasNextChunk = 9,
    LoadedFrom = 10
}
```

### SemanticsType

```csharp
public enum SemanticsType
{
    Sentence = 1,
    Paragraph = 2
}
```

---

## Error handling

The library does not throw exceptions in most normal scenarios:
- Empty text returns an empty dictionary
- Text without structured elements returns only text chunks
- Invalid Markdown is handled as plain text

**Exceptions:**
- `InvalidOperationException` for an unsupported `SemanticsType`

---

## Limitations

1. **Key types in collection methods**
   - `Dictionary<T, ...>` methods require `T` to be `unmanaged`
   - `string` is not supported directly; use `int` or another numeric type

2. **Performance**
   - Regular expressions are compiled at build time with `GeneratedRegex`
   - Large documents over 1 MB can be slower

3. **Format support**
   - Markdown: partial support for the main elements
   - HTML: tables only
   - Math blocks: declared, but not implemented

---

## Usage examples

See also [EXAMPLES.md](EXAMPLES.md) for more detailed examples.
