# Tasks: Connectors, File Ingest, and Keyword Extraction

**Input**: Design documents from `specs/001-connectors-file-ingest-keywords/`  
**Prerequisites**: plan.md, spec.md, research.md, data-model.md, contracts/

**Tests**: Edge-case-focused per FR-009; test data (fake texts, expected chunks) used for assertions. No redundant coverage.

**Organization**: Tasks grouped by user story for independent implementation and testing.

## Format: `[ID] [P?] [Story] Description`

- **[P]**: Can run in parallel (different files, no dependencies on incomplete tasks)
- **[Story]**: US1, US2, US3 for user story phases
- Include exact file or project paths in descriptions

## Path Conventions

- Repository root: `RagDataTools.*` projects (Chunkers, Connectors, Connectors.Neo4j, Connectors.Qdrant, Di, UnitTests, Benchmarks)
- New: RagDataTools.Ingest, optional RagDataTools.KeywordExtraction
- Tests: RagDataTools.UnitTests/Connectors/, Ingest/, KeywordExtraction/, TestData/
- Docs: specs/PERFORMANCE.md at repo root

---

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Project and solution structure, baseline documentation, and package references.

- [x] T001 Ensure solution contains RagDataTools.Connectors.Qdrant and create RagDataTools.Ingest project per plan.md (repo root solution)
- [x] T002 Add or update specs/PERFORMANCE.md with baseline reference for ExtractSemanticChunksFromText, ExtractSemanticChunksDeeply, BuildRelationsGraph (FR-010)
- [x] T003 [P] Add Qdrant client NuGet package to RagDataTools.Connectors.Qdrant
- [x] T004 [P] Add PDF conversion package (e.g. PdfPig) per research.md to RagDataTools.Ingest or conversion project
- [x] T005 [P] Add Word conversion package (e.g. Open XML SDK / DocX) per research.md
- [x] T006 [P] Add HTML parsing and Markdown conversion packages (e.g. HtmlAgilityPack or AngleSharp) per research.md

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Core model and contract extensions that all user stories depend on.

**⚠️ CRITICAL**: No user story implementation can start until this phase is complete.

- [x] T007 Extend ChunkModel usage for page_number and keywords in Data per data-model.md in RagDataTools.Chunkers/Models/ChunkModel.cs (or document keys in XML/docs)
- [x] T008 Extend IChunksRepository (or add overload) with scope id and replace-by-scope in RagDataTools.Connectors/Interfaces/
- [x] T009 [P] Add FakeTextBuilder and expected-chunk helpers in RagDataTools.UnitTests/TestData/ per research.md §3 and plan TestData

**Checkpoint**: Foundation ready — US1, US2, US3 implementation can begin.

---

## Phase 3: User Story 1 — Persist chunks to storage via connectors (Priority: P1) — MVP

**Goal**: Caller can save chunks to a configured vector or graph store with a scope id; replace-by-scope; clear error on failure.

**Independent Test**: Configure connector and set of chunks; call save; verify chunks (and metadata) in store. Connector failure returns clear error.

### Implementation for User Story 1

- [x] T010 [P] [US1] Implement scope id and replace-by-scope in RagDataTools.Connectors.Neo4j/Repositories/Neo4jChunksRepository.cs; fix ChunkModel→Cypher mapping (parameterize, no string interpolation of labels)
- [x] T011 [P] [US1] Implement IChunksRepository in RagDataTools.Connectors.Qdrant/Repositories/ with scope id, order and heading metadata in payload
- [x] T012 [US1] Add SaveAsync(scopeId, chunks) extension or facade in RagDataTools.Connectors/ calling repository (contract persist-api.md)
- [x] T013 [US1] Register connectors and scope-based persist in RagDataTools.Di (if applicable)
- [x] T014 [P] [US1] Edge-case test: connector unavailable returns clear failure in RagDataTools.UnitTests/Connectors/
- [x] T015 [US1] Edge-case test: save chunks with order and heading hierarchy then verify in store (in-memory or test double) in RagDataTools.UnitTests/Connectors/

