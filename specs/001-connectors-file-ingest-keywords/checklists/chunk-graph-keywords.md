# Chunk Graph & Keywords Checklist: Connectors, File Ingest, and Keyword Extraction

**Purpose**: Validate that requirements for extracting chunks in graph form, keyword extraction from chunks, chunk chains, heading chains, and relations among tables, text chunks, images, headings, and other chunk types are complete, clear, and consistent.  
**Created**: 2025-03-14  
**Feature**: [spec.md](../spec.md)  
**Focus**: Graph representation of chunks, relationship types, chains (chunk/heading), keyword extraction from chunks.

**Note**: This checklist validates the quality of the *requirements* (completeness, clarity, measurability), not the implementation.

**Applied**: 2025-03-15 — spec § FR-002, Key Entities, User Stories; data-model.md; plan § Checklist application.

---

## Requirement Completeness

- [x] CHK001 Are requirements for representing chunks as a graph (nodes = chunks, edges = relationships) explicitly stated or clearly derivable from the spec? [Completeness, Spec §FR-001, §FR-002, Key Entities]
- [x] CHK002 Are all relationship types that link chunk types (e.g. text chunk ↔ table, text chunk ↔ image, text chunk ↔ heading, heading ↔ heading) enumerated or referenced in the spec so that “relations among tables, text chunks, images, headings” are bounded? [Completeness, Gap, Key Entities Chunk]
- [x] CHK003 Are “chunk chains” (e.g. sequential text chunks) and “heading chains” (e.g. topic hierarchy) explicitly distinguished in requirements, or is only generic “order and heading hierarchy” specified? [Completeness, Spec §FR-002]
- [x] CHK004 Are requirements for keyword extraction from chunks defined per chunk type (e.g. text chunks vs tables vs images) or only for “chunks” in general? [Completeness, Spec §FR-006, §FR-007]
- [x] CHK005 When persisting to a graph store, are requirements for storing relationships as first-class edges (vs metadata only) specified? [Completeness, Spec §FR-002, data-model.md]

## Requirement Clarity

- [x] CHK006 Is “chunk order” defined in measurable terms (e.g. sequence index, HasNextChunk relation) so that chunk chains can be restored unambiguously? [Clarity, Spec §FR-002]
- [x] CHK007 Is “heading hierarchy” defined with specific relationship types or levels (e.g. HasFirstSubtopic, HasNextTopic, heading level) so that heading chains are testable? [Clarity, Spec §FR-002]
- [x] CHK008 Are “keywords” from keyword extraction defined as a list of terms/phrases per chunk with clear storage location (e.g. Chunk.Data) so that persistence is unambiguous? [Clarity, Spec §FR-007, data-model §Keyword set]
- [x] CHK009 Is the scope of “relations among tables, text chunks, images, headings” bounded (e.g. only relations produced by the chunking pipeline) or could it imply additional user-defined relations? [Clarity, Gap]

## Requirement Consistency

- [x] CHK010 Do requirements for preserving “order and heading hierarchy” in vector stores (FR-002) align with requirements for graph stores (e.g. relationships as edges) without conflict? [Consistency, Spec §FR-001, §FR-002]
- [x] CHK011 Are keyword extraction requirements (FR-006, FR-007) consistent with chunk types that carry extractable text (e.g. TextChunk, Table, CodeBlock) vs non-text (e.g. ImageLink) where keywords might be from alternate text only? [Consistency, Spec §FR-006]

## Acceptance Criteria Quality

- [x] CHK012 Can “chunks and order/heading metadata are present in the store” (SC-001) be verified for both vector and graph stores using the same conceptual criteria? [Acceptance Criteria, Spec §SC-001]
- [x] CHK013 Are success criteria for “chunk graph” (nodes + relationships) and “keyword extraction” (keywords per chunk) independently testable? [Measurability, Spec §SC-001, §SC-003]

## Scenario Coverage

- [x] CHK014 Are requirements specified for the scenario where a document has only headings (no text chunks) or only tables/images — i.e. graph of chunks with no “chunk chain”? [Coverage, Edge Case]
- [x] CHK015 Are requirements for building the relationship graph (chunk → chunk relations) documented as part of chunking output, not only as persistence input? [Coverage, Spec §User Story 1 acceptance]
- [x] CHK016 Are keyword extraction requirements defined for the case where some chunks are skipped (e.g. non-text) or return empty keywords? [Coverage, Spec §FR-006, User Story 3]

## Edge Case Coverage

- [x] CHK017 Is the behavior defined when the relationship graph is empty (e.g. single chunk, or no relations between chunks)? [Edge Case, Gap]
- [x] CHK018 Are requirements for cycles or ambiguous ordering in heading chains (e.g. same-level headings) addressed or explicitly out of scope? [Edge Case, Gap]
- [x] CHK019 Are relations between chunks of the same type (e.g. table ↔ table, image ↔ image) in scope or excluded in the requirements? [Edge Case, Gap]

## Non-Functional Requirements

- [x] CHK020 Are performance or scalability requirements for building or persisting the chunk graph (e.g. number of nodes/edges) specified or deferred to the existing baseline? [Non-Functional, Spec §FR-010]
- [x] CHK021 Is the representation of the graph (in-memory vs serialized) or the format of relationships when persisting (e.g. metadata schema) specified enough to avoid ambiguous implementations? [Clarity, Spec §FR-002]

## Dependencies & Assumptions

- [x] CHK022 Is the assumption that the existing chunking pipeline produces both chunks and a relationship graph (e.g. BuildRelationsGraph) documented so that “extracting chunks in graph form” is traceable? [Assumption, plan.md]
- [x] CHK023 Are dependencies between “chunk graph” (from chunking) and “keyword extraction” (applied to chunks) clearly stated — e.g. keywords do not change graph structure? [Dependency, Spec §FR-007]

## Ambiguities & Conflicts

- [x] CHK024 Is “graph form” used consistently to mean nodes = chunks and edges = relationship types, or could it be interpreted as a different structure? [Ambiguity, Spec §FR-001, §FR-002]
- [x] CHK025 Do any requirements conflict between preserving “order” (sequence) and “heading hierarchy” (tree) when both apply to the same chunks? [Conflict, Spec §FR-002]

---

## Notes

- Check items off as completed: `[x]`
- Reference spec sections (e.g. §FR-002) when verifying; use [Gap] where requirements appear missing
- This checklist validates *requirements quality* for chunk graph, chains, relations, and keywords; it does not replace implementation or integration tests
