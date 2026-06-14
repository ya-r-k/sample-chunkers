# Project deep-dive: RagDataTools

## Solution overview

RagDataTools is a .NET 10 class-library suite for preparing RAG input data: it chunks text into semantic units, extracts Markdown/HTML structure into typed chunks, builds relationship graphs between chunks, and defines persistence abstractions for downstream stores such as Neo4j. It is meant to be consumed by other applications or services rather than hosting its own UI or HTTP API.

The current codebase is centered on `RagDataTools.Chunkers`, which owns the chunking pipeline and the public extension-method API. `RagDataTools.Connectors` defines repository contracts, `RagDataTools.Connectors.Neo4j` contains the only real storage implementation, `RagDataTools.Connectors.Qdrant` is still a stub, and `RagDataTools.UnitTests` plus `RagDataTools.Benchmarks` exercise the core parsing and relation-building logic.

## Project structure

The repository does not physically use `src/` and `tests/` folders even though the solution groups projects that way. The real layout is flat at the repo root, with a separate `specs/` tree for design and planning docs.

```text
rag-data-tools/
├── RagDataTools.sln
├── RagDataTools.Chunkers/
│   ├── Extensions/
│   ├── Infrastructure/
│   ├── Interfaces/
│   ├── Models/
│   └── Strategies/
├── RagDataTools.Connectors/
│   └── Interfaces/
├── RagDataTools.Connectors.Neo4j/
│   └── Repositories/
├── RagDataTools.Connectors.Qdrant/
│   └── Repositories/
├── RagDataTools.Di/
├── RagDataTools.Unifiers/
├── RagDataTools.UnitTests/
│   └── Chunkers/
├── RagDataTools.Benchmarks/
└── specs/
```

This structure is broadly sound for a library suite because it separates chunking, abstractions, connectors, tests, and benchmarks. The main problem is that the solution and the docs imply a fuller `src/` / `tests/` / `docs/` organization than actually exists. That makes the repo look more mature than it is and increases the chance of stale documentation.

The placeholder projects are also part of the structure story: `RagDataTools.Di`, `RagDataTools.Unifiers`, and `RagDataTools.Connectors.Qdrant` exist, but they are effectively empty. That is acceptable during early design work, but it should be called out clearly until the projects gain real responsibility.

## Technology stack

- Runtime: .NET 10 (`net10.0`) across all projects.
- Language features: nullable reference types and implicit usings are enabled everywhere; `LangVersion` is set to `latest` in the test and benchmark projects.
- Markdown / parsing: `Markdig` is referenced in `RagDataTools.Chunkers` and also in `RagDataTools.Connectors.Neo4j`, but the current source shown here does not appear to use Markdig APIs directly yet.
- Graph DB: `Neo4j.Driver` 6.0.0 is used by `RagDataTools.Connectors.Neo4j`.
- Testing: NUnit 4.4.0, FluentAssertions 8.8.0, coverlet.collector 6.0.4, Microsoft.NET.Test.Sdk 18.0.1, NUnit.Analyzers 4.11.2, and NUnit3TestAdapter 6.1.0.
- Benchmarking: BenchmarkDotNet 0.15.8 in `RagDataTools.Benchmarks`.

There is no repo-wide `.editorconfig`, `Directory.Build.props`, `global.json`, or NuGet config. That is not fatal, but it does mean build and style conventions are mostly local to each project file.

## Public APIs and persistence

The real public surface is extension-method driven. The main entry points are in `RagDataTools.Chunkers.Extensions` and the simple extractor factories live in `RagDataTools.Chunkers.Infrastructure`.

```csharp
public static string[] ExtractSemanticChunksFromText(
    this string text,
    int chunkWordsCount,
    IPrimitivesIndexesExtractor indexesExtractor,
    double overlapPercentage = 0.0)

public static ChunkModel[] ExtractSemanticChunksDeeply(
    this string text,
    int chunkWordsCount,
    IPrimitivesIndexesExtractor indexesExtractor,
    double overlapPercentage = 0.0,
    int lastUsedIndex = 0)

public static RelationshipModel[] BuildRelationsGraph(this ChunkModel[] chunks)
public static Dictionary<int, int> FindRepeatedChunksWithUrls<T>(this Dictionary<T, ChunkModel[]> chunks)
```

`ChunkModel` carries the chunk index, `ChunkType`, raw content, a loosely typed `Data` dictionary, and `RelatedChunksIndexes`. `RelationshipModel` is the graph edge record. The models are convenient for consumers, but the `Dictionary<string, object>` payload is intentionally weakly typed and will need discipline from callers.

