# Chunking Logic: Weaknesses, Bottlenecks & Best Practices — Requirements Quality Checklist

**Purpose**: Validate that requirements address known weaknesses and bottlenecks in the implemented chunking logic and that chunking requirements align with best practices from other libraries (including Python ecosystems such as LangChain, LlamaIndex).  
**Created**: 2025-03-15  
**Feature**: [spec.md](../spec.md)  
**Context**: Implemented logic (ExtractSemanticChunksDeeply, ExtractSemanticChunksFromText, chain of Markdown extractors, BuildRelationsGraph); FR-010 (no regression), FR-009 (edge cases); constitution (benchmarks, performance vs peers).

**Note**: This checklist validates the quality of the *requirements* (completeness, clarity, measurability) for chunking behavior and constraints — not the implementation itself.

**Applied**: 2025-03-15 — spec § Chunking strategy (word-first, token optional/comparison, overlap, semantic boundaries, extractor order); plan § Chunking strategy, Refactoring (no static extensions, preserve API/perf), Checklist application.

---

## Requirement Completeness (Chunking Weaknesses & Bottlenecks)

- [x] CHK001 Are requirements or constraints defined for **chunking memory behavior** (e.g. full-document-in-memory vs streaming, or documented max input size as the memory bound) so that implementers and reviewers know the intended scalability? [Completeness, Spec §FR-011, plan.md, Gap]
- [x] CHK002 Are requirements for **chunk size units** (word count vs character count vs token count) explicitly stated, and is alignment with embedding or retrieval models (e.g. token limits) documented or out of scope? [Completeness, Gap, Spec §Key Entities Chunk]
- [x] CHK003 Is **overlap** between consecutive chunks (percentage, semantics, and effect on boundary placement) specified in the spec or linked docs so that “overlap” is a testable requirement? [Completeness, Gap]
- [x] CHK004 Are requirements for **semantic boundary types** (e.g. sentence, paragraph only vs list/code-block boundaries) documented so that chunking respects intended boundaries and edge cases are bounded? [Completeness, Spec §FR-009, Gap]
- [x] CHK005 Is the **order or priority of chunk extractors** (e.g. code block → table → heading) required to be documented or stable so that overlapping patterns have defined behavior? [Completeness, plan.md, Gap]
- [ ] CHK006 Are **encoding and script requirements** for chunking (e.g. Unicode normalization, non-Latin scripts, grapheme clusters) specified so that “special characters or non-Latin scripts” (Edge Cases) is unambiguous? [Completeness, Spec §Edge Cases, Gap]
- [x] CHK007 Are requirements for **chunk count or size bounds** (e.g. max chunks per document, min/max chunk size in chosen units) stated or explicitly out of scope? [Completeness, Gap]

## Requirement Completeness (Best Practices from Other Libraries)

- [x] CHK008 Are requirements for **token-based chunk sizing** (e.g. 400–512 tokens as in common Python RAG guidance) or explicit **word/token equivalence** documented, or is word-only sizing intentionally chosen and justified? [Completeness, Best Practice, Gap]
- [x] CHK009 Is **overlap** quantified and justified (e.g. 10–20% overlap to reduce boundary loss) in requirements or linked research so that it matches common best practices where applicable? [Completeness, Best Practice, Gap]
- [x] CHK010 Are requirements for **recursive or hierarchical boundary splitting** (e.g. try paragraph then sentence then word) documented or explicitly out of scope compared to single-level boundaries? [Completeness, Best Practice, Gap]
- [ ] CHK011 Is **comparison or alignment with named chunking libraries** (e.g. LangChain text splitters, LlamaIndex node parsers) required in governance or spec so that “best practices from other libraries” is verifiable? [Completeness, constitution §IV, Gap]
- [x] CHK012 Are **semantic-boundary preservation** requirements (e.g. do not split mid-sentence/mid-paragraph) explicitly stated so that they match common “semantic chunking” expectations? [Completeness, Spec §FR-009, Best Practice]

## Requirement Clarity (Weaknesses & Bottlenecks)

- [x] CHK013 Is “chunking MUST NOT regress” (FR-010) tied to **specific operations** (e.g. ExtractSemanticChunksFromText, ExtractSemanticChunksDeeply, BuildRelationsGraph) and **workloads** so that bottlenecks in regex, allocations, or indexing are in scope? [Clarity, Spec §FR-010, §SC-006]
- [x] CHK014 Are **performance-sensitive operations** in chunking (e.g. repeated regex passes, full-text scans, per-chunk allocations) required to be documented or bounded so that no-regression is interpretable? [Clarity, Spec §FR-010, Gap]
- [x] CHK015 Is “maximum … words in a text chunk” (or equivalent) defined with **measurable rules** (e.g. word boundaries, handling of placeholders) so that chunk size is unambiguous? [Clarity, plan.md, Gap]
- [x] CHK016 When multiple **structured elements** (code, table, heading) could overlap in the source, are requirements for which element “wins” or for deterministic ordering stated? [Clarity, Gap]

## Requirement Clarity (Best Practices)