**Checkpoint**: User Story 1 independently testable — persist with scope id works; failures are clear.

---

## Phase 4: User Story 2 — Ingest PDF, Word, HTML as Markdown (Priority: P2)

**Goal**: Single document (PDF/Word/HTML) in → Markdown → chunked output with page numbers preserved for PDF/Word. Strict fail on malformed; empty list when zero chunks.

**Independent Test**: Provide PDF, Word, and HTML files; run ingest; verify Markdown-derived chunks and page_number for PDF/Word. Unsupported/malformed → clear error.

### Implementation for User Story 2

- [ ] T016 Create RagDataTools.Ingest project with references to RagDataTools.Chunkers and conversion packages (if not exists)
- [ ] T017 [P] [US2] Define IDocumentToMarkdownConverter in RagDataTools.Ingest/Conversion/ and implement PDF converter with page boundaries in RagDataTools.Ingest/Conversion/
- [ ] T018 [P] [US2] Implement Word-to-Markdown converter with page boundaries in RagDataTools.Ingest/Conversion/
- [ ] T019 [P] [US2] Implement HTML-to-Markdown converter in RagDataTools.Ingest/Conversion/
- [ ] T020 [US2] Implement document size validation and documented max per format (FR-011) in RagDataTools.Ingest
- [ ] T021 [US2] Implement ingest pipeline: validate → convert → call ExtractSemanticChunksDeeply → attach page_number to chunks in RagDataTools.Ingest/
- [ ] T022 [US2] Add IngestAsync(path or stream, format) API in RagDataTools.Ingest/Extensions/ or service (contract ingest-api.md)
- [ ] T023 [US2] Document max input size per format in XML or docs (FR-011)
- [ ] T024 [P] [US2] Edge-case test: valid document yielding zero chunks returns empty list in RagDataTools.UnitTests/Ingest/
- [ ] T025 [P] [US2] Edge-case test: malformed or unsupported file returns clear error (no partial result) in RagDataTools.UnitTests/Ingest/
- [ ] T026 [US2] Edge-case test: PDF/Word chunks carry page_number in metadata in RagDataTools.UnitTests/Ingest/

**Checkpoint**: User Story 2 independently testable — ingest → chunks with page numbers; edge cases covered.

---

## Phase 5: User Story 3 — Extract keywords from chunks (Priority: P3)

**Goal**: Optional keyword extraction from chunks via configurable backend; configurable timeout enforced; no retry; empty/minimal keywords when appropriate.

**Independent Test**: Chunks + configured backend → chunks with keywords; timeout or unavailable backend → clear indication.

### Implementation for User Story 3

- [ ] T027 Define IKeywordExtractor (or IKeywordExtractionBackend) with timeout in options in RagDataTools.KeywordExtraction/ or RagDataTools.Connectors (contract keyword-extraction-api.md)
- [ ] T028 [P] [US3] Implement one backend (e.g. Ollama) with timeout via CancellationToken in RagDataTools.KeywordExtraction/
- [ ] T029 [US3] Implement ExtractAsync(chunks, options) returning chunks with keywords in Data; enforce timeout (FR-007a)
- [ ] T030 [US3] Register keyword extractor (optional) in RagDataTools.Di
- [ ] T031 [P] [US3] Edge-case test: timeout enforced (no indefinite hang) in RagDataTools.UnitTests/KeywordExtraction/
- [ ] T032 [P] [US3] Edge-case test: backend unavailable or minimal text returns clear result (error or empty keywords) in RagDataTools.UnitTests/KeywordExtraction/

**Checkpoint**: User Story 3 independently testable — keyword extraction with timeout and edge-case behavior.

---

## Phase 6: Polish & Cross-Cutting Concerns

**Purpose**: Baseline doc, benchmarks for new hot paths, extractor order documentation, quickstart validation.

