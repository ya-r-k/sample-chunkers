# Research: Connectors, File Ingest, Keyword Extraction

**Feature**: 001-connectors-file-ingest-keywords  
**Purpose**: Resolve technology choices and patterns for document conversion, connectors, keyword extraction, and test data.

---

## 1. PDF / Word / HTML conversion to Markdown

**Decision**: Use dedicated, widely used .NET libraries for each format; convert to Markdown in a single pipeline; enforce documented max size per format (bytes or pages) and strict fail on malformed input.

**Rationale**: Spec requires PDF, Word, HTML → Markdown before chunking; strict fail (no partial result); single-document per call. Existing chunking is regex/Markdown-based; conversion must produce Markdown that existing Chunkers can process without modification.

**Alternatives considered**:
- **PdfPig / iText / DocNET for PDF**: PdfPig (Apache 2.0, text extraction + structure) or similar; avoid heavy PDF renderers. Choose one that supports page-by-page text and page numbers.
- **Open XML SDK / DocX for Word**: Microsoft-supported, good for .docx; page boundaries may be approximate (Word is not page-based by nature); document max size in bytes or page estimate.
- **HtmlAgilityPack / AngleSharp for HTML**: Already common in .NET; convert to Markdown via a small HTML→Markdown layer (or use Markdig’s HTML handling if sufficient). Prefer not adding a separate “HTML chunking” path; always go HTML → Markdown → existing chunking.

**Implementation note**: One conversion abstraction (e.g. `IDocumentToMarkdownConverter`) with format-specific implementations; output includes optional page boundaries for PDF/Word. Documented limits per format in code and in docs (FR-011).

---

## 2. Keyword extraction backend (Ollama or other)

**Decision**: Pluggable abstraction (e.g. `IKeywordExtractor` or `IKeywordExtractionBackend`) with configurable timeout; one implementation can target Ollama (local LLM); others can target remote APIs. Library does not retry; caller gets clear error on failure or timeout.

**Rationale**: Spec says “configurable backend (e.g. local or remote language model)”; FR-007a requires configurable timeout enforced by library; no retry (caller responsibility).

**Alternatives considered**:
- **Ollama-only**: Rejected; spec requires pluggable backend.
- **Sync vs async**: Prefer async for timeout and non-blocking calls; timeout via `CancellationToken` or equivalent.
- **Batch extraction**: Spec is single-document/single-scope; keyword extraction can still process multiple chunks in one call (one document’s chunks) but one scope id. No batch across documents.

**Implementation note**: Interface returns keywords per chunk (e.g. `IReadOnlyList<string>` or similar); empty list for minimal text; timeout passed by caller or from options; no retry inside library.

---

## 3. Test data: fake input texts and expected output chunks

**Decision**: Implement a test data building layer that provides (1) fake input texts (plain, Markdown, HTML, and optionally minimal PDF/Word fixtures or stubs) and (2) expected chunk outputs (e.g. `ChunkModel[]` or equivalent) for known inputs. Use for unit tests (edge-case assertions) and for benchmark reproducibility (same input → compare duration). Do not remove existing test data in RagDataTools.UnitTests/Chunkers/TestData; extend or add a shared layer.

**Rationale**: User requirement: “implement test data building,” “input fake texts and output chunks for tests”; spec FR-009: tests focus on edge cases and stay minimal. Centralized, reusable test data reduces duplication and keeps benchmarks comparable.

**Alternatives considered**:
- **Inline strings only**: Kept for small cases; add a builder or factory for larger/structured fake texts and expected chunks.
- **External files (e.g. JSON)**: Optional for large fixtures; prefer in-code builders for clarity and compile-time safety.
- **Snapshot testing**: Possible later; not required for initial plan. Focus on explicit “given this fake text, expect these chunks” for edge cases (empty, malformed, large, special characters).

**Implementation note**: Add or extend `RagDataTools.UnitTests/TestData/` (or equivalent) with: `FakeTextBuilder` / static helpers for plain and Markdown texts; `ExpectedChunksBuilder` or expected results for key scenarios (empty, single chunk, multiple chunks with relationships, page numbers when applicable). Use in both unit tests and benchmark setups.

---

## 4. Performance and C# patterns (no removal of existing logic)

**Decision**: Preserve all existing Chunkers and Connectors code paths. New code follows: extension methods for new ingest/persist APIs; async where I/O (connectors, keyword extraction); `Span<T>`/`ReadOnlySpan<T>` and minimal allocations in hot paths where conversion produces text; reuse existing `ExtractSemanticChunksDeeply` and `BuildRelationsGraph` without duplicating chunking logic. BenchmarkDotNet for new hot paths (e.g. conversion + chunking); baseline remains existing Chunkers benchmarks; document in specs/PERFORMANCE.md.

**Rationale**: User requirement: “performance best practices C# patterns and not to remove existing logic”; constitution: no regression vs baseline; benchmarks for hot paths.

**Alternatives considered**:
- **Reimplement chunking**: Rejected; reuse existing Chunkers.
- **Synchronous only**: Rejected for connectors and keyword extraction; async for I/O.
- **No new benchmarks**: Rejected; conversion and connector write paths should have benchmarks where they are hot.

**Implementation note**: Conversion pipeline: stream or bounded read → convert to Markdown (with page boundaries if needed) → call existing `string.ExtractSemanticChunksDeeply(...)`. Avoid loading entire large file into a single string if format allows streaming; otherwise enforce documented max size and fail clearly when exceeded.

**Chunking sizing (2025-03-15)**: Primary unit is **word count** (max words per text chunk). Token-based chunk sizing may be added as an optional mode for comparison with word-based and for embedding-model alignment; benchmarks comparing word-based vs token-based on the same workloads should be planned. Chunking MUST NOT be only token-based; word-based remains default. See spec § Chunking strategy.

---

## 5. Connector scope id and replace-by-scope

**Decision**: Every persist call includes a caller-provided scope id (e.g. `string` or value type). Connectors (Neo4j, Qdrant) replace existing chunks for that scope before writing new chunks (upsert-by-scope). No batch; one scope per call. Existing `IChunksRepository<TFlag, TId>` is extended or used with scope id as part of the contract (e.g. via `TFlag` or an explicit parameter).

**Rationale**: Spec FR-012, FR-013; clarifications: caller-provided scope id, replace-by-scope, single-document only.

**Implementation note**: Neo4j: fix current implementation to map `ChunkModel` to Cypher parameters (no string interpolation of labels); add scope id to node labels or a dedicated property; delete or replace nodes for that scope before insert. Qdrant: implement `IChunksRepository` with scope id; store order and heading metadata in payload or metadata; replace points by scope id (e.g. delete by filter then upsert).

---

## Summary Table

| Topic | Decision | Key constraint |
|-------|----------|----------------|
| PDF/Word/HTML → Markdown | Dedicated libs; single pipeline; strict fail; documented max size | FR-003, FR-008, FR-011 |
| Keyword extraction | Pluggable backend; configurable timeout; no retry | FR-006, FR-007a |
| Test data | Build fake texts + expected chunks; reuse in tests and benchmarks | User request, FR-009 |
| Performance / C# | Reuse Chunkers; async for I/O; benchmark new hot paths; no removal | Constitution IV, user request |
| Connectors | Scope id in contract; replace-by-scope; one scope per call | FR-012, FR-013 |