- [x] CHK017 Is “semantic unit” (sentence vs paragraph) for chunk boundaries **enumerated and chosen per use case** in requirements or linked docs so that it aligns with common recursive/semantic splitting practices? [Clarity, Best Practice, Gap]
- [ ] CHK018 Are **chunk quality criteria** (e.g. “self-contained,” “single coherent question”) referenced or out of scope so that requirements are consistent with best-practice descriptions of ideal chunk size? [Clarity, Best Practice, Gap]

## Requirement Consistency

- [x] CHK019 Do requirements for **chunk size** (words), **overlap**, and **max input size per format** (FR-011) align without conflict (e.g. no implied chunk count that exceeds reasonable memory)? [Consistency, Spec §FR-011, §FR-010]
- [x] CHK020 Are **edge-case chunking behaviors** (empty input, zero chunks, very large document) consistent between spec Edge Cases, FR-009, and FR-011? [Consistency, Spec §Edge Cases, §FR-009, §FR-011]
- [x] CHK021 Do requirements for **preserving order and heading hierarchy** (FR-002) align with how chunking produces ordered chunks and relationship types (e.g. HasNextChunk, heading relations)? [Consistency, Spec §FR-002, plan.md]

## Acceptance Criteria Quality

- [x] CHK022 Can “no regression” for chunking (SC-006, FR-010) be verified **per workload type** (e.g. plain text, Markdown with tables/code/headings) using the documented baseline? [Measurability, Spec §SC-006, §FR-010]
- [x] CHK023 Are acceptance criteria for **chunk output shape** (count, types, relationships) defined or referenced (e.g. test data, expected chunks) so that correctness of chunking logic is testable? [Measurability, Spec §User Stories, plan.md TestData]
- [x] CHK024 Can **best-practice alignment** (e.g. overlap, semantic boundaries) be objectively checked if the spec or project docs reference target practices or comparison workloads? [Measurability, Gap]

## Scenario Coverage

- [x] CHK025 Are requirements specified for **chunking when conversion output is degenerate** (e.g. empty Markdown, only placeholders, no sentences) so that edge cases are covered? [Coverage, Spec §Edge Cases, Gap]
- [x] CHK026 Are requirements for **relationship graph construction** (BuildRelationsGraph) from chunks in scope for chunking logic quality (e.g. no duplicate edges, correct relationship types)? [Coverage, Spec §FR-002, plan.md]
- [x] CHK027 Is **large-document chunking** (at or near documented max size) explicitly in scope for performance and correctness requirements, or only rejection at ingest? [Coverage, Spec §FR-011, §FR-010]

## Edge Case Coverage

- [x] CHK028 Are requirements defined for **chunking when semantic boundaries are missing or ambiguous** (e.g. no sentence boundaries, very long single “word”) so that behavior is bounded? [Edge Case, Gap]
- [x] CHK029 Is **overlap** behavior at document start/end or with very few semantic units specified so that edge cases are not undefined? [Edge Case, Gap]
- [x] CHK030 Are **non-Latin and multi-script** chunking requirements (word/sentence boundaries) specified so that “keywords and persistence work without corruption” (Edge Cases) is testable for chunking output? [Edge Case, Spec §Edge Cases]

## Non-Functional Requirements (Chunking)

- [x] CHK031 Are **allocation or complexity expectations** for chunking (e.g. avoid unnecessary per-chunk string copies, regex match reuse) stated in non-functional requirements or linked performance doc? [Non-Functional, Spec §FR-010, Gap]
- [x] CHK032 Is **determinism** of chunk output (same input → same chunks) required or assumed, and is it stated so that benchmarks and tests are reproducible? [Non-Functional, plan.md TestData, Gap]

## Dependencies & Assumptions

- [x] CHK033 Is the assumption that **chunking is word-based (and optionally overlap)** and not token-based documented so that integration with token-limited embeddings is a known limitation or future work? [Assumption, Gap]
- [x] CHK034 Are dependencies on **Markdown structure** (headings, code fences, tables) for chunking explicitly stated so that non-Markdown or malformed Markdown behavior is bounded? [Dependency, Spec §FR-003, §FR-008]

## Ambiguities & Conflicts

- [x] CHK035 Is “existing chunking” or “existing Chunkers logic” used consistently to mean the current ExtractSemanticChunksDeeply pipeline (extractors → placeholders → text chunks → relations) so that no-regression scope is clear? [Ambiguity, Spec §FR-010, plan.md]
- [x] CHK036 Is there a conflict between **minimal tests** (FR-009) and the need to validate chunking logic against **multiple workloads and best practices** (e.g. overlap, boundaries)? [Conflict, Spec §FR-009, Best Practice]

---

## Notes

- Check items off as completed: `[x]`
- Reference spec sections and plan.md when verifying; use [Gap] where requirements for chunking weaknesses, bottlenecks, or best-practice alignment appear missing
- This checklist validates *requirements quality* for chunking logic; it does not replace implementation review or benchmarking
- For implemented logic context: see RagDataTools.Chunkers (ComplexDataChunkerExtensions, NaiveTextChunkerExtensions, ChunksExtensions, Markdown extractors)
