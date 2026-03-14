# Data Model: Connectors, File Ingest, Keyword Extraction

**Feature**: 001-connectors-file-ingest-keywords  
**Source**: [spec.md](./spec.md) Key Entities and Requirements

---

## Entities

### Chunk (existing; extend metadata)

- **Purpose**: Segment of content (text or structured element) with optional metadata; same conceptual model as existing RagDataTools.Chunkers. Text chunk size is governed by **word count** (max words per chunk) per spec § Chunking strategy; token-based sizing is optional.
- **Attributes** (existing + additions):
  - `Index` (int): Unique index in processing run.
  - `ChunkType` (enum): TextChunk, Table, CodeBlock, Topic, etc. (unchanged).
  - `RawContent` (string): Original content (unchanged).
  - `Data` (dictionary): Type-specific metadata (unchanged). **Add**: `page_number` (int?) for PDF/Word when applicable; `keywords` (string[] or list) when keyword extraction is used.
  - `RelatedChunksIndexes` (dictionary): Relationship types → chunk indices (unchanged).
- **Validation**: Index ≥ 0; ChunkType in enum; Data keys consistent with ChunkType; page_number ≥ 1 when present.
- **Lifecycle**: Created by chunking pipeline; optionally enriched with page number (ingest) and keywords (extraction); persisted via connector with scope id.

### Connector (existing abstraction; clarify contract)

- **Purpose**: Writes chunks to a store (vector or graph) for one scope id; replace-by-scope semantics.
- **Attributes**: Not a data entity; operational. Inputs: caller-provided scope id (string or value type), collection of chunks (and optionally relationships). Output: success or clear error (no retry).
- **Identity**: Scope id is caller-provided; “same scope” = same scope id. No library-derived identity.

### Document source (new for ingest)

- **Purpose**: Represents a single document input (file or in-memory content) before conversion.
- **Attributes**:
  - `Format` (enum or string): Pdf, Word, Html.
  - `Content` (stream or byte[] or path): Raw content or path; size bounded by documented max for format.
  - Optional: `FileName` or `ContentType` for error messages.
- **Validation**: Format supported; content length ≤ documented maximum for format (FR-011); malformed content → strict fail (no partial result).
- **Lifecycle**: Provided by caller (one per call); converted to Markdown + optional page boundaries; then chunked. Zero chunks from valid document → success with empty list.

### Keyword set (per chunk)

- **Purpose**: Result of keyword extraction for one chunk.
- **Attributes**: Ordered list or set of strings (terms/phrases). May be empty (minimal text or extraction unavailable).
- **Validation**: No null entries; empty list allowed. Stored in Chunk.Data or separate structure when persisting.
- **Lifecycle**: Produced by keyword extraction backend (with timeout); attached to chunk; optional when saving via connector.

### RelationshipModel (existing)

- **Purpose**: Link between two chunks (first index, second index, relationship type). Unchanged.
- **Persistence**: When persisting to vector DB, order and headings are preserved (e.g. metadata); graph DB can store relationships as first-class edges.

---

## State transitions

- **Document ingest**: Document source → (validate format & size) → convert to Markdown (+ page boundaries) → chunk (existing pipeline) → ChunkModel[] (with optional page_number in Data). On malformed: fail. On valid but zero chunks: return empty array.
- **Keyword extraction**: ChunkModel[] + backend + timeout → ChunkModel[] with keywords in Data (or equivalent). Timeout or failure → clear error; no partial result for that call.
- **Persist**: ChunkModel[] + scope id + connector → replace existing chunks for scope, then write chunks (and optional relationships). Failure → clear error once; no retry.

---

## Validation rules (from spec)

| Rule | Source |
|------|--------|
| Max input size per format documented and enforced | FR-011 |
| Malformed input → strict fail, no partial result | FR-008 |
| One document or one scope per call | FR-013 |
| Scope id caller-provided; replace-by-scope | FR-012 |
| Keyword extraction timeout configurable and enforced | FR-007a |
| Zero chunks from valid document → success with empty list | Edge case / clarification |
