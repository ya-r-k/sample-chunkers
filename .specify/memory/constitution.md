<!--
Sync Impact Report
- Version change: (template/initial) → 1.0.0
- Modified principles: N/A (initial fill from template)
- Added sections: None
- Removed sections: None
- Templates: plan-template.md ✅ (Constitution Check references constitution); spec-template.md ✅ (no change); tasks-template.md ✅ (no change); command files ✅ (generic, no updates)
- Follow-up TODOs: None
-->

# RagDataTools Constitution

## Core Principles

### I. Class library for chunking large text

This project is a **C# class library** for chunking large text data. The solution MUST contain: (1) core library logic (chunking, relationship graph, optional persistence abstractions), (2) unit tests, and (3) benchmarks. There is no web API or host; the deliverable is a consumable library. New features MUST be exposed as library API (e.g. extension methods, interfaces) and MUST NOT introduce an HTTP or process boundary unless the constitution is amended.

**Rationale**: The product is a reusable RAG data-preparation library; keeping it library-only preserves a single responsibility and allows callers to integrate as needed.

### II. Regex-based chunk identification

Chunk boundaries and structured elements (headings, code blocks, tables, links, etc.) MUST be identified via **regex expressions** (or equivalent pattern-based extraction) as the primary mechanism. Regex providers and extractors are first-class components; new chunk types or boundaries MUST have defined patterns and SHOULD be covered by tests. Performance-critical regex paths SHOULD be benchmarked.

**Rationale**: Explicit patterns keep behavior predictable and testable; the codebase already centers on regex providers and Strategy/Chain-of-Responsibility extractors.

### III. Tests and benchmarks (non-negotiable)

The solution MUST contain both **unit tests** and **benchmarks**.

- **Unit tests**: All public API and critical chunking/relationship logic MUST have unit tests. Tests MUST be runnable via `dotnet test` and SHOULD use the existing stack (NUnit, FluentAssertions, coverlet). New behavior MUST not ship without corresponding tests.
- **Benchmarks**: Performance-critical code paths (e.g. semantic chunking, relation graph building, regex-heavy extractors) MUST have BenchmarkDotNet benchmarks. The benchmark project MUST remain runnable and results SHOULD be documented (e.g. in docs/PERFORMANCE.md).

**Rationale**: Tests ensure correctness; benchmarks enforce and document the performance goals stated in Principle IV.

### IV. Performance over comparable libraries

The library MUST aim to be **more performant** than comparable libraries for the same workload, **including Python analogues**. This implies:

- Benchmarks MUST be maintained for key operations (e.g. ExtractSemanticChunksFromText, ExtractSemanticChunksDeeply, BuildRelationsGraph).
- Regressions in benchmark results MUST be justified or fixed before merge.
- When adding features, performance impact MUST be considered; hot paths SHOULD avoid unnecessary allocations and repeated regex work.

**Rationale**: User requirement and project.md both state that outperforming other libraries (including Python) is important; this principle makes that a governance requirement.

### V. Layered design and clarity

Architecture MUST keep a clear separation: **Chunkers** = domain and application (extension methods as main API); **Connectors** = infrastructure (repository interfaces and implementations). The public API SHOULD remain extension-method–centric for discoverability. Complexity MUST be justified; YAGNI applies—no organizational-only projects or unused abstractions.

**Rationale**: Aligns with current codebase (project.md) and keeps the library maintainable and consumable.

## Performance standards and technology

- **Language and runtime**: C#, .NET (current target net10.0). New code MUST comply with the solution’s target framework and nullable/ImplicitUsings settings.
- **Chunking mechanism**: Regex-based extraction is the standard; any alternative (e.g. full parser) MUST be justified and SHOULD not regress benchmarked performance.
- **Benchmarking**: BenchmarkDotNet is the standard. Benchmark results SHOULD be documented and compared where relevant (e.g. vs Python analogues) in docs.
- **Dependencies**: New library dependencies MUST be justified; avoid unnecessary transitive cost for core chunking paths.

## Development workflow and quality gates

- **Before merge**: All constitution principles MUST be satisfied; `dotnet build` and `dotnet test` MUST pass. Changes that touch chunking or relationship logic SHOULD include or update benchmarks.
- **Documentation**: Public API (extension methods, key types) MUST have XML documentation; breaking changes or new capabilities SHOULD be reflected in docs/ (e.g. API.md, ARCHITECTURE.md).
- **Code review**: Reviews MUST verify compliance with Principles I–V and that performance-sensitive changes have benchmark coverage or justification.

## Governance

- This constitution is the source of truth for project governance. Conflicting practices MUST be resolved in favor of the constitution unless it is amended.
- **Amendments**: Any change to principles or governance MUST be documented in this file, with version and date updated. Semantic versioning applies: MAJOR = backward-incompatible principle removals/redefinitions; MINOR = new principle or material expansion; PATCH = clarifications, typos, non-semantic edits.
- **Compliance**: All PRs and reviews MUST verify that changes comply with the core principles. Violations MUST be justified in writing (e.g. in plan’s Complexity Tracking) or fixed before merge.

**Version**: 1.0.0 | **Ratified**: 2025-03-14 | **Last Amended**: 2025-03-14
