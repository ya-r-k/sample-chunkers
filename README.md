# RagDataTools

RagDataTools is a .NET 10 class-library suite for RAG data preparation: it chunks text, extracts structured Markdown/HTML content, builds relationships between chunks, and provides persistence abstractions for storage backends.

## What it does

- Semantic text chunking by sentence or paragraph boundaries.
- Deep extraction of Markdown/HTML structures such as headings, code blocks, tables, links, and images.
- Chunk relationship graph building and duplicate URL detection.
- Repository abstractions for persistence, with a Neo4j connector and a Qdrant placeholder.
- NUnit-based unit tests and BenchmarkDotNet performance benchmarks.

## Requirements

- .NET SDK 10.0
- Optional for connector work: a running Neo4j instance for `RagDataTools.Connectors.Neo4j`

## Getting started

```bash
dotnet restore RagDataTools.sln
dotnet build RagDataTools.sln
dotnet test RagDataTools.sln
```

For a quick usage example, see the chunking APIs in `RagDataTools.Chunkers` and the deeper overview in [`project.md`](project.md).

## Run scripts / commands

- Restore: `dotnet restore RagDataTools.sln`
- Build: `dotnet build RagDataTools.sln`
- Test: `dotnet test RagDataTools.sln`
- Benchmarks: `dotnet run --project RagDataTools.Benchmarks/RagDataTools.Benchmarks.csproj -c Release`

## Optional: external services

- `RagDataTools.Connectors.Neo4j` depends on `Neo4j.Driver` and expects a reachable Neo4j database for real persistence scenarios.
- `RagDataTools.Connectors.Qdrant` is currently a placeholder and does not provide a working connector yet.

## Project layout

The repo is organized as a flat solution root with separate projects:

- `RagDataTools.Chunkers/` - chunking and relationship-building logic
- `RagDataTools.Connectors/` - repository interfaces
- `RagDataTools.Connectors.Neo4j/` - Neo4j connector implementation
- `RagDataTools.Connectors.Qdrant/` - Qdrant placeholder
- `RagDataTools.Di/` - DI placeholder
- `RagDataTools.Unifiers/` - unifier placeholder
- `RagDataTools.UnitTests/` - NUnit tests
- `RagDataTools.Benchmarks/` - BenchmarkDotNet runner
- `specs/` - architecture, API, performance, and feature-spec documents

## Documentation & help

- [`project.md`](project.md) - detailed technical overview, risks, and recommendations
- [`specs/ARCHITECTURE.md`](specs/ARCHITECTURE.md)
- [`specs/API.md`](specs/API.md)
- [`specs/PERFORMANCE.md`](specs/PERFORMANCE.md)
- [`specs/EXAMPLES.md`](specs/EXAMPLES.md)

## Contributing

Keep changes focused, update tests when behavior changes, and prefer small, reviewable pull requests. If you touch chunking, persistence, or benchmarks, update the relevant `specs/` docs and `project.md` as needed.

## License

License terms are TBD. Add a `LICENSE` file when the project license is finalized.
