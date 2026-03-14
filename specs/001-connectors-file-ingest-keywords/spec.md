# Feature Specification: Connectors, File Ingest, and Keyword Extraction

**Feature Branch**: `001-connectors-file-ingest-keywords`  
**Created**: 2025-03-14  
**Status**: Draft  
**Input**: User description (connectors to persist chunks; PDF/Word/HTML and other formats converted to Markdown; page numbers preserved for PDF/Word; vector DB preserves order and headings; keyword extraction from chunks via language model; tests focused on edge cases and simplified).

## Clarifications

### Session 2025-03-14

- Q: How should chunking (and convert→chunk→persist) performance be specified? → A: No regression vs existing benchmarks — chunking MUST NOT regress vs current BenchmarkDotNet baseline; baseline documented in spec or linked docs.
- Q: How should very large document size be bounded for ingest (PDF/Word/HTML)? → A: Documented maximum per format — a maximum input size (or equivalent, e.g. page cap) MUST be documented per format; input above that is rejected or handled with a defined error.
- Q: When the caller persists chunks for a document that already exists in the store (same document/scope), what should the default behavior be? → A: Replace by scope — chunks for the same document/scope replace any existing chunks for that scope (upsert by document/scope id).
- Q: Who defines and enforces the timeout for keyword extraction (so there is no indefinite hang)? → A: Library enforces configurable timeout — library MUST accept a configurable timeout and MUST enforce it so extraction never hangs indefinitely; caller can set a value or use a documented default.
- Q: How is “same document/scope” determined when persisting chunks (for replace-by-scope)? → A: Caller-provided scope id — the caller supplies a scope (or document) identifier when saving; “same document/scope” means the same identifier; library does not derive it.
- Q: When a PDF/Word/HTML input is malformed, should the library return partial result + warning or always fail the whole operation? → A: Strict fail — on any malformed input the library MUST fail the operation and return a clear error (no partial result); caller retries or handles.
- Q: Is processing or persisting multiple documents in one call in scope? → A: Single-document only — one library call accepts one document (or one set of chunks for one scope); multiple documents require multiple calls; each call has one caller-provided scope id.
- Q: On transient connector or keyword-extraction failure, should the library retry or only report the error? → A: No retry; caller responsibility — library does not retry; on any failure it returns a clear error once; the caller is responsible for retrying if desired.
- Q: Where must the benchmark baseline be documented for no-regression acceptance? → A: In project docs — baseline MUST live in project docs (e.g. docs/PERFORMANCE.md); this spec MUST reference that doc; acceptance is no regression vs that doc.
- Q: When ingest succeeds but the document yields zero chunks (e.g. no extractable text), return empty list or error? → A: Success with empty list — ingest succeeds; library returns an empty chunk list; caller may handle zero chunks as they see fit.

### Chunking strategy (2025-03-15)

- **Primary sizing by words**: Chunk size is defined and controlled primarily by **word count** (configurable maximum words per text chunk). The library MUST support word-based chunking as the default and recommended approach. This avoids dependency on a tokenizer and keeps behavior predictable and comparable across environments.
- **Token-based sizing**: Token-based chunk sizing (e.g. for alignment with embedding model limits) MAY be supported as an **optional or alternative mode**. Where supported, the library SHOULD allow comparison of results and performance between word-based and token-based chunking (e.g. same document, same target size in words vs tokens) so that callers can evaluate trade-offs. Chunking MUST NOT be only token-based; word-based sizing remains the primary and default.
- **Semantic boundaries**: Chunk boundaries MUST respect semantic units (e.g. sentence or paragraph, configurable). Chunks MUST NOT split mid-sentence or mid-paragraph unless no boundary is available (e.g. degenerate input). Overlap between consecutive chunks (0–1, configurable) is supported; its effect on boundary placement MUST be documented.
- **Extractor order**: When multiple structured elements (code blocks, tables, headings, etc.) are extracted from Markdown, the order or priority of extractors MUST be documented and stable so that overlapping patterns have deterministic behavior.

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Persist chunks to storage via connectors (Priority: P1)

As a developer using the library, I can pass the library’s formed chunks to a connector so that the library persists them to a database (e.g. vector DB or graph DB) without me writing persistence code myself.

**Why this priority**: Core value: the library can save chunks itself; without this, the feature does not deliver its main goal.

**Independent Test**: Provide a connector configuration and a set of chunks; call “save”; verify chunks (and any required metadata) are present in the configured store. Delivers: end-to-end persistence without custom code.

**Acceptance Scenarios**:

1. **Given** a configured connector and a list of chunks with relationships, **When** the user requests save, **Then** all chunks are stored and can be retrieved or queried from the store.
2. **Given** a configured connector and chunks that include order and heading hierarchy, **When** saving to a vector store, **Then** chunk order and heading structure are preserved (e.g. as metadata or structure) so that sequence and hierarchy can be restored.
3. **Given** a connector that fails (e.g. store unavailable), **When** save is requested, **Then** the caller receives a clear failure indication and can handle it (no silent loss of data).

---

### User Story 2 - Ingest PDF, Word, HTML, and other formats as Markdown (Priority: P2)

As a developer, I can pass a document (PDF file, Word document, HTML file, or other supported format) into the library so that it is converted to Markdown and then chunked. For PDF and Word, page numbers are preserved in the converted content or in chunk metadata.

**Why this priority**: Enables “file in → chunks out” without external conversion; PDF/Word are common; HTML is already common on the web.

**Independent Test**: Provide a PDF, a Word document, and an HTML file; run ingest and chunking; verify output is Markdown-derived chunks and that PDF/Word chunks carry page number information. Delivers: single entry point for multiple file types.

**Acceptance Scenarios**:

1. **Given** a valid PDF file, **When** the user passes it to the library for ingest, **Then** the content is converted to Markdown and chunked, and each chunk that came from a specific page carries that page number (in content or metadata).
2. **Given** a valid Word document, **When** the user passes it for ingest, **Then** the content is converted to Markdown and chunked, and page numbers are preserved similarly to PDF.
3. **Given** an HTML file or fragment, **When** the user passes it for ingest, **Then** it is converted to Markdown (if needed) and chunking is applied; no separate “HTML-only” path is required for chunking.
4. **Given** an unsupported or corrupted file, **When** the user attempts ingest, **Then** the caller receives a clear error (e.g. unsupported format or parse failure) and can handle it.

---

### User Story 3 - Extract keywords from chunks (e.g. for search or metadata) (Priority: P3)

As a developer, I can run keyword extraction on chunks (using a configurable language-model backend) so that I get key terms or phrases per chunk for search, tagging, or metadata when persisting to a store.

**Why this priority**: Improves discoverability and retrieval; can be used optionally when saving chunks.

**Independent Test**: Provide chunks and a configured keyword-extraction backend; run extraction; verify each chunk receives a set of keywords (or empty if not applicable). Delivers: keywords usable for indexing or display without implementing extraction yourself.

**Acceptance Scenarios**:

1. **Given** one or more text chunks and a configured keyword-extraction service, **When** the user requests keyword extraction, **Then** each chunk is annotated with a set of keywords (or equivalent) that can be stored or displayed.
2. **Given** keyword extraction is unavailable (e.g. service down or not configured), **When** the user requests extraction, **Then** the caller receives a clear indication (e.g. optional step skipped or error) and can still proceed with chunking/persistence without keywords.
3. **Given** a chunk with very little or no meaningful text, **When** extraction runs, **Then** the result is empty or minimal keywords rather than a failure.

---

### Edge Cases

- Empty or zero-byte file: reject or return empty chunk list with a clear outcome; no crash.
- Valid document yields zero chunks (e.g. no extractable text, image-only): ingest succeeds; library returns an empty chunk list; caller may handle as they see fit.
- Very large single file: document exceeds the documented maximum for that format (see FR-011); caller receives a defined error; no silent truncation.
- Malformed PDF/Word/HTML: library MUST fail the operation and return a clear error (no partial result); no unhandled exception.
- Chunks with special characters or non-Latin scripts: keywords and persistence work without corruption or encoding errors.
- Vector store already contains chunks for the same “document” or run: replace-by-scope (see FR-012); existing chunks for that scope are replaced; no undefined overwrite.
- Keyword extraction timeout or rate limit: library enforces configurable timeout (see FR-007a); no indefinite hang; rate limiting is caller or backend responsibility. Transient connector or extraction failure: library does not retry; returns clear error once; caller responsible for retry.

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: The library MUST provide connectors that persist formed chunks to a backing store (e.g. vector DB or graph DB) so that callers do not need to implement persistence themselves. The library MUST NOT retry on connector or keyword-extraction failure; it MUST return a clear error once; the caller is responsible for retrying if desired.
- **FR-002**: When persisting to a vector store, the library MUST preserve chunk order and heading hierarchy (e.g. via metadata or structure) so that sequence and headings can be restored by the caller.
- **FR-003**: The library MUST accept document content from at least: PDF files, Word documents, and HTML (file or fragment). All such content MUST be converted to a common Markdown representation before chunking.
- **FR-004**: For PDF and Word ingest, the library MUST preserve page numbers (in the converted Markdown or in chunk metadata) so that each chunk can be associated with a source page when applicable.
- **FR-005**: The library MUST support HTML as input; chunking MUST be applied after conversion to Markdown (i.e. HTML is not a separate chunking path).
- **FR-006**: The library MUST provide keyword extraction from chunks using a configurable backend (e.g. a local or remote language model). Extraction MAY be optional (caller chooses when to run it).
- **FR-007**: Keyword extraction MUST be usable in combination with chunking and persistence (e.g. keywords can be stored as part of chunk metadata when saving via a connector).
- **FR-007a**: Keyword extraction MUST accept a configurable timeout and MUST enforce it so that extraction never hangs indefinitely; the caller MAY set a value or use a documented default.
- **FR-008**: Unsupported or invalid input (unsupported format, corrupted file, or malformed content) MUST result in a strict fail: the library MUST return a clear, handleable error and MUST NOT return partial results (no silent failure or unhandled exception).
- **FR-009**: Tests MUST focus on edge cases (empty input, malformed input, large input, encoding/special characters, store failures, extraction failures) and MUST be kept minimal (no redundant or duplicate coverage).

