# Contract: Keyword Extraction API

**Feature**: 001-connectors-file-ingest-keywords  
**Type**: Library public API (pluggable backend + timeout)

## Responsibility

Extract keywords (terms/phrases) from chunks using a configurable backend. Optional step; caller chooses when to run. Configurable timeout enforced by library; no retry on failure. Empty or minimal keywords for minimal text (no failure).

## Contract (conceptual)

- **Input**:
  - `chunks` (ChunkModel[] or equivalent): One or more chunks (typically from one document).
  - `options` or backend config: Includes timeout (e.g. TimeSpan or seconds). Caller may set or use documented default.
- **Output**: Same chunks with keywords attached (e.g. in Data["keywords"] or new structure). Or separate result: chunk id/index → keywords. Empty list per chunk when backend unavailable or minimal text.
- **Errors**: Timeout → clear error (e.g. exception or result). Backend unavailable → clear indication; caller can proceed without keywords. No indefinite hang; timeout enforced by library.
- **Backend**: Pluggable (e.g. IKeywordExtractor or IKeywordExtractionBackend). One implementation may call Ollama or similar; others remote API. Not mandated by spec.

## Dependencies

- RagDataTools.Chunkers.Models (ChunkModel). Optional: RagDataTools.Di for registering backend.
