using FluentAssertions;
using Sample.Chunkers.Enums;
using Sample.Chunkers.Extensions;
using Sample.Chunkers.Models;
using Sample.Chunkers.UnitTests.TestData;

namespace Sample.Chunkers.UnitTests.Extensions;

public class ComplexDataChunkerExtensionsTests
{
    [Test]
    public void RetrieveChunksFromText_WithMixedContent_ShouldExtractAllChunks()
    {
        // Arrange
        var text = @"# Main Title

## Subtitle

Here is a paragraph with some text.

```csharp
public class Test
{
    public void Method() { }
}
```

<table class=""custom-table"">
    <thead>
        <tr>
            <th>Header 1</th>
            <th>Header 2</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>Data 1</td>
            <td>Data 2</td>
        </tr>
    </tbody>
</table>

![Image Alt](https://example.com/image.jpg)

## Another Subtitle

More text here.";

        var expectedHeaders = new[]
        {
            new ChunkModel
            {
                ChunkType = ChunkType.Title,
                RelatedChunksIndexes = [],
                RawContent = @"# Main Title",
                Data = new HeaderData
                {
                    Text = "Main Title",
                    Level = 1,
                },
            },
            new ChunkModel
            {
                ChunkType = ChunkType.Title,
                RelatedChunksIndexes = [],
                RawContent = @"## Subtitle",
                Data = new HeaderData
                {
                    Text = "Subtitle",
                    Level = 2,
                },
            },
            new ChunkModel
            {
                ChunkType = ChunkType.Title,
                RelatedChunksIndexes = [],
                RawContent = @"## Another Subtitle",
                Data = new HeaderData
                {
                    Text = "Another Subtitle",
                    Level = 2,
                },
            },
        };
        var expectedCodeBlocks = new[]
        {
            new ChunkModel
            {
                ChunkType = ChunkType.CodeBlock,
                RelatedChunksIndexes = [],
                RawContent = @"```csharp
public class Test
{
    public void Method() { }
}
```",
                Data = new CodeBlockData
                {
                    Language = "csharp",
                },
            },
        };
        var expectedTables = new[]
        {
            new ChunkModel
            {
                ChunkType = ChunkType.Table,
                RelatedChunksIndexes = [],
                RawContent = @"<table class=""custom-table"">
    <thead>
        <tr>
            <th>Header 1</th>
            <th>Header 2</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>Data 1</td>
            <td>Data 2</td>
        </tr>
    </tbody>
</table>",
            }
        };
        var expectedImages = new[]
        {
            new ChunkModel
            {
                ChunkType = ChunkType.ImageLink,
                RelatedChunksIndexes = [],
                RawContent = @"![Image Alt](https://example.com/image.jpg)",
                Data = new LinkData
                {
                    Url = "https://example.com/image.jpg",
                    TextDescription = "Image Alt",
                },
            }
        };
        
        // Act
        var chunks = text.RetrieveChunksFromText(withTables: true, withCodeBlocks: true, withImages: true, withLinks: true);

        // Assert
        var chunksList = chunks.SelectMany(x => x.Value).ToArray();
        chunksList.Should().HaveCount(6); // 3 headers, 1 code block, 1 table, 1 image

        var headers = chunks[ChunkType.Title];
        headers.Should().BeEquivalentTo(expectedHeaders, options => options
            .Excluding(x => x.Index));

        var codeBlocks = chunks[ChunkType.CodeBlock];
        codeBlocks.Should().BeEquivalentTo(expectedCodeBlocks, options => options
            .Excluding(x => x.Index));

        var tables = chunks[ChunkType.Table];
        tables.Should().BeEquivalentTo(expectedTables, options => options
            .Excluding(x => x.Index));

        var images = chunks[ChunkType.ImageLink];
        images.Should().BeEquivalentTo(expectedImages, options => options
            .Excluding(x => x.Index));
    }

    [Test]
    public void RetrieveChunksFromText_WithNestedContent_ShouldExtractCorrectly()
    {
        // Arrange
        var text = @"# Title with `inline code`

## Subtitle with [link](https://example.com)

```html
<table>
    <tr><td>Nested table</td></tr>
</table>
```

<table>
    <tr>
        <td>
            <h1>Nested header</h1>
            <img src=""nested.jpg"" alt=""Nested image"">
        </td>
    </tr>
</table>";

        var expectedCodeBlocks = new[]
        {
            new ChunkModel
            {
                ChunkType = ChunkType.CodeBlock,
                RelatedChunksIndexes = [],
                RawContent = @"```html
<table>
    <tr><td>Nested table</td></tr>
</table>
```",
                Data = new CodeBlockData
                {
                    Language = "html",
                },
            },
        };
        var expectedTables = new[]
        {
            new ChunkModel
            {
                ChunkType = ChunkType.Table,
                RelatedChunksIndexes = [],
                RawContent = @"<table>
    <tr>
        <td>
            <h1>Nested header</h1>
            <img src=""nested.jpg"" alt=""Nested image"">
        </td>
    </tr>
</table>",
            }
        };
        var expectedLinks = new[]
        {
            new ChunkModel
            {
                Index = 3,
                ChunkType = ChunkType.ExternalLink,
                RelatedChunksIndexes = [],
                RawContent = @"[link](https://example.com)",
                Data = new LinkData
                {
                    Url = "https://example.com",
                    TextDescription = "link",
                },
            }
        };
        var expectedHeaders = new[]
        {
            new ChunkModel
            {
                ChunkType = ChunkType.Title,
                RelatedChunksIndexes = [],
                RawContent = @"# Title with `inline code`",
                Data = new HeaderData
                {
                    Text = "Title with `inline code`",
                    Level = 1,
                },
            },
            new ChunkModel
            {
                ChunkType = ChunkType.Title,
                RelatedChunksIndexes = new()
                {
                    [expectedLinks[0].ChunkType] = [expectedLinks[0].Index],
                },
                RawContent = @$"## Subtitle with link[RELATEDCHUNK]External-Link-{expectedLinks[0].Index}[/RELATEDCHUNK]",
                Data = new HeaderData
                {
                    Text = "Subtitle with link",
                    Level = 2,
                },
            },
        };


        // Act
        var chunks = text.RetrieveChunksFromText(withTables: true, withCodeBlocks: true, withImages: true, withLinks: true);

        // Assert
        var chunksList = chunks.SelectMany(x => x.Value).ToArray();
        chunksList.Should().HaveCount(5); // 2 headers, 1 code block, 1 table, 1 link

        var headers = chunks[ChunkType.Title];
        headers.Should().BeEquivalentTo(expectedHeaders, options => options
            .Excluding(x => x.Index));

        var codeBlocks = chunks[ChunkType.CodeBlock];
        codeBlocks.Should().BeEquivalentTo(expectedCodeBlocks, options => options
            .Excluding(x => x.Index));

        var tables = chunks[ChunkType.Table];
        tables.Should().BeEquivalentTo(expectedTables, options => options
            .Excluding(x => x.Index));

        var links = chunks[ChunkType.ExternalLink];
        links.Should().BeEquivalentTo(expectedLinks);
    }

    [Test]
    public void ReplaceChunksWithLabels_WithMixedContent_ShouldReplaceAllChunks()
    {
        // Arrange
        var text = @"# Main Title

## Subtitle

Here is a paragraph with some text.

```csharp
public class Test
{
    public void Method() { }
}
```

<table class=""custom-table"">
    <thead>
        <tr>
            <th>Header 1</th>
            <th>Header 2</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>Data 1</td>
            <td>Data 2</td>
        </tr>
    </tbody>
</table>

![Image Alt](https://example.com/image.jpg)

### Another Subtitle

More text here.";

        var codeBlocksIndexes = new[] { 1 };
        var tablesIndexes = new[] { 2 };
        var imagesIndexes = new[] { 3 };
        var headerIndexes = new[] { 4, 5, 6 };

        var expectedLabels = codeBlocksIndexes.Select(x => string.Format(ChunksConsts.CodeBlockTemplate, x))
            .Concat(tablesIndexes.Select(x => string.Format(ChunksConsts.TableTemplate, x)))
            .Concat(imagesIndexes.Select(x => string.Format(ChunksConsts.ImageLinkTemplate, x)))
            .Concat(headerIndexes.Select(x => string.Format(ChunksConsts.HeaderTemplate, x)))
            .ToArray();

        var expectedResult = @$"{expectedLabels[3]}{expectedLabels[4]}Here is a paragraph with some text.

{expectedLabels[0]}{expectedLabels[1]}{expectedLabels[2]}{expectedLabels[5]}More text here.";

        // Act
        var chunks = text.RetrieveChunksFromText(withTables: true, withCodeBlocks: true, withImages: true, withLinks: true);
        var processedText = text.ReplaceChunksWithLabels(chunks);

        // Assert
        processedText.Should().BeEquivalentTo(expectedResult);
    }

    [Test]
    public void ReplaceChunksWithLabels_WithNestedContent_ShouldReplaceCorrectly()
    {
        // Arrange
        var text = @"# Title with `inline code`

## Subtitle with [link](https://example.com)

```html
<table>
    <tr><td>Nested table</td></tr>
</table>
```

<table>
    <tr>
        <td>
            <h1>Nested header</h1>
            <img src=""nested.jpg"" alt=""Nested image"">
        </td>
    </tr>
</table>";

        var codeBlocksIndexes = new[] { 1 };
        var tablesIndexes = new[] { 2 };
        var headerIndexes = new[] { 4, 5 };

        var expectedLabels = codeBlocksIndexes.Select(x => string.Format(ChunksConsts.CodeBlockTemplate, x))
            .Concat(tablesIndexes.Select(x => string.Format(ChunksConsts.TableTemplate, x)))
            .Concat(headerIndexes.Select(x => string.Format(ChunksConsts.HeaderTemplate, x)))
            .ToArray();

        var expectedResult = @$"{expectedLabels[2]}{expectedLabels[3]}{expectedLabels[0]}{expectedLabels[1]}";

        // Act
        var chunks = text.RetrieveChunksFromText(withTables: true, withCodeBlocks: true, withImages: true, withLinks: true);
        var processedText = text.ReplaceChunksWithLabels(chunks);

        // Assert
        processedText.Should().BeEquivalentTo(expectedResult);
    }

    [Test]
    public void ExtractSemanticChunksDeeply_WithRealWorldText_ShouldReturnCorrectChunks()
    {
        // Arrange
        var text = ArticlesTestData.DevToRealWorldArticleText;
        var expectedCodeBlocks = CodeBlocksTestData.DevToRealWorldArticleCodeBlocks;
        var expectedLinks = LinksTestData.DevToRealWorldArticleLinks;
        var expectedTexts = TextChunkTestData.DevToRealWorldArticleTextChunks;

        // Act
        var chunks = text.ExtractSemanticChunksDeeply(200, SemanticsType.Sentence, 0.5, withTables: true, withCodeBlocks: true, withImages: true, withLinks: true);

        // Assert
        var chunkList = chunks.SelectMany(x => x.Value).ToArray();
        chunkList.Should().NotBeEmpty();

        var codeBlocks = chunks[ChunkType.CodeBlock];
        codeBlocks.Should().BeEquivalentTo(expectedCodeBlocks);

        var textsChunks = chunks[ChunkType.Text];
        textsChunks.Should().BeEquivalentTo(expectedTexts, options => options
            .Excluding(x => x.Index));

        var links = chunks[ChunkType.ExternalLink];
        links.Should().BeEquivalentTo(expectedLinks);
    }

    [Test]
    public void ExtractSemanticChunksDeeply_WithRealWorldTextsCollection_ShouldReturnCorrectChunks()
    {
        // Arrange
        var texts = new[]
        {
            // https://dev.to/alex_ricciardi/recursion-in-programming-techniques-benefits-and-limitations-java-3o4p
            ArticlesTestData.DevToRealWorldArticleText,
            // https://www.geeksforgeeks.org/data-modeling-a-comprehensive-guide-for-analysts/
            ArticlesTestData.GeeksForGeeksTextAboutDataModeling,
            // https://www.geeksforgeeks.org/basic-operators-in-relational-algebra-2/
            ArticlesTestData.GeeksForGeeksTextAboutRelationalAlgebra,
        };

        var secondShiftValue = CodeBlocksTestData.DevToRealWorldArticleCodeBlocks.Length +
                               LinksTestData.DevToRealWorldArticleLinks.Length +
                               TextChunkTestData.DevToRealWorldArticleTextChunks.Length;
        var thirdShiftValue = secondShiftValue +
                              ImageLinksTestData.GeeksForGeeksAboutDataModelingImageLinks.Length +
                              LinksTestData.GeeksForGeeksAboutDataModelingLinks.Length +
                              HeadersTestData.GeeksForGeeksAboutDataModelingHeaders.Length +
                              TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks.Length;
        var indexShiftValues = new[]
        {
            0,
            secondShiftValue,
            thirdShiftValue,
        };

        var expectedCodeBlocks = new Dictionary<int, ChunkModel[]>
        {
            [0] = CodeBlocksTestData.DevToRealWorldArticleCodeBlocks,
            [2] = CodeBlocksTestData.GeeksForGeeksAboutRelationalAlgebraCodeBlocks,
        }.SelectMany(x => x.Value.Select(y => new ChunkModel
            {
                Index = y.Index + indexShiftValues[x.Key],
                ChunkType = y.ChunkType,
                RawContent = y.RawContent,
                RelatedChunksIndexes = y.RelatedChunksIndexes,
                Data = y.Data,
            })).ToArray();
        var expectedTables = new Dictionary<int, ChunkModel[]>
        {
            [2] = TablesTestData.GeeksForGeeksAboutRelationalAlgebraTables,
        }.SelectMany(x => x.Value.Select(y => new ChunkModel
            {
                Index = y.Index + indexShiftValues[x.Key],
                ChunkType = y.ChunkType,
                RawContent = y.RawContent,
                RelatedChunksIndexes = y.RelatedChunksIndexes,
                Data = y.Data,
            })).ToArray();
        var expectedImageLinks = new Dictionary<int, ChunkModel[]>
        {
            [1] = ImageLinksTestData.GeeksForGeeksAboutDataModelingImageLinks,
            [2] = ImageLinksTestData.GeeksForGeeksAboutRelationalAlgebraImageLinks,
        }.SelectMany(x => x.Value.Select(y => new ChunkModel
            {
                Index = y.Index + indexShiftValues[x.Key],
                ChunkType = y.ChunkType,
                RawContent = y.RawContent,
                RelatedChunksIndexes = y.RelatedChunksIndexes,
                Data = y.Data,
            })).ToArray();
        var expectedLinks = new Dictionary<int, ChunkModel[]>
        {
            [0] = LinksTestData.DevToRealWorldArticleLinks,
            [1] = LinksTestData.GeeksForGeeksAboutDataModelingLinks,
            [2] = LinksTestData.GeeksForGeeksAboutRelationalAlgebraLinks,
        }.SelectMany(x => x.Value.Select(y => new ChunkModel
            {
                Index = y.Index + indexShiftValues[x.Key],
                ChunkType = y.ChunkType,
                RawContent = y.RawContent,
                RelatedChunksIndexes = y.RelatedChunksIndexes.Select(y => new
                {
                    y.Key,
                    Value = y.Value.Select(y => y + indexShiftValues[x.Key]).ToList(),
                }).ToDictionary(x => x.Key, x => x.Value),
                Data = y.Data,
            })).ToArray();
        var expectedHeaders = new Dictionary<int, ChunkModel[]>
        {
            [1] = HeadersTestData.GeeksForGeeksAboutDataModelingHeaders,
            [2] = HeadersTestData.GeeksForGeeksAboutRelationalAlgebraHeaders,
        }.SelectMany(x => x.Value.Select(y => new ChunkModel
            {
                Index = y.Index + indexShiftValues[x.Key],
                ChunkType = y.ChunkType,
                RawContent = y.RawContent,
                RelatedChunksIndexes = y.RelatedChunksIndexes.Select(y => new
                {
                    y.Key,
                    Value = y.Value.Select(y => y + indexShiftValues[x.Key]).ToList(),
                }).ToDictionary(x => x.Key, x => x.Value),
                Data = y.Data,
            })).ToArray();
        var expectedTextsChunks = new Dictionary<int, ChunkModel[]>
        {
            [0] = TextChunkTestData.DevToRealWorldArticleTextChunks,
            [1] = TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks,
            [2] = TextChunkTestData.GeeksForGeeksAboutRelationalAlgebraTextChunks,
        }.SelectMany(x => x.Value.Select(y => new ChunkModel
            {
                Index = y.Index + indexShiftValues[x.Key],
                ChunkType = y.ChunkType,
                RawContent = y.RawContent,
                RelatedChunksIndexes = y.RelatedChunksIndexes.Select(y => new
                {
                    y.Key,
                    Value = y.Value.Select(y => y + indexShiftValues[x.Key]).ToList(),
                }).ToDictionary(x => x.Key, x => x.Value),
                Data = y.Data,
            }));

        // Act
        var chunks = texts.ExtractSemanticChunksDeeply(200, SemanticsType.Sentence, 0.5, withTables: true, withCodeBlocks: true, withImages: true, withLinks: true);

        // Assert
        var chunkList = chunks.SelectMany(x => x.Value).ToArray();
        chunkList.Should().NotBeEmpty();

        var codeBlocks = chunks[ChunkType.CodeBlock];
        codeBlocks.Should().BeEquivalentTo(expectedCodeBlocks);

        var tables = chunks[ChunkType.Table];
        tables.Should().BeEquivalentTo(expectedTables);

        var links = chunks[ChunkType.ExternalLink];
        links.Should().BeEquivalentTo(expectedLinks);

        var imageLinks = chunks[ChunkType.ImageLink];
        imageLinks.Should().BeEquivalentTo(imageLinks);

        var headers = chunks[ChunkType.Title];
        headers.Should().BeEquivalentTo(expectedHeaders);

        var textsChunks = chunks[ChunkType.Text];
        textsChunks.Should().BeEquivalentTo(expectedTextsChunks);
    }
}