The deep extraction flow is the heart of the library. `ComplexDataChunkerExtensions` builds a Markdown extractor chain, replaces structured content with placeholders, preprocesses the remaining text, chunks the text by sentence or paragraph boundaries, and then restores relationships back to the structured chunks. The extractor order is fixed: code blocks, unusual code blocks, HTML tables, info blocks, image links, external links, and headings.

The collection-oriented overload of `ExtractSemanticChunksDeeply` accepts `Dictionary<T, string>` with `where T : unmanaged`, so the caller controls document IDs while the library keeps chunk indexes unique across the collection.

On the persistence side, `RagDataTools.Connectors` defines the contracts:

- `IChunksRepository<TFlag, TId>` with `AddAsync`, `GetIndexesIdsPairsByFlagAsync`, and `RemoveFlagFromAllDataAsync`
- `IChunksRelationshipsRepository<TData>` with `AddRelationshipsAsync`

`RagDataTools.Connectors.Neo4j.Neo4jChunksRepository` is the only implementation present. It is still incomplete and currently looks more like scaffolding than a finished persistence adapter. The implementation hardcodes the `neo4j` database name, interpolates labels directly into Cypher, and uses `ChunkModel` as if it had `type` and `properties` members. Its read path also expects a `temporary_index` property that nothing in the current code writes. In other words, the repository contract exists, but the mapping and query behavior are not production-ready yet.

`RagDataTools.Connectors.Qdrant.QdrantChunksRepository` is an empty class and does not implement the connector interface yet.

## Patterns and architecture

The codebase uses a few clear patterns that fit the problem well:

- Extension-method API. This is the dominant public style and makes the library easy to consume from plain text and chunk collections.
- Strategy. Sentence, paragraph, and word boundary extraction are split into separate index extractors behind `IPrimitivesIndexesExtractor`.
- Chain of Responsibility. Markdown structure extraction is a staged pipeline of extractor classes that mutate a shared `StringBuilder` and pass the result forward.
- Repository abstraction. Storage concerns are separated from chunking concerns through generic repository interfaces.

Those patterns are appropriate for a library of this shape, but there are some weak spots.

The extension-method layer is good for discoverability, but the public API surface is slightly misleading because the docs describe methods that are not actually present anymore, such as a separate `RetrieveChunksFromText` entry point and a nested document/chunk-type return shape. The code itself currently exposes a smaller, flatter API.

The Markdown extractor chain is a reasonable design choice, but it is quite stateful. Each extractor mutates the same `StringBuilder`, and chunk relationships are inferred from placeholder text. That works, but it makes correctness sensitive to replacement order, placeholder syntax, and regex behavior.

The repository abstraction is sound in concept, but the current Neo4j adapter shows the usual risk of over-abstracting before the first implementation is complete. The interface is cleaner than the implementation beneath it.

## Testing strategy

`RagDataTools.UnitTests` is a focused NUnit test project with FluentAssertions and coverlet enabled. It references `RagDataTools.Chunkers` and uses `InternalsVisibleTo` so the tests can validate internal behavior where needed.

Coverage is strongest around the core chunking pipeline:

- `SimpleTextChunkerExtensionsTests` covers preprocessing and semantic chunk splitting for sentence and paragraph modes, including overlap behavior.
- `ComplexDataChunkerExtensionsTests` covers mixed Markdown content, nested content, collection processing, and the extraction of code blocks, tables, links, images, headings, and info blocks.
- `ChunksExtensionsTests` covers relation graph construction and URL duplicate detection.
- Separate tests cover word, sentence, and paragraph index extractors.

The test data strategy is fixture-heavy and realistic. The `TestData` classes hold large in-code samples from real articles and expected chunk/relationship outputs. That gives good signal for behavior regressions, but it also makes the tests long and somewhat brittle when extractor behavior changes in small ways.

The integration gaps are obvious: there are no tests for Neo4j persistence, no Qdrant tests, no DI registration tests, and no CI gate visible in the repository. The benchmark and connector layers therefore remain mostly unverified from an integration perspective.

## Performance and benchmarks

`RagDataTools.Benchmarks` is a BenchmarkDotNet console app that exercises the hot paths in the chunking layer. `Program.cs` uses `BenchmarkSwitcher.FromAssembly(...).Run(args, new DebugInProcessConfig())`, so it is convenient for local runs but not ideal for representative benchmark discipline.