- [ ] T033 Ensure specs/PERFORMANCE.md documents baseline and is referenced by spec (FR-010, SC-006)
- [ ] T034 [P] Add IngestBenchmarks.cs for conversion + chunking hot path in RagDataTools.Benchmarks/ (FR-010)
- [ ] T035 Document extractor order (CodeBlock → UnusualBlock → HtmlTable → InfoBlock → ImageLink → ExternalLink → Heading) in RagDataTools.Chunkers code or docs per plan
- [ ] T036 Run quickstart.md validation (all code paths runnable)
- [ ] T037 Document in specs/PERFORMANCE.md how baseline satisfies constitution IV (comparison with comparable libraries including Python analogues): e.g. describe benchmark workload so it is comparable to Python chunking libs, or state "Python comparison: out of scope for v1; baseline documented for future comparison"
- [ ] T038 Add XML documentation comments to all new public APIs introduced by US1-US3 in RagDataTools.Connectors/, RagDataTools.Ingest/, and RagDataTools.KeywordExtraction/ (public methods, interfaces, and key models)

---

## Dependencies & Execution Order

### Phase Dependencies

- **Phase 1 (Setup)**: No dependencies — start immediately.
- **Phase 2 (Foundational)**: Depends on Phase 1 — blocks all user stories.
- **Phase 3 (US1)**: Depends on Phase 2 — MVP.
- **Phase 4 (US2)**: Depends on Phase 2; can start after Phase 2 (optionally after or in parallel with US1).
- **Phase 5 (US3)**: Depends on Phase 2; can start after Phase 2 (optionally in parallel with US1/US2).
- **Phase 6 (Polish)**: Depends on completion of desired user stories.

### User Story Dependencies

- **US1 (P1)**: No dependency on US2 or US3. Delivers persist with scope id and replace-by-scope.
- **US2 (P2)**: No dependency on US1 or US3 for core ingest; may use same connectors for E2E. Delivers document → chunks with page numbers.
- **US3 (P3)**: No dependency on US1 or US2 for extraction API; typically used with US1/US2 for full flow. Delivers keyword extraction with timeout.

### Parallel Opportunities

- Phase 1: T003–T006 [P] can run in parallel.
- Phase 2: T009 [P] can run in parallel with T007–T008 after T008 is done (T007/T008 may be quick).
- US1: T010, T011, T014 can run in parallel after T008; T015 after T012.
- US2: T017, T018, T019 [P]; T024, T025 [P] tests.
- US3: T028, T031, T032 [P].
- Phase 6: T034 [P] with T033, T035.

---

## Parallel Example: User Story 1

```text
# After Phase 2:
T010: Neo4j scope id + replace-by-scope + Cypher mapping
T011: Qdrant IChunksRepository with scope id and metadata
T014: Edge-case test connector failure

# After T010/T011:
T012: SaveAsync(scopeId, chunks) facade
T013: Di registration
T015: Save and verify order/headings test
```

---

## Implementation Strategy

### MVP First (User Story 1 Only)

1. Complete Phase 1: Setup  
2. Complete Phase 2: Foundational  
3. Complete Phase 3: User Story 1 (persist with scope id, Neo4j + Qdrant)  
4. **STOP and VALIDATE**: Run US1 independent test and edge-case tests  
5. Deploy or hand off for integration

### Incremental Delivery

1. Setup + Foundational → base ready  
2. Add US1 → validate persist → MVP  
3. Add US2 → validate ingest → document → chunks  
4. Add US3 → validate keyword extraction  
5. Polish (docs, benchmarks, quickstart)

### Parallel Team Strategy

- After Phase 2:  
  - Dev A: US1 (connectors)  
  - Dev B: US2 (ingest)  
  - Dev C: US3 (keyword extraction)  
- Stories integrate via ChunkModel and optional connectors/ingest flow.

---

## Notes

- [P] = different files or no dependency on same-phase incomplete tasks.
- [USn] maps to spec.md User Story n for traceability.
- Each user story is independently completable and testable per Independent Test in spec.
- FR-009: tests edge cases only; minimal set.
- Commit after each task or logical group; validate at checkpoints.
