# Performance analysis

## Overview

This document analyzes the performance of the RagDataTools chunking library, identifies hot spots, and lists optimization ideas. It also compares the library with popular Python chunking libraries.

---

## Measurement setup

### Tools

Performance is measured with:
- BenchmarkDotNet
- the `RagDataTools.Benchmarks` project
- `dotnet run --project RagDataTools.Benchmarks --configuration Release`

### Test data

Benchmarks use texts of different sizes:
- Small: about 100 words
- Medium: about 1,000 words
- Large: about 10,000 words
- Very large: about 100,000 words

They also cover different content types:
- plain text
- Markdown with headings
- complex Markdown with code, tables, links, and images

---

## Benchmark results

> Note: all results were collected on an AMD Ryzen 7 4800H with .NET 10.0 in Release mode.

### SimpleTextChunkerExtensions

#### ExtractSemanticChunksFromText

**Method:** splits text into semantic chunks while respecting sentence or paragraph boundaries.

**Observed performance:**
- Small (100 words): 16.74 us
- Medium (1K words, sentence mode): 153.91 us
- Large (10K words, sentence mode): 2.01 ms
- Very large (100K words): 64.58 ms

**Memory usage:**
- Small: 17 KB
- Medium: 136 KB
- Large: 1.3 MB
- Very large: 14 MB

**Analysis:** Performance is excellent for small and medium inputs. Very large inputs are still reasonable for this workload.

**Hot spots:**
1. sentence-boundary regex
2. repeated `Split` calls
3. LINQ operations for overlap calculations

#### PreprocessNaturalTextForChunking

**Observed performance:**
- Small (100 words): 219.1 ns
- Medium (1K words): 1.30 us
- Large (10K words): 14.57 us

**Memory usage:** 0 B

**Analysis:** The method is very efficient and allocates no memory in the benchmark.

**Hot spots:**
1. chained `Replace` calls
2. regex for repeated spaces

#### GetWords

**Observed performance:**
- Small (100 words): 2.06 us
- Large (10K words): 316.80 us

**Memory usage:**
- Small: 4 KB
- Large: 406 KB

**Analysis:** The method is efficient and uses `Span<string>` to reduce allocations.

---

### ComplexDataChunkerExtensions

#### ExtractSemanticChunksDeeply

**Observed performance:**
- Plain text (1K words): 210.96 us
- Simple Markdown (1K words): 265.57 us
- Complex Markdown (5K words): 1.71 ms

**Memory usage:**
- Plain text: 259 KB
- Simple Markdown: 394 KB
- Complex Markdown: 1.99 MB

**Analysis:** Overall performance is very good. Memory use grows with document complexity, which is expected.

**Hot spots:**
1. multiple regex passes
2. repeated `StringBuilder.Replace`
3. sequential processing
4. object creation for each chunk

#### RetrieveChunksFromText

**Observed performance:**
- Simple Markdown (1K words): 19.95 us
- Complex Markdown (5K words): 181.39 us

**Memory usage:**
- Simple Markdown: 98 KB
- Complex Markdown: 476 KB

**Analysis:** This method is very fast, especially for plain structured content.

---

### ChunksExtensions

#### BuildRelationsGraph

**Observed performance:**
- Medium (1K words): 218.15 us
- Complex (5K words): 1.75 ms

**Memory usage:**
- Medium: 325 KB
- Complex: 2.01 MB

**Analysis:** Performance is good and suitable for this workload.

**Hot spots:**
1. heading hierarchy construction
2. repeated LINQ usage
3. object creation for `RelationshipModel`

#### FindRepeatedChunksWithUrls

**Observed performance:**
- Two-document collection: 2.06 ms

**Memory usage:** 2.39 MB

**Analysis:** Acceptable, but this method has the most room for optimization.

**Hot spots:**
1. `SelectMany`, `Where`, `GroupBy`
2. `TryGetValue` for each chunk
3. intermediate anonymous objects

---

## Python comparison

### Common Python libraries

1. `langchain.text_splitter`
2. `tiktoken`
3. `nltk`
4. `spaCy`

### Why .NET can be faster

- compiled machine code
- `Span<T>` for low-allocation memory handling
- `GeneratedRegex`
- value types reduce allocations

### Why Python can sometimes be faster

- optimized C extensions
- mature NLP pipelines
- JIT support through tools like Numba

### Practical comparison

For 10K words, this library is typically much faster than common Python chunking libraries, especially for structured Markdown workloads.

---

## Hot spots and recommendations

### 1. Regex operations

Regex performance is already good thanks to `GeneratedRegex`, but combining some passes could help for very large documents.

### 2. `StringBuilder.Replace`

This is acceptable today, but a single-pass replacement strategy could reduce allocations.

### 3. HTML table extraction

Current performance is fine. A stack-based implementation could simplify the code and help protect against excessive nesting.

### 4. LINQ-heavy graph and duplicate logic

`FindRepeatedChunksWithUrls` is the best candidate for optimization by replacing LINQ with loops and by reducing intermediate allocations.

---

## Optimization roadmap

### Priority 1
1. Optimize `FindRepeatedChunksWithUrls`
2. Reduce repeated regex passes where possible

### Priority 2
3. Improve `StringBuilder.Replace` usage
4. Reduce allocations with `ArrayPool` where it makes sense

### Priority 3
5. Add caching for repeated processing
6. Consider parallel processing for extremely large documents

---

## Summary

The library is already fast enough for most practical scenarios. The main remaining optimization target is `FindRepeatedChunksWithUrls`, followed by allocation reduction in a few hot paths.

The next step is to keep benchmarking changes over time so regressions can be caught early.