The benchmark class covers:

- preprocessing
- word, sentence, and paragraph index extraction
- semantic chunking for plain text and Markdown
- relation graph building
- repeated URL detection

The benchmark data is generated in memory at several sizes, including small, medium, large, very large, and complex Markdown corpora. That is a good fit for a parser-heavy library because it exposes allocation and regex costs without depending on external files.

The project docs report broadly healthy results for the current baseline, with chunking in the microsecond-to-low-millisecond range for the sample workloads. The main hot spots are the expected ones: repeated regex passes, `StringBuilder.Replace`, and LINQ-heavy duplicate detection.

One benchmark smell is the runtime mismatch: the benchmark project targets `net10.0`, but the job attribute is pinned to `RuntimeMoniker.Net90`. That does not break the code, but it makes the benchmark config look stale relative to the rest of the solution.

## Documentation and discoverability

Documentation is present, but it is not well synchronized with the code.

The biggest mismatch is that `README.md` still speaks in the old `Sample.Chunkers` naming, says the library uses .NET 9.0, and links to a `docs/` folder that does not exist in the repository. The real long-form docs are in `specs/`, which are a mix of architecture notes, performance notes, and future feature specifications.

`project.md` should therefore be treated as the current deep-dive source of truth, while `README.md` is closer to stale onboarding material. Several spec documents also describe planned ingest and keyword-extraction features that are not implemented yet, so they should be read as roadmap material rather than current behavior.

## Code quality and maintainability

The code is reasonably modern C# for a library: nullable is on, implicit usings are on, and the public API is typed. The tests also use analyzers and coverage tooling, which is a good sign.

The main maintainability concern is that the models are mutable records with loosely typed metadata. That keeps the API flexible, but it pushes a lot of correctness onto convention instead of type safety.

Contributor complexity is moderate. The public API is easy to understand, but the internals are regex-heavy, order-dependent, and sensitive to placeholder replacement details. A junior developer can follow the tests and extension methods, but the Markdown extraction chain and Neo4j adapter need more care.

## Strengths

- Clear separation between chunking logic, connector abstractions, connector implementations, tests, and benchmarks.
- Strong focus on typed domain objects for chunks and relationships.
- Realistic, high-signal tests that exercise the full extraction pipeline with representative content.
- Benchmark coverage for the main hot paths.
- Extension-method API is ergonomic for downstream consumers.
- Nullable and implicit usings are consistently enabled.

## Weak points & risks

- `RagDataTools.Connectors.Neo4j` is incomplete: the current query mapping does not match `ChunkModel`, `temporary_index` is not written anywhere, and label interpolation is unsafe if inputs are not trusted.
- `RagDataTools.Connectors.Qdrant`, `RagDataTools.Di`, and `RagDataTools.Unifiers` are effectively placeholders, which makes the solution look broader than its actual implementation.
- Public docs are stale and mismatched with the codebase naming, folder layout, and even some API shapes.
- The deep extraction pipeline is order-sensitive and string-replacement driven, so small regex changes can have wide ripple effects.
- `ChunkModel.Data` is a weakly typed dictionary, which is flexible but easy to misuse.
- There are no connector integration tests and no visible CI workflow in the repository.
- The benchmark job config is not aligned with the current target framework.
- `Markdig` is referenced, but the current code shown here does not use it directly, which suggests either dead dependency weight or unfinished parser integration.

## Recommendations

1. Finish or simplify the Neo4j adapter first. Map `ChunkModel` to an explicit persistence payload, stop interpolating labels directly into Cypher, and make the `temporary_index` story real or remove it.
2. Either implement the placeholder projects or mark them explicitly as WIP. Empty projects make the solution harder to trust.
3. Bring `README.md` and the spec docs back in sync with the current API, project names, and actual folder layout.
4. Add connector integration tests, starting with Neo4j behavior and failure paths, so persistence is covered by something more than the interface shape.
5. Add a repo-level `.editorconfig` and build-time analyzers so formatting and naming rules are consistent across projects.
6. Align the benchmark job/runtime settings with `net10.0`, then keep the current benchmark baseline in sync with performance docs.
7. Tighten the public metadata model over time. Even partial typed wrappers for `Data` would reduce risk without removing flexibility.
8. Decide whether `Markdig` is a real dependency in the current codepath. If it is, use it explicitly; if not, remove the unused reference.

