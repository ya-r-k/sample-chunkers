# Implementation Plan: Connectors, File Ingest, and Keyword Extraction

**Branch**: `001-connectors-file-ingest-keywords` | **Date**: 2025-03-14 | **Spec**: [spec.md](./spec.md)  
**Input**: Feature specification from `specs/001-connectors-file-ingest-keywords/spec.md`

## Summary

Implement connectors that persist formed chunks to vector/graph stores (extend existing Neo4j; implement Qdrant); add document ingest from PDF, Word, and HTML with conversion to Markdown and chunking (preserving page numbers for PDF/Word); add keyword extraction from chunks via a configurable backend with configurable timeout. All changes preserve existing Chunkers logic and extension-method API. Single-document/single-scope per call; strict fail on malformed input; no retry in library. Include test data building (fake input texts and expected output chunks) and benchmarks for new hot paths; document baseline in specs/PERFORMANCE.md.

## Technical Context

**Language/Version**: C#, .NET 10.0 (net10.0)  
**Primary Dependencies**: Markdig (existing), Neo4j.Driver (existing), Qdrant client (to add), PDF/Word/HTML conversion libraries (see research.md), optional LLM backend for keyword extraction (pluggable)  
**Storage**: Vector DB (Qdrant), graph DB (Neo4j); connectors persist chunks with caller-provided scope id; replace-by-scope semantics  
**Testing**: NUnit 4, FluentAssertions, coverlet; edge-case-focused tests; test data: built-in fake texts and expected chunks for reproducible assertions  
**Target Platform**: .NET 10.0 (cross-platform class library)  
**Project Type**: Class library (RAG data tools); no web API  
**Performance Goals**: No regression vs existing BenchmarkDotNet baseline (specs/PERFORMANCE.md); new conversion/connector paths benchmarked where hot; avoid unnecessary allocations and repeated regex work. Per constitution IV, specs/PERFORMANCE.md MUST address comparison with comparable libraries (including Python analogues)—e.g. document baseline workload for future comparison or state comparison methodology/out-of-scope for v1.
**Constraints**: Single document/scope per call; no retry in library; strict fail on malformed input; configurable timeout for keyword extraction; documented max input size per format  
**Scale/Scope**: One call = one document or one scope; batch out of scope
**Documentation**: New public APIs introduced by this feature will have XML documentation comments and short example coverage in the API docs/contracts.

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

| Principle | Status | Notes |
|-----------|--------|--------|
| I. Class library for chunking | Pass | New code is library-only; no HTTP/host; existing Chunkers unchanged. |
| II. Regex-based chunk identification | Pass | Existing chunking stays regex/pattern-based; conversion to Markdown feeds into same pipeline. |
| III. Tests and benchmarks | Pass | Unit tests (edge-case-focused) and BenchmarkDotNet; test data building (fake texts, expected chunks); baseline in specs/PERFORMANCE.md; XML-doc task tracked in T038. |
| IV. Performance over peers | Pass | No regression vs baseline; new hot paths benchmarked; test data supports reproducible perf tests. specs/PERFORMANCE.md MUST document baseline and address comparison with Python analogues (see tasks.md T037). |
| V. Layered design | Pass | Chunkers = domain/API; Connectors = infrastructure; new ingest/conversion in new or existing projects without removing existing logic. |

## Project Structure

### Documentation (this feature)

```text
specs/001-connectors-file-ingest-keywords/
├── plan.md              # This file
├── research.md          # Phase 0: conversion libraries, keyword backend, test data strategy
├── data-model.md        # Phase 1: entities and validation
├── quickstart.md        # Phase 1: usage examples
├── contracts/           # Phase 1: public API contracts
└── tasks.md             # Phase 2: /speckit.tasks output
```

### Source Code (repository root)

```text
RagDataTools.Chunkers/           # UNCHANGED core; reuse ExtractSemanticChunksDeeply, BuildRelationsGraph, etc.
├── Extensions/
├── Infrastructure/
├── Interfaces/
├── Models/                      # Extend ChunkModel metadata (page number, etc.) if needed; do not remove
└── Strategies/

RagDataTools.Connectors/         # Extend interfaces for scope id; optional relationships repo
├── Extensions/                  # Optional: extension methods for save with scope id
└── Interfaces/

RagDataTools.Connectors.Neo4j/   # Extend for scope id, replace-by-scope; fix ChunkModel→Cypher mapping
└── Repositories/

RagDataTools.Connectors.Qdrant/  # Implement IChunksRepository; vector persistence with order/headings
└── Repositories/

RagDataTools.Di/                 # Register connectors, optional keyword extraction, timeouts

# NEW or EXTENDED (per research.md): document conversion + ingest API
RagDataTools.Ingest/             # NEW: PDF/Word/HTML → Markdown, then call Chunkers; one doc per call; max size per format
├── Conversion/                  # PDF, Word, HTML converters (or separate small projects)
├── Extensions/                  # Ingest(string path/stream, format) → ChunkModel[]
└── (optional) RagDataTools.KeywordExtraction/  # Pluggable backend + timeout; optional

RagDataTools.UnitTests/
├── Chunkers/                    # EXISTING; add edge-case tests using test data
├── TestData/                    # BUILD: fake input texts and expected output chunks (reusable)
├── Connectors/                  # NEW: connector tests (e.g. in-memory/fake store)
├── Ingest/                      # NEW: conversion + ingest edge cases
└── KeywordExtraction/           # NEW: timeout, empty chunk, unavailable backend

RagDataTools.Benchmarks/
├── ChunkersBenchmarks.cs        # EXISTING; keep; ensure no regression
└── (add) IngestBenchmarks.cs    # Optional: conversion + chunking hot path
```

