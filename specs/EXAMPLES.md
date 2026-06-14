# Usage examples

## Basic examples

### Example 1: Simple text chunk extraction

```csharp
using RagDataTools.Chunkers.Extensions;
using RagDataTools.Chunkers.Enums;

var text = @"First sentence. Second sentence. Third sentence.";

var chunks = text.ExtractSemanticChunksFromText(
    chunkWordsCount: 5,
    semanticsType: SemanticsType.Sentence,
    overlapPercentage: 0.0
);

// Result:
// chunks[0] = "First sentence."
// chunks[1] = "Second sentence."
// chunks[2] = "Third sentence."
```

---

### Example 2: Extraction with overlap

```csharp
var text = @"First sentence. Second sentence. Third sentence. Fourth sentence.";

var chunks = text.ExtractSemanticChunksFromText(
    chunkWordsCount: 5,
    semanticsType: SemanticsType.Sentence,
    overlapPercentage: 0.3
);

// Result:
// chunks[0] = "First sentence. Second sentence."
// chunks[1] = "Second sentence. Third sentence."
// chunks[2] = "Third sentence. Fourth sentence."
```

---

### Example 3: Markdown extraction with headings and code

```csharp
var markdown = @"# Introduction

This is the first paragraph.

```csharp
var code = ""example"";
```

## Subheading

Another paragraph.";

var chunks = markdown.ExtractSemanticChunksDeeply(
    chunkWordsCount: 10,
    semanticsType: SemanticsType.Paragraph,
    overlapPercentage: 0.2
);

var topics = chunks[ChunkType.Topic];
var codeBlocks = chunks[ChunkType.CodeBlock];
var textChunks = chunks[ChunkType.TextChunk];

Console.WriteLine($"Headings found: {topics.Count}");
Console.WriteLine($"Code blocks found: {codeBlocks.Count}");
Console.WriteLine($"Text chunks found: {textChunks.Count}");
```

---

### Example 4: Structured elements only

```csharp
var text = @"# Heading

Paragraph text.

```csharp
var code = 1;
```

<table>
    <tr><td>Cell</td></tr>
</table>

![Image](image.jpg)
[Link](https://example.com)";

var chunks = text.RetrieveChunksFromText(
    withTables: true,
    withInfoBlocks: false,
    withCodeBlocks: true,
    withImages: true,
    withLinks: true
);

// chunks contains:
// - ChunkType.Topic
// - ChunkType.CodeBlock
// - ChunkType.Table
// - ChunkType.ImageLink
// - ChunkType.AdditionalLink
// TextChunk is absent
```

---

### Example 5: Processing a document collection

```csharp
var documents = new Dictionary<int, string>
{
    [0] = @"# Document 1

Text of the first document.",

    [1] = @"# Document 2

Text of the second document.
```python
def hello():
    print('Hello')
```"
};

var allChunks = documents.ExtractSemanticChunksDeeply(
    chunkWordsCount: 20,
    semanticsType: SemanticsType.Sentence,
    overlapPercentage: 0.0
);

// Indexes accumulate across documents:
// document 0 topic index = 1
// document 0 text chunk index = 2
// document 1 topic index = 3
// document 1 text chunk index = 4
// document 1 code block index = 5

var doc0Topics = allChunks[0][ChunkType.Topic];
var doc1CodeBlocks = allChunks[1][ChunkType.CodeBlock];
```

---

### Example 6: Relation graph building

```csharp
var text = @"# Heading 1

Paragraph text with a [link](https://example.com).

```csharp
var code = 1;
```

## Heading 2

More text.";

var chunks = text.ExtractSemanticChunksDeeply(
    chunkWordsCount: 10,
    semanticsType: SemanticsType.Paragraph
);

var relationships = chunks.BuildRelationsGraph();

foreach (var relation in relationships)
{
    var firstChunk = GetChunkByIndex(chunks, relation.FirstChunkIndex);
    var secondChunk = GetChunkByIndex(chunks, relation.SecondChunkIndex);

    Console.WriteLine(
        $"{firstChunk.ChunkType}[{relation.FirstChunkIndex}] " +
        $"{relation.RelationshipType} " +
        $"{secondChunk.ChunkType}[{relation.SecondChunkIndex}]"
    );
}

// Output:
// Topic[1] StartsWith AdditionalLink[2]
// TextChunk[3] HasNextChunk TextChunk[4]
// Topic[5] HasNextTopic Topic[6]
```

---

### Example 7: Duplicate URL detection

```csharp
var documents = new Dictionary<int, string>
{
    [0] = @"![Image](https://example.com/image1.jpg)
Text with a [link](https://example.com/link)",

    [1] = @"![Image](https://example.com/image1.jpg)
Text with a [link](https://example.com/link)"
};

var chunks = documents.ExtractSemanticChunksDeeply(10, SemanticsType.Sentence);

var duplicates = chunks.FindRepeatedChunksWithUrls();

foreach (var duplicate in duplicates)
{
    Console.WriteLine(
        $"Chunk {duplicate.Key} is a duplicate of chunk {duplicate.Value}"
    );
}
```

---

## Advanced examples

### Example 8: Chunk type filtering

