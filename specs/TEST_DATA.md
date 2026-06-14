# Test data storage

## Overview

This document describes how test data is organized in the RagDataTools test project and what trade-offs each storage approach has.

---

## Current structure

Test data is stored in static classes under `TestData/`:

```text
RagDataTools.UnitTests/
└── TestData/
    ├── ArticlesTestData.cs        # Full article texts
    ├── CodeBlocksTestData.cs      # Expected code blocks
    ├── HeadersTestData.cs         # Expected headings
    ├── ImageLinksTestData.cs      # Expected images
    ├── InfoBlocksTestData.cs      # Expected info blocks
    ├── LinksTestData.cs           # Expected links
    ├── RelationsTestData.cs       # Expected relations
    ├── TablesTestData.cs          # Expected tables
    └── TextChunkTestData.cs       # Expected text chunks
```

## Current approach

### Pros
- All data is in one place
- Data is compiled with the code
- Fast access
- Type safety

### Cons
- Large files with long strings
- Hard to edit large texts inside C# string literals
- Hard to keep inputs and expected outputs visually aligned
- Data is duplicated across several files
- Test data is not versioned separately from code

---

## Storage options

### Option 1: Static classes

This is the current approach.

**Pros**
- Fast
- Type-safe
- No runtime file loading
- No test-time performance penalty

**Cons**
- Hard to edit large texts
- Poor visual formatting for Markdown or HTML
- Hard to trace data relationships
- Large source files

**Recommendation:** Good for small test data, not ideal for large articles.

---

### Option 2: Embedded resources

Store data as embedded resources in the assembly.

**Pros**
- Data is separated from code
- Easy to edit in text editors
- Supports Markdown, TXT, JSON, and other formats
- Keeps code cleaner

**Cons**
- Requires runtime loading
- No direct type safety
- Needs parsing for structured data

**Example:**
```csharp
var assembly = Assembly.GetExecutingAssembly();
var resourceName = "RagDataTools.UnitTests.TestData.Articles.article1.md";
using var stream = assembly.GetManifestResourceStream(resourceName);
using var reader = new StreamReader(stream);
var content = await reader.ReadToEndAsync();
```

**Recommendation:** A good option for large texts, but it adds loading code.

---

### Option 3: Separate files

Store test data as files in a project directory.

**Example structure:**
```text
TestData/
├── Articles/
│   ├── devto_article.md
│   ├── geeksforgeeks_data_modeling.md
│   └── wikipedia_complex_tables.md
├── Expected/
│   ├── code_blocks.json
│   ├── headers.json
│   ├── links.json
│   └── relations.json
└── Config/
    └── test_cases.json
```

**Pros**
- Easy to edit in any editor
- Great syntax highlighting for Markdown and HTML
- Easy to version separately
- Readable and clear
- Easy to add new test cases

**Cons**
- Requires runtime loading
- Needs copy-to-output configuration
- JSON is not type-safe by itself

**Recommendation:** Good for readability and maintainability, but it needs project setup.

---

### Option 4: JSON or YAML

Use structured files for test cases and expected results.

**Example JSON:**
```json
{
  "testCases": [
    {
      "name": "DevTo Real World Article",
      "input": "Articles/devto_article.md",
      "expected": {
        "codeBlocks": "Expected/devto_code_blocks.json",
        "links": "Expected/devto_links.json",
        "textChunks": "Expected/devto_text_chunks.json",
        "relations": "Expected/devto_relations.json"
      },
      "config": {
        "chunkWordsCount": 200,
        "semanticsType": "Sentence",
        "overlapPercentage": 0.5
      }
    }
  ]
}
```

**Pros**
- Structured
- Easy to parse with `System.Text.Json`
- Easy to generate automatically
- Supports references between files

**Cons**
- Requires parsing
- Not directly type-safe
- Adds loader code

**Recommendation:** Good for structured test configurations.

---

### Option 5: Hybrid approach

Combine several approaches depending on the data type.

**Structure:**
```text
TestData/
├── Articles/                    # Embedded resources or files
├── Expected/                    # JSON files or code
├── SmallTestCases.cs            # Small test cases in code
└── TestDataLoader.cs            # Shared loading helper
```

**Pros**
- Flexible
- Good balance between performance and readability
- Large data stays readable
- Small data stays type-safe

**Cons**
- More organization work
- Multiple loading strategies

**Recommendation:** The best balance for this project.

---

## Recommended approach for this project

Use the hybrid approach:

1. Large articles -> embedded resources or files
2. Expected results -> JSON files
3. Small cases -> C# code
4. Shared loader -> `TestDataLoader`

### Suggested project layout

```text
RagDataTools.UnitTests/
├── TestData/
│   ├── Articles/
│   ├── Expected/
│   ├── SmallTestCases.cs
│   └── TestDataLoader.cs
└── Extensions/
    └── ... tests ...
```

---

## TestDataLoader example

```csharp
namespace RagDataTools.UnitTests.TestData;

public static class TestDataLoader
{
    private static readonly Dictionary<string, string> _cachedArticles = new();
    private static readonly Dictionary<string, ChunkModel[]> _cachedExpected = new();

    public static string LoadArticle(string fileName)
    {
        if (_cachedArticles.TryGetValue(fileName, out var cached))
            return cached;

        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = $"RagDataTools.UnitTests.TestData.Articles.{fileName}";

        using var stream = assembly.GetManifestResourceStream(resourceName);
        if (stream == null)
            throw new FileNotFoundException($"Article not found: {fileName}");

        using var reader = new StreamReader(stream);
        var content = reader.ReadToEnd();

        _cachedArticles[fileName] = content;
        return content;
    }
}
```

### Example in tests

```csharp
[Test]
public void ExtractSemanticChunksDeeply_WithRealWorldText_ShouldReturnCorrectChunks()
{
    var text = TestDataLoader.LoadArticle("devto_article.md");

    var chunks = text.ExtractSemanticChunksDeeply(200, SemanticsType.Sentence, 0.5);

    chunks.Should().NotBeNull();
}
```

---

## Formats for expected data

### JSON

Recommended for expected results.

**Pros**
- Standard format
- Easy to parse
- IDE-friendly
- Readable

### YAML

More readable than JSON, but requires an extra package.

### C# code

Best for tiny test cases.

**Recommendation:** Use C# code for small data and JSON for larger expected results.

---

## Performance

- Static classes: ~0 ms loading
- Embedded resources: ~1-5 ms loading, then ~0 ms with caching
- JSON files: ~5-10 ms loading and parsing, then ~0 ms with caching

With caching, the difference is negligible.

---

## Recommendations

1. Keep the current approach for small cases
2. Move large articles to embedded resources or files
3. Move expected results to JSON
4. Add `TestDataLoader`
5. Add caching

---

## Summary

The best approach for this project is a hybrid one:
- embedded resources or files for large articles
- JSON for expected results
- C# code for tiny cases
- caching for performance

The next step is to introduce a shared loader and gradually move large test data out of source files.
