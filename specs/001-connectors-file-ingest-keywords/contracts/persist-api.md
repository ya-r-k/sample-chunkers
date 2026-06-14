# Contract: Persist (Connector) API

**Feature**: 001-connectors-file-ingest-keywords  
**Type**: Library public API (existing interfaces extended or used with explicit scope id)

## Responsibility

Persist one set of chunks (and optionally relationships) under a caller-provided scope id. Replace any existing chunks for that scope (replace-by-scope). Single scope per call. No retry on failure; return clear error once.

## Contract (conceptual)

- **Input**: 
  - `scopeId` (string or value type): Caller-provided; identifies the document/scope. Same id → replace existing.
  - `chunks` (ChunkModel[]): Chunks to store. May include order index, heading level, page number, keywords in metadata.
  - Optional: `relationships` (RelationshipModel[] or equivalent) when store supports it.
- **Output**: Success (void or result type) or clear failure (exception or result).
- **Errors**: Connector unavailable, write failure, timeout (if applicable). No retry; caller responsible for retry.
- **Existing interfaces**: `IChunksRepository<TFlag, TId>` — extend or use with scope id as part of the contract (e.g. AddAsync(scopeId, chunks)). Neo4j: fix ChunkModel → Cypher mapping; parameterize labels. Qdrant: implement repository; store order and heading metadata.

## Dependencies

- RagDataTools.Chunkers.Models (ChunkModel, RelationshipModel).
- RagDataTools.Connectors.Interfaces.
- Store-specific drivers (Neo4j.Driver, Qdrant client).
