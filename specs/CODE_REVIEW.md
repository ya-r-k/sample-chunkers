# Code and test notes

## Overview

This document reflects the current RagDataTools state: unit tests mostly cover `RagDataTools.Chunkers`, while the biggest risks are in the Neo4j repository, input validation, and integration coverage.

---

## Test coverage

### Covered well

- `SimpleTextChunkerExtensions`
- `ComplexDataChunkerExtensions`
- `ChunksExtensions`
- chunk extraction, relation graph building, duplicate URL detection

### Weak or missing coverage

- argument edge cases
- empty or invalid Markdown / HTML
- large documents and multi-document scenarios
- `MathBlock`, which is declared but unused
- integration tests for `RagDataTools.Connectors.Neo4j`
- future coverage for `RagDataTools.Connectors.Qdrant`
- DI and end-to-end flow tests

---

## Code issues

### 1. `Neo4jChunksRepository.AddAsync` needs work

The current implementation interpolates labels into Cypher and the `ChunkModel` to `item.type` / `item.properties` mapping is incomplete. This is both a correctness risk and a potential Cypher injection risk if flags become external.

### 2. No explicit argument validation

Public chunking methods do not validate all inputs.

### 3. `MathBlock` is declared but not used

Either implement it or remove it.

### 4. Integration coverage is missing

The tests focus on chunking and do not cover the connector projects.

### 5. XML docs are incomplete

Not all public methods have consistent XML comments and examples.

---

## Test recommendations

1. Add tests for `Neo4jChunksRepository`
2. Add argument validation tests
3. Add empty and minimal input tests
4. Add flag-combination tests
5. Add invalid Markdown / HTML tests
6. Add performance checks for large texts
7. Cover all relation types

---

## Architecture notes

### Strengths
- Clear separation between Chunkers, Connectors, Neo4j, and tests
- Extension-method API is easy to use
- `GeneratedRegex` and `BenchmarkDotNet` are used appropriately

### Improvements
- Input validation
- Connector integration tests
- Finish WIP projects: `RagDataTools.Di`, `RagDataTools.Unifiers`, and `RagDataTools.Connectors.Qdrant`
- More consistent XML documentation

---

## Summary

- Core chunking functionality is well covered by unit tests
- The biggest risk is currently in the Neo4j layer
- The next useful step is integration testing plus input validation
