﻿using Sample.Chunkers.Enums;
using Sample.Chunkers.Models;

namespace Sample.Chunkers.UnitTests.TestData;

internal static class HeadersTestData
{
    // https://www.geeksforgeeks.org/functions-in-discrete-mathematics/
    internal static readonly ChunkModel[] ArticleWithMathInfoBlocksHeaders =
    [
        new ChunkModel
        {
            Index = 34,
            ChunkType = ChunkType.Topic,
            RelatedChunksIndexes = [],
            RawContent = "# Functions in Discrete Mathematics",
            Data = new Dictionary<string, object>
            {
                ["name"] = "Functions in Discrete Mathematics",
                ["level"] = 1,
            },
        },
        new ChunkModel
        {
            Index = 35,
            ChunkType = ChunkType.Topic,
            RelatedChunksIndexes = [],
            RawContent = "## What is Function?",
            Data = new Dictionary<string, object>
            {
                ["name"] = "What is Function?",
                ["level"] = 2,
            },
        },
        new ChunkModel
        {
            Index = 36,
            ChunkType = ChunkType.Topic,
            RelatedChunksIndexes = [],
            RawContent = "### Pre-Image and Image of a Function",
            Data = new Dictionary<string, object>
            {
                ["name"] = "Pre-Image and Image of a Function",
                ["level"] = 3,
            },
        },
        new ChunkModel
        {
            Index = 37,
            ChunkType = ChunkType.Topic,
            RelatedChunksIndexes = [],
            RawContent = "### Domain of a Function",
            Data = new Dictionary<string, object>
            {
                ["name"] = "Domain of a Function",
                ["level"] = 3,
            },
        },
        new ChunkModel
        {
            Index = 38,
            ChunkType = ChunkType.Topic,
            RelatedChunksIndexes = [],
            RawContent = "### Codomain (Range) of a Function",
            Data = new Dictionary<string, object>
            {
                ["name"] = "Codomain (Range) of a Function",
                ["level"] = 3,
            },
        },
        new ChunkModel
        {
            Index = 39,
            ChunkType = ChunkType.Topic,
            RelatedChunksIndexes = [],
            RawContent = "## Types of Function",
            Data = new Dictionary<string, object>
            {
                ["name"] = "Types of Function",
                ["level"] = 2,
            },
        },
        new ChunkModel
        {
            Index = 40,
            ChunkType = ChunkType.Topic,
            RelatedChunksIndexes = [],
            RawContent = "### One-One Function ( or Injective Function):",
            Data = new Dictionary<string, object>
            {
                ["name"] = "One-One Function ( or Injective Function):",
                ["level"] = 3,
            },
        },
        new ChunkModel
        {
            Index = 41,
            ChunkType = ChunkType.Topic,
            RelatedChunksIndexes = [],
            RawContent = "### Many-One Function",
            Data = new Dictionary<string, object>
            {
                ["name"] = "Many-One Function",
                ["level"] = 3,
            },
        },
        new ChunkModel
        {
            Index = 42,
            ChunkType = ChunkType.Topic,
            RelatedChunksIndexes = [],
            RawContent = "### Onto Function( or Surjective Function)",
            Data = new Dictionary<string, object>
            {
                ["name"] = "Onto Function( or Surjective Function)",
                ["level"] = 3,
            },
        },
        new ChunkModel
        {
            Index = 43,
            ChunkType = ChunkType.Topic,
            RelatedChunksIndexes = [],
            RawContent = "### Into Function",
            Data = new Dictionary<string, object>
            {
                ["name"] = "Into Function",
                ["level"] = 3,
            },
        },
        new ChunkModel
        {
            Index = 44,
            ChunkType = ChunkType.Topic,
            RelatedChunksIndexes = [],
            RawContent = "### One-One Correspondent Function (or Bijective Function or One-One Onto Function)",
            Data = new Dictionary<string, object>
            {
                ["name"] = "One-One Correspondent Function (or Bijective Function or One-One Onto Function)",
                ["level"] = 3,
            },
        },
        new ChunkModel
        {
            Index = 45,
            ChunkType = ChunkType.Topic,
            RelatedChunksIndexes = [],
            RawContent = "### One-One Into Function",
            Data = new Dictionary<string, object>
            {
                ["name"] = "One-One Into Function",
                ["level"] = 3,
            },
        },
        new ChunkModel
        {
            Index = 46,
            ChunkType = ChunkType.Topic,
            RelatedChunksIndexes = [],
            RawContent = "### Many-One Onto Function",
            Data = new Dictionary<string, object>
            {
                ["name"] = "Many-One Onto Function",
                ["level"] = 3,
            },
        },
        new ChunkModel
        {
            Index = 47,
            ChunkType = ChunkType.Topic,
            RelatedChunksIndexes = [],
            RawContent = "### Many-One Into Function",
            Data = new Dictionary<string, object>
            {
                ["name"] = "Many-One Into Function",
                ["level"] = 3,
            },
        },
        new ChunkModel
        {
            Index = 48,
            ChunkType = ChunkType.Topic,
            RelatedChunksIndexes = [],
            RawContent = "## Inverse of a Function",
            Data = new Dictionary<string, object>
            {
                ["name"] = "Inverse of a Function",
                ["level"] = 2,
            },
        },
        new ChunkModel
        {
            Index = 49,
            ChunkType = ChunkType.Topic,
            RelatedChunksIndexes = [],
            RawContent = "## Composition of Functions",
            Data = new Dictionary<string, object>
            {
                ["name"] = "Composition of Functions",
                ["level"] = 2,
            },
        },
        new ChunkModel
        {
            Index = 50,
            ChunkType = ChunkType.Topic,
            RelatedChunksIndexes = [],
            RawContent = "## Summary: Functions in Discrete Mathematics",
            Data = new Dictionary<string, object>
            {
                ["name"] = "Summary: Functions in Discrete Mathematics",
                ["level"] = 2,
            },
        },
        new ChunkModel
        {
            Index = 51,
            ChunkType = ChunkType.Topic,
            RelatedChunksIndexes = [],
            RawContent = "## Sample Questions on Functions in Discrete Mathematics",
            Data = new Dictionary<string, object>
            {
                ["name"] = "Sample Questions on Functions in Discrete Mathematics",
                ["level"] = 2,
            },
        },
        new ChunkModel
        {
            Index = 52,
            ChunkType = ChunkType.Topic,
            RelatedChunksIndexes = [],
            RawContent = "### Related Articles:",
            Data = new Dictionary<string, object>
            {
                ["name"] = "Related Articles:",
                ["level"] = 3,
            },
        },
    ];