```csharp
var text = @"# Heading
Text with code ```code``` and a [link](url)";

var chunks = text.ExtractSemanticChunksDeeply(
    chunkWordsCount: 10,
    semanticsType: SemanticsType.Sentence,
    withTables: true,
    withInfoBlocks: false,
    withCodeBlocks: true,
    withImages: false,
    withLinks: false
);
```

### Example 9: Working with chunk metadata

```csharp
var text = @"# Heading level 1

## Heading level 2

```python
def hello():
    print('Hello')
```";

var chunks = text.ExtractSemanticChunksDeeply(10, SemanticsType.Sentence);

foreach (var topic in chunks[ChunkType.Topic])
{
    var name = topic.Data["name"] as string;
    var level = (int)topic.Data["level"];
    Console.WriteLine($"Level {level} heading: {name}");
}
```

### Example 10: Working with related chunks

```csharp
var text = @"# Heading with a [link](https://example.com)

Paragraph text.";

var chunks = text.ExtractSemanticChunksDeeply(10, SemanticsType.Sentence);
var topic = chunks[ChunkType.Topic].First();

if (topic.RelatedChunksIndexes.TryGetValue(ChunkType.AdditionalLink, out var linkIndexes))
{
    foreach (var linkIndex in linkIndexes)
    {
        var link = chunks[ChunkType.AdditionalLink]
            .FirstOrDefault(c => c.Index == linkIndex);

        if (link != null)
        {
            var url = link.Data["url"] as string;
            Console.WriteLine($"Heading is linked to: {url}");
        }
    }
}
```

### Example 11: Continuing numbering across documents

```csharp
var text1 = @"# Heading 1
Text 1";

var chunks1 = text1.ExtractSemanticChunksDeeply(
    10, SemanticsType.Sentence,
    lastUsedIndex: 0
);

var lastIndex = chunks1.Values.SelectMany(x => x).Max(c => c.Index);

var text2 = @"# Heading 2
Text 2";

var chunks2 = text2.ExtractSemanticChunksDeeply(
    10, SemanticsType.Sentence,
    lastUsedIndex: lastIndex
);
```

### Example 12: Nested table extraction

```csharp
var html = @"<table>
    <tr>
        <td>
            <table>
                <tr><td>Nested table</td></tr>
            </table>
        </td>
    </tr>
</table>";

var chunks = html.RetrieveChunksFromText(
    withTables: true,
    withInfoBlocks: false,
    withCodeBlocks: false,
    withImages: false,
    withLinks: false
);
```

### Example 13: Working with info blocks

```csharp
var markdown = @"Plain text.

> This is an info block
> Multi-line

More text.";

var chunks = markdown.ExtractSemanticChunksDeeply(
    10, SemanticsType.Paragraph
);

foreach (var infoBlock in chunks[ChunkType.InfoBlock])
{
    var content = infoBlock.Data["content"] as string;
    Console.WriteLine($"Info block: {content}");
}
```

### Example 14: Complex real-world article processing

```csharp
var article = @"
# Article heading

Introduction text.

## First section

Text with a [link](https://example.com).

```csharp
public class Example
{
    public void Method() { }
}
```

### Subsection

Text with an image:

![Image](https://example.com/image.jpg)

<table>
    <tr>
        <th>Header</th>
        <th>Value</th>
    </tr>
    <tr>
        <td>Data</td>
        <td>123</td>
    </tr>
</table>

## Second section

More text.";

var chunks = article.ExtractSemanticChunksDeeply(
    chunkWordsCount: 50,
    semanticsType: SemanticsType.Sentence,
    overlapPercentage: 0.3
);

var relationships = chunks.BuildRelationsGraph();

Console.WriteLine($"Total chunks: {chunks.Values.SelectMany(x => x).Count()}");
Console.WriteLine($"Headings: {chunks[ChunkType.Topic].Count}");
Console.WriteLine($"Code blocks: {chunks[ChunkType.CodeBlock].Count}");
Console.WriteLine($"Tables: {chunks[ChunkType.Table].Count}");
Console.WriteLine($"Images: {chunks[ChunkType.ImageLink].Count}");
Console.WriteLine($"Links: {chunks[ChunkType.AdditionalLink].Count}");
Console.WriteLine($"Text chunks: {chunks[ChunkType.TextChunk].Count}");
Console.WriteLine($"Relations: {relationships.Length}");
```

---

## Practical scenarios

### Scenario 1: Knowledge graph building

```csharp
var documents = LoadDocuments();
var allChunks = documents.ExtractSemanticChunksDeeply(100, SemanticsType.Paragraph);
var relationships = allChunks.BuildRelationsGraph();
var duplicates = allChunks.FindRepeatedChunksWithUrls();
```

### Scenario 2: Vector search preparation

```csharp
var documents = LoadDocuments();
var allChunks = documents.ExtractSemanticChunksDeeply(200, SemanticsType.Sentence);
```

### Scenario 3: Export for external systems

```csharp
var chunks = document.ExtractSemanticChunksDeeply(100, SemanticsType.Paragraph);
var relationships = chunks.BuildRelationsGraph();
```

---

## Summary

These examples show the main capabilities of the library. For API details, see [API.md](API.md).
