# Contract: Document Ingest API

**Feature**: 001-connectors-file-ingest-keywords  
**Type**: Library public API (extension methods or service methods)

## Responsibility

Accept one document (PDF, Word, or HTML); validate format and size; convert to Markdown (with page boundaries for PDF/Word); run existing chunking pipeline; return chunks or clear error. Single document per call. Strict fail on malformed input; valid document with zero extractable content returns empty chunk list.

## Contract (conceptual)

- **Input**: One of: file path (string), or stream/bytes + format (Pdf | Word | Html). Optional: chunking options (e.g. chunkWordsCount, semanticsType, overlapPercentage) to pass to existing pipeline.
- **Output**: `ChunkModel[]` (or equivalent) with optional `page_number` in each chunk’s Data when applicable. Empty array when document yields zero chunks (success).
- **Errors**: Clear exception or result type when: unsupported format, size exceeds documented max, malformed content, I/O failure. No partial result on malformed.
- **Documented limits**: Max size (or page cap) per format must be documented (e.g. in XML docs or specs/); behavior when exceeded is defined (e.g. throw or return error).

## Dependencies

- Existing RagDataTools.Chunkers: `ExtractSemanticChunksDeeply`, `PrimitivesExtractors`, etc. No changes to Chunkers public API.
- Conversion: PDF/Word/HTML → Markdown (see research.md).