**Structure Decision**: Keep existing Chunkers, Connectors, Connectors.Neo4j, Connectors.Qdrant, Di, UnitTests, Benchmarks. Add RagDataTools.Ingest for document conversion and ingest (single doc → Markdown → chunk); add or extend RagDataTools.KeywordExtraction (or keep inside Connectors/Ingest). Test data: centralized fake texts and expected chunks in UnitTests/TestData (or Chunkers/TestData extended) for both unit tests and benchmark reproducibility. No removal of existing logic.

## Chunking strategy (from spec and checklists)

- **Primary: word count**. Chunk size is controlled by **maximum words per text chunk** (existing behaviour). Token-based sizing is optional; when supported, enable **comparison** of word-based vs token-based chunking (same document, comparable target size) for evaluation and optional embedding alignment. See spec § Chunking strategy.
- **Semantic boundaries**. Boundaries are sentence or paragraph (configurable via existing indexes extractors); overlap 0–1 configurable; extractor order documented below.
- **Extractor order** (deterministic, documented): CodeBlock → UnusualBlock → HtmlTable → InfoBlock → ImageLink → ExternalLink → Heading (current chain in ComplexDataChunkerExtensions). Document in code comments and, if needed, in docs so overlapping patterns have defined behaviour.
- **Baseline and regression**. No regression vs existing BenchmarkDotNet baseline (ExtractSemanticChunksFromText, ExtractSemanticChunksDeeply, BuildRelationsGraph); baseline in specs/PERFORMANCE.md. New chunking API (after refactor) must preserve or improve these benchmarks.

## Refactoring: from static extensions to testable design

**Goal**: Replace reliance on static extension methods with an instance-based or injectable chunking design, without breaking existing behaviour or performance.

**Constraints**:
- **No breaking changes**: Existing public API (e.g. `text.ExtractSemanticChunksDeeply(...)`, `chunks.BuildRelationsGraph()`) MUST remain available. Extensions can become thin facades that delegate to the new implementation.
- **No performance regression**: BenchmarkDotNet results for current chunking operations MUST not regress. Prefer reusing the same hot-path logic behind an abstraction rather than rewriting.
- **Testability**: New code paths SHOULD be testable via interfaces or injectable services (e.g. `IChunkingPipeline`, `ISemanticChunkExtractor`) so that unit tests and benchmarks can target chunking logic without depending on static state.

**Phased approach**:
1. **Phase A (optional, post–minimal feature)**: Introduce an instance-based chunking service (e.g. in RagDataTools.Chunkers) that encapsulates the current logic (extractors chain, word/semantic indexes, overlap). Keep existing extension methods implemented by delegating to a default instance or a shared implementation. No change to public API or benchmark results.
2. **Phase B**: Move internal logic (regex providers, extractors chain, text preprocessing) behind interfaces or injectable dependencies so that tests and future implementations (e.g. token-based sizing) can plug in. Extensions continue to call the default implementation.
3. **Phase C**: If needed, add optional token-based chunk sizing as an alternative implementation (same interface), with benchmarks comparing word-based vs token-based on the same workloads. Word-based remains the default.

**Out of scope for initial feature**: Full removal of extension methods from the public API; that would be a breaking change. Only internal refactor and optional new entry points (e.g. service-based API) are in scope.

## Checklist application (2025-03-15)

Checklists under `specs/001-connectors-file-ingest-keywords/checklists/` were applied to this plan and to [spec.md](./spec.md). Summary of decisions:

| Checklist | Decisions reflected |
|-----------|---------------------|
| **requirements.md** | Already fully satisfied; no change. |
| **chunking-logic.md** | Spec: word-based primary, token optional/comparison; overlap and semantic boundaries documented; extractor order documented in plan. Assumption: chunking word-based documented. Refactoring: plan includes move away from static extensions without breaking API or perf. |
| **performance.md** | FR-010, SC-006, baseline in specs/PERFORMANCE.md; no-regression scope = ExtractSemanticChunksFromText, ExtractSemanticChunksDeeply, BuildRelationsGraph. Plan and refactor section align. |
| **benchmarks-chunking.md** | Baseline and workload scope tied to existing benchmarked operations; plan references specs/PERFORMANCE.md. |
| **testing-chunks.md** | Edge-case test focus (FR-009); test data (fake texts, expected chunks) in plan; acceptance scenarios in spec. |
| **chunk-graph-keywords.md** | FR-002 (order and heading hierarchy); Key Entities and data-model define chunk and relationships. |
| **throughput-scale.md** | N documents = N calls (FR-013); per-document time from baseline; no batch guarantee. Spec and plan leave throughput/latency as “no regression” + baseline; explicit throughput targets deferred. |

Items in each checklist that are satisfied by the current spec and this plan are marked `[x]` in the checklist files.

## Complexity Tracking

> No constitution violations. Optional justification if adding a new project (RagDataTools.Ingest or KeywordExtraction).

| Violation | Why Needed | Simpler Alternative Rejected Because |
|-----------|------------|-------------------------------------|
| (None) | — | — |

If RagDataTools.Ingest is a new project: keeps conversion and max-size logic separate from Chunkers (no regression risk to core chunking). Alternative of putting conversion inside Chunkers would mix concerns and increase regression surface.