    // https://www.geeksforgeeks.org/data-modeling-a-comprehensive-guide-for-analysts/
    internal static readonly ChunkModel[] GeeksForGeeksAboutDataModelingHeaders =
    [
        new ChunkModel
        {
            Index = 25,
            ChunkType = ChunkType.Topic,
            RelatedChunksIndexes = [],
            RawContent = "# Data Modeling: A Comprehensive Guide for Analysts",
            Data = new Dictionary<string, object>
            {
                ["name"] = "Data Modeling: A Comprehensive Guide for Analysts",
                ["level"] = 1,
            },
        },
        new ChunkModel
        {
            Index = 26,
            ChunkType = ChunkType.Topic,
            RelatedChunksIndexes = [],
            RawContent = "## **What is Data Modeling?**",
            Data = new Dictionary<string, object>
            {
                ["name"] = "**What is Data Modeling?**",
                ["level"] = 2,
            },
        },
        new ChunkModel
        {
            Index = 27,
            ChunkType = ChunkType.Topic,
            RelatedChunksIndexes = [],
            RawContent = "### **What is Data Model?**",
            Data = new Dictionary<string, object>
            {
                ["name"] = "**What is Data Model?**",
                ["level"] = 3,
            },
        },
        new ChunkModel
        {
            Index = 28,
            ChunkType = ChunkType.Topic,
            RelatedChunksIndexes = [],
            RawContent = "## Types of Data Models",
            Data = new Dictionary<string, object>
            {
                ["name"] = "Types of Data Models",
                ["level"] = 2,
            },
        },
        new ChunkModel
        {
            Index = 29,
            ChunkType = ChunkType.Topic,
            RelatedChunksIndexes = [],
            RawContent = "## Data Modeling Process",
            Data = new Dictionary<string, object>
            {
                ["name"] = "Data Modeling Process",
                ["level"] = 2,
            },
        },
        new ChunkModel
        {
            Index = 30,
            ChunkType = ChunkType.Topic,
            RelatedChunksIndexes = [],
            RawContent = "## Types of Data Modeling",
            Data = new Dictionary<string, object>
            {
                ["name"] = "Types of Data Modeling",
                ["level"] = 2,
            },
        },
        new ChunkModel
        {
            Index = 31,
            ChunkType = ChunkType.Topic,
            RelatedChunksIndexes = [],
            RawContent = "## Benefits of Data Modeling",
            Data = new Dictionary<string, object>
            {
                ["name"] = "Benefits of Data Modeling",
                ["level"] = 2,
            },
        },
        new ChunkModel
        {
            Index = 32,
            ChunkType = ChunkType.Topic,
            RelatedChunksIndexes = [],
            RawContent = "## Conclusion",
            Data = new Dictionary<string, object>
            {
                ["name"] = "Conclusion",
                ["level"] = 2,
            },
        },
    ];

