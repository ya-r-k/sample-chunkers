# Architecture and components

## Architecture overview

`RagDataTools.Chunkers` is a layered .NET library with extension methods as the public API.

The architecture is split into:
1. Data models for chunks and relationships
2. Enums for chunk, relationship, and semantics types
3. Extension methods for text processing
4. Helper classes for regex and placeholder templates

## Component structure

### Models

#### ChunkModel

Represents one chunk of text or one structured element.

#### RelationshipModel

Represents a relationship between two chunks in the graph.

### Enums

#### ChunkType

- `TextChunk`
- `Table`
- `CodeBlock`
- `MathBlock`
- `InfoBlock`
- `ImageLink`
- `Topic`
- `AdditionalLink`

#### RelationshipType

- `Unknown`
- `StartsWith`
- `RelatedCodeBlock`
- `RelatedImage`
- `RelatedTable`
- `RelatedInfoBlock`
- `AdditionalLink`
- `HasNextTopic`
- `HasFirstSubtopic`
- `HasNextChunk`
- `LoadedFrom`

#### SemanticsType

- `Sentence`
- `Paragraph`

### Extensions

#### SimpleTextChunkerExtensions

Basic operations for plain text without structured elements.

#### ComplexDataChunkerExtensions

Extracts structured elements and performs deeper text processing.

#### ChunksExtensions

Works with chunk collections, builds the relation graph, and finds duplicates.

### Helpers

#### ChunkTypesRegexHelper

Contains compiled regular expressions for structured elements and uses `GeneratedRegex` for build-time compilation.

#### CommonRegexHelper

Contains common regex utilities such as multiple-space detection.

### Constants

#### ChunksConsts

Placeholder templates used when replacing structured elements in text.

## Data processing flow

### Scenario 1: Single document processing

```text
Text -> ExtractSemanticChunksDeeply
  |- RetrieveChunksFromText
  |   |- ExtractMarkdownCodeBlocks
  |   |- ExtractHtmlTables
  |   |- ExtractMarkdownInfoBlocks
  |   |- ExtractMarkdownImageLinks
  |   |- ExtractMarkdownLinks
  |   `- ExtractMarkdownHeaders
  |- SquashLabelsIntoWords
  |- PreprocessNaturalTextForChunking
  `- ExtractSemanticChunks
      |- GetWords
      |- ExtractSentenceStartIndices / ExtractParagraphStartIndexes
      `- GetChunks
```

### Scenario 2: Relation graph building

```text
Chunk collection -> BuildRelationsGraph
  |- BuildTextChunkSequenceRelations
  |- BuildTitlesSequenceRelations
  `- BuildRelationshipsForRelatedChunks
```

### Scenario 3: Duplicate detection

```text
Chunk collection -> FindRepeatedChunksWithUrls
  |- Filter URL chunks
  |- Group by URL
  `- Keep the first index for each URL
