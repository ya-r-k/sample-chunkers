# Quickstart: Connectors, File Ingest, Keyword Extraction

**Feature**: 001-connectors-file-ingest-keywords  
**Audience**: Developer integrating the library

## Prerequisites

- .NET 10.0
- RagDataTools.Chunkers (existing)
- RagDataTools.Connectors (existing)
- One of: RagDataTools.Connectors.Neo4j, RagDataTools.Connectors.Qdrant (or both)
- Optional: RagDataTools.Ingest (document conversion), RagDataTools keyword extraction package

## 1. Chunk text (existing; unchanged)

```csharp
using RagDataTools.Chunkers.Extensions;
using RagDataTools.Chunkers.Infrastructure;

var text = "Your Markdown or plain text...";
var chunks = text.ExtractSemanticChunksDeeply(
    chunkWordsCount: 100,
    indexesExtractor: PrimitivesExtractors.SentencesExtractor,
    overlapPercentage: 0.5);
var relationships = chunks.BuildRelationsGraph();
```

## 2. Persist chunks (with scope id)

```csharp
// One scope per call; replace-by-scope
var scopeId = "doc-123"; // Caller-provided
await connector.SaveAsync(scopeId, chunks);
// On failure: clear error, no retry. Caller may retry.
```

## 3. Ingest a document (PDF/Word/HTML → chunks)

```csharp
// Single document per call
var chunks = await ingestService.IngestAsync("path/to/file.pdf"); // or Word, HTML
// chunks have optional page_number in Data when applicable
// Empty list if document yields zero chunks (success)
// Throws or returns error on: unsupported format, size exceeded, malformed
```

## 4. Keyword extraction (optional)

```csharp
var options = new KeywordExtractionOptions { Timeout = TimeSpan.FromSeconds(30) };
var chunksWithKeywords = await keywordExtractor.ExtractAsync(chunks, options);
// Use chunksWithKeywords when persisting (keywords in metadata)
// On timeout or backend down: clear error; no retry
```

## 5. End-to-end (ingest → chunk → optional keywords → persist)

```csharp
var chunks = await ingestService.IngestAsync("document.pdf");
if (chunks.Length > 0)
{
    var withKeywords = await keywordExtractor.ExtractAsync(chunks, options);
    await connector.SaveAsync("doc-456", withKeywords);
}
```

## Test data (for unit tests and benchmarks)

Use the test data building layer: fake input texts and expected output chunks. See RagDataTools.UnitTests/TestData (or Chunkers/TestData extended). Enables edge-case assertions and reproducible benchmarks without removing existing logic.

## Performance

- Chunking: no regression vs baseline (specs/PERFORMANCE.md).
- New hot paths (conversion, connector write) have benchmarks where applicable.
- Documented max size per format; enforce before conversion to avoid unbounded memory.