    // https://www.geeksforgeeks.org/basic-operators-in-relational-algebra-2/
    internal static readonly ChunkModel[] GeeksForGeeksAboutRelationalAlgebraHeaders =
    [
        new ChunkModel
        {
            Index = 38,
            ChunkType = ChunkType.Topic,
            RelatedChunksIndexes = [],
            RawContent = "# Basic Operators in Relational Algebra",
            Data = new Dictionary<string, object>
            {
                ["name"] = "Basic Operators in Relational Algebra",
                ["level"] = 1,
            },
        },
        new ChunkModel
        {
            Index = 39,
            ChunkType = ChunkType.Topic,
            RelatedChunksIndexes = [],
            RawContent = "## ***1. Selection operator (*****σ*****)***",
            Data = new Dictionary<string, object>
            {
                ["name"] = "***1. Selection operator (*****σ*****)***",
                ["level"] = 2,
            },
        },
        new ChunkModel
        {
            Index = 40,
            ChunkType = ChunkType.Topic,
            RelatedChunksIndexes = [],
            RawContent = "## ***2. Projection Operator (*****∏*****)***",
            Data = new Dictionary<string, object>
            {
                ["name"] = "***2. Projection Operator (*****∏*****)***",
                ["level"] = 2,
            },
        },
        new ChunkModel
        {
            Index = 41,
            ChunkType = ChunkType.Topic,
            RelatedChunksIndexes = [],
            RawContent = "## ***3. Cross Product(X)***",
            Data = new Dictionary<string, object>
            {
                ["name"] = "***3. Cross Product(X)***",
                ["level"] = 2,
            },
        },
        new ChunkModel
        {
            Index = 42,
            ChunkType = ChunkType.Topic,
            RelatedChunksIndexes = [],
            RawContent = "## ***4. Union (U)***",
            Data = new Dictionary<string, object>
            {
                ["name"] = "***4. Union (U)***",
                ["level"] = 2,
            },
        },
        new ChunkModel
        {
            Index = 43,
            ChunkType = ChunkType.Topic,
            RelatedChunksIndexes = [],
            RawContent = "## ***5. Minus (-) or Set Difference***",
            Data = new Dictionary<string, object>
            {
                ["name"] = "***5. Minus (-) or Set Difference***",
                ["level"] = 2,
            },
        },
        new ChunkModel
        {
            Index = 44,
            ChunkType = ChunkType.Topic,
            RelatedChunksIndexes = [],
            RawContent = "## ***6. Rename(*****ρ*****)***",
            Data = new Dictionary<string, object>
            {
                ["name"] = "***6. Rename(*****ρ*****)***",
                ["level"] = 2,
            },
        },
        new ChunkModel
        {
            Index = 45,
            ChunkType = ChunkType.Topic,
            RelatedChunksIndexes = [],
            RawContent = "### Extended Relational Algebra Operators",
            Data = new Dictionary<string, object>
            {
                ["name"] = "Extended Relational Algebra Operators",
                ["level"] = 3,
            },
        },
        new ChunkModel
        {
            Index = 46,
            ChunkType = ChunkType.Topic,
            RelatedChunksIndexes = [],
            RawContent = "## Conclusion",
            Data = new Dictionary<string, object>
            {
                ["name"] = "Conclusion",
                ["level"] = 2,
            },
        },
    ];
}