```

## Implementation notes

- Extraction order matters: code, tables, info blocks, images, links, headings
- Chunk indexes are assigned sequentially and accumulate across document collections
- HTML tables use nesting-depth tracking
- Markdown elements inside headings are extracted separately and linked back
- With overlap enabled, the nearest semantic boundary is selected for the next chunk

## Performance

- `GeneratedRegex` compiles regex at build time
- `Span<T>` is used for word handling without extra allocations
- `StringBuilder` is used for efficient text replacement
- Memory allocations are reduced through structure reuse

*** End of File

## Architecture overview

`RagDataTools.Chunkers` is organized as a layered .NET library with extension methods as the main public API. The design is split into:

1. Data models for chunks and relationships
2. Enums for chunk, relationship, and semantics types
3. Extension methods for text processing
4. Helper classes for regex and placeholder templates

## Component structure

### Models

#### ChunkModel

Represents one chunk of text or one structured element.

**Main fields:**
- `Index` - unique numeric chunk identifier
- `ChunkType` - chunk type such as text, code, table, or heading
- `RawContent` - original chunk content
- `Data` - type-specific metadata
- `RelatedChunksIndexes` - related chunk indexes grouped by chunk type

**Example:**
```csharp
var chunk = new ChunkModel
{
    Index = 1,
    ChunkType = ChunkType.CodeBlock,
    RawContent = "```csharp\nvar x = 1;\n```",
    Data = new Dictionary<string, object>
    {
        ["language"] = "csharp",
        ["content"] = "```csharp\nvar x = 1;\n```"
    },
    RelatedChunksIndexes = new Dictionary<ChunkType, List<int>>()
};
```

**Notes:**
- Implemented as a `record`
- `Data` stores type-specific information:
  - `CodeBlock`: `language`, `content`
  - `Topic`: `name`, `level`
  - `ImageLink` / `AdditionalLink`: `url`, `alterText`
  - `TextChunk`: `content`
  - `Table` / `InfoBlock`: `content`

#### RelationshipModel

Represents a relationship between two chunks in the graph.

**Main fields:**
- `FirstChunkIndex`
- `SecondChunkIndex`
- `RelationshipType`

**Relationship types:**
- `HasNextChunk` - next text chunk
- `HasNextTopic` - next heading at the same or a higher level
- `HasFirstSubtopic` - first subtopic at a lower level
- `RelatedCodeBlock` - related code block
- `RelatedTable` - related table
- `RelatedImage` - related image
- `RelatedInfoBlock` - related info block
- `AdditionalLink` - additional link
- `StartsWith` - heading starts with another element

**Example:**
```csharp
var relation = new RelationshipModel
{
    FirstChunkIndex = 1,
    SecondChunkIndex = 2,
    RelationshipType = RelationshipType.HasNextChunk
};
```

### Enums

#### ChunkType

Describes the type of extracted chunk.

- `TextChunk` (0) - plain text chunk
- `Table` (1) - HTML table
- `CodeBlock` (2) - Markdown code block
- `MathBlock` (3) - math block, not used in the current implementation
- `InfoBlock` (4) - Markdown blockquote / info block
- `ImageLink` (5) - Markdown image link
- `Topic` (6) - Markdown heading
- `AdditionalLink` (7) - Markdown external link

#### RelationshipType

Describes the type of relationship between chunks.

- `Unknown` (0) - unknown relationship
- `StartsWith` (1) - heading starts with another chunk
- `RelatedCodeBlock` (2) - related code block
- `RelatedImage` (3) - related image
- `RelatedTable` (4) - related table
- `RelatedInfoBlock` (5) - related info block
- `AdditionalLink` (6) - additional link
- `HasNextTopic` (7) - next heading
- `HasFirstSubtopic` (8) - first subtopic
- `HasNextChunk` (9) - next text chunk
- `LoadedFrom` (10) - loaded from source, not used

#### SemanticsType

Describes the semantic unit used for chunking.

- `Sentence` (1) - sentence-based splitting
- `Paragraph` (2) - paragraph-based splitting

### Extensions

#### SimpleTextChunkerExtensions

Basic operations for plain text without structured elements.

**Main methods:**

1. `GetWords(string text)`
   - Splits text into words
   - Returns a `Span<string>` word array
   - Splits on spaces and removes empty entries

2. `ExtractSentenceStartIndices(string text)`
   - Finds the start indexes of sentences
   - Returns the word indexes where sentences begin
   - Uses regex to detect `.`, `!`, `?`, and `:\n`

3. `ExtractParagraphStartIndexes(string text)`
   - Finds the start indexes of paragraphs
   - Returns the word indexes where paragraphs begin
   - Splits the text on `\n ` (newline plus space)

4. `PreprocessNaturalTextForChunking(string text)`
   - Prepares text for chunking
   - Removes extra spaces
   - Replaces non-breaking spaces with normal spaces
   - Normalizes line endings
   - Replaces em dash with a hyphen
   - Removes repeated spaces

5. `ExtractSemanticChunksFromText(string text, int chunkWordsCount, SemanticsType semanticsType, double overlapPercentage = 0.0)`
   - Main method for splitting text into semantic chunks
   - Respects sentence and paragraph boundaries
   - Supports overlap to preserve context
   - Returns a `string[]` array of text chunks

#### ComplexDataChunkerExtensions

Extracts structured elements and performs deeper text processing.

**Main methods:**

1. `ExtractSemanticChunksDeeply<T>(Dictionary<T, string> texts, ...)`
   - Processes a document collection with automatic indexing
   - Returns a nested document/chunk-type/chunk list dictionary
   - Indexes accumulate across documents
   - `T` must be `unmanaged`

2. `ExtractSemanticChunksDeeply(string text, ...)`
   - Main deep extraction method for one text
   - Extracts structured elements first
   - Replaces them with placeholders
   - Preprocesses the text
   - Extracts text chunks
   - Detects links back to extracted elements

3. `RetrieveChunksFromText(string text, ...)`
   - Extracts only structured elements without text chunks
   - Used when only tables, code, or links are needed

**Private extraction methods:**

- `ExtractMarkdownCodeBlocks`
- `ExtractMarkdownUnusualCodeBlocks`
- `ExtractHtmlTables`
- `ExtractMarkdownInfoBlocks`
- `ExtractMarkdownImageLinks`
- `ExtractMarkdownHeaders`
- `ExtractMarkdownLinks`

**Important order:**
code -> tables -> info blocks -> images -> links -> headings

After extraction, the text contains only placeholders. `SquashLabelsIntoWords` removes spaces around them, and `ExtractRelatedChunksIndexes` creates relationships.

#### ChunksExtensions

Works with chunk collections, builds the relation graph, and finds duplicates.

**Main methods:**

1. `BuildRelationsGraph<T>(Dictionary<T, Dictionary<ChunkType, List<ChunkModel>>> chunks)`
   - Builds a relation graph for a document collection
   - Returns `RelationshipModel[]`

2. `BuildRelationsGraph(Dictionary<ChunkType, List<ChunkModel>> chunks)`
   - Builds a relation graph for one document
   - Creates text chunk sequences
   - Builds heading hierarchy
   - Extracts relations from `RelatedChunksIndexes`

3. `FindRepeatedChunksWithUrls<T>(Dictionary<T, Dictionary<ChunkType, List<ChunkModel>>> chunks)`
   - Finds duplicate chunks with URLs
   - Works with `ImageLink` and `AdditionalLink`
   - Returns a `duplicate_index -> unique_index` map

### Helpers

#### ChunkTypesRegexHelper

Contains compiled regular expressions for structured elements:
- code blocks
- unusual code blocks
- blockquotes
- tables
- links
- headings
- images
- chunk placeholders

Uses `GeneratedRegex` for build-time compilation and is optimized for multiline text.

#### CommonRegexHelper

Contains common regex utilities, such as multiple-space detection.

### Constants

#### ChunksConsts

Placeholder templates used when replacing structured elements in text:
- `ExternalLinkTemplate`
- `ImageLinkTemplate`
- `TableTemplate`
- `CodeBlockTemplate`
- `InfoBlockTemplate`
- `HeaderTemplate`
- `RelatedChunkTemplate`

## Data processing flow

### Scenario 1: Single document processing

```text
Text -> ExtractSemanticChunksDeeply
  |- RetrieveChunksFromText
  |   |- ExtractMarkdownCodeBlocks
  |   |- ExtractHtmlTables
  |   |- ExtractMarkdownInfoBlocks
  |   |- ExtractMarkdownImageLinks
  |   |- ExtractMarkdownLinks
  |   `- ExtractMarkdownHeaders
  |- SquashLabelsIntoWords
  |- PreprocessNaturalTextForChunking
  `- ExtractSemanticChunks
      |- GetWords
      |- ExtractSentenceStartIndices / ExtractParagraphStartIndexes
      `- GetChunks
```

### Scenario 2: Relation graph building

```text
Chunk collection -> BuildRelationsGraph
  |- BuildTextChunkSequenceRelations
  |- BuildTitlesSequenceRelations
  `- BuildRelationshipsForRelatedChunks
```

### Scenario 3: Duplicate detection

```text
Chunk collection -> FindRepeatedChunksWithUrls
  |- Filter URL chunks
  |- Group by URL
  `- Keep the first index for each URL
```

## Implementation notes

### Extraction order

The extraction order matters for correct placeholder handling:
1. Code blocks
2. Tables
3. Info blocks
4. Images
5. Links
6. Headings

### Chunk indexing

- Indexes are assigned sequentially
- Document collection indexes accumulate between documents
- `lastUsedIndex` allows custom starting indexes

### Nesting handling

- HTML tables use nesting-depth tracking
- Markdown elements inside headings are extracted separately and then linked back

### Overlap handling

When `overlapPercentage > 0`, the target overlap index is calculated, the nearest semantic boundary is selected, and the next chunk starts from that boundary.

## Performance

- `GeneratedRegex` compiles regex at build time
- `Span<T>` is used for word handling without extra allocations
- `StringBuilder` is used for efficient text replacement
- Memory allocations are reduced through structure reuse