### Performance

- **FR-010**: Chunking performance MUST NOT regress versus the existing BenchmarkDotNet baseline (e.g. ExtractSemanticChunksFromText, ExtractSemanticChunksDeeply). The baseline MUST be documented in project docs (e.g. docs/PERFORMANCE.md); this spec references that document for acceptance. New features (e.g. file conversion, connector writes) SHOULD have benchmarks where they affect hot paths.
- **FR-011**: A maximum input size (or equivalent, e.g. page cap) MUST be documented per supported format (PDF, Word, HTML). Input that exceeds the documented maximum for that format MUST be rejected or handled with a defined error (no silent truncation).
- **FR-012**: When persisting chunks for a document/scope that already has chunks in the store, the default behavior MUST be replace-by-scope: existing chunks for that document/scope are replaced (upsert by document/scope id); no duplicate chunks for the same scope without explicit caller opt-in. Scope identity is determined by a caller-provided scope (or document) identifier at save time; the library does not derive it.
- **FR-013**: One library call MUST accept at most one document (for ingest) or one set of chunks under one scope id (for persist). Multiple documents or scopes require multiple calls; batch processing is out of scope for this feature.

### Key Entities

- **Chunk**: A segment of content (text or structured element) with optional metadata (e.g. order index, heading level, page number, keywords). Same conceptual model as existing chunking; may be extended with new metadata. Text chunk size is governed by **word count** (max words per chunk); optional token-based sizing may be available for comparison or embedding alignment.
- **Connector**: A component that takes chunks, a caller-provided scope (document) identifier, and optional metadata and writes them to a specific store (vector DB, graph DB, etc.), preserving order and headings when the store supports it. Replace-by-scope uses the same scope id to identify existing chunks to replace.
- **Document source**: A file or content in a format (PDF, Word, HTML, or other). Has format type and raw content or path; after conversion, yields Markdown and optional page boundaries.
- **Keyword set**: Per-chunk result of keyword extraction (terms or phrases) used for search, tagging, or metadata when persisting.

## Assumptions

- “Vector DB” and “graph DB” are used in the sense of existing or planned connectors (e.g. Qdrant, Neo4j); the exact list of connectors is an implementation detail.
- Keyword extraction backend is pluggable (e.g. local model vs remote API); the specification does not mandate a specific provider.
- “Other formats” beyond PDF, Word, and HTML may be added later; the first release targets at least these three.
- One call = one document or one scope; batch (multiple documents/scopes in one call) is out of scope.
- Tests are simplified by focusing only on edge cases and avoiding redundant scenarios; existing regression coverage style can be refined to match.
- **Chunking**: Primary sizing is by word count; token-based sizing is optional and for comparison/embedding alignment. Integration with token-limited embedding models may use word-based chunks with documented approximate word/token ratios or optional token counting; token-only chunking is not required.

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: A developer can persist a set of chunks to a configured store with a single library call and then verify that chunks (and order/heading metadata) are present in the store.
- **SC-002**: A developer can pass a PDF, a Word document, and an HTML file and receive Markdown-derived chunks with correct page-number attribution for PDF and Word, without implementing conversion or chunking themselves.
- **SC-003**: A developer can obtain keywords for chunks via a configured extraction backend and use those keywords when persisting chunks (e.g. in metadata).
- **SC-004**: Invalid or unsupported input results in a clear, handleable error in 100% of specified edge cases (no unhandled exceptions or silent data loss).
- **SC-005**: Test suite is limited to edge-case scenarios and is easier to maintain (e.g. fewer tests than a full matrix, with clear coverage of boundaries and failure modes).
- **SC-006**: Chunking benchmarks show no regression against the baseline documented in project docs (e.g. docs/PERFORMANCE.md); same or better results for the existing benchmarked operations.
