using Sample.Chunkers.Enums;
using Sample.Chunkers.Models;

namespace Sample.Chunkers.UnitTests.TestData;

internal static class HeadersTestData
{
    // https://www.geeksforgeeks.org/data-modeling-a-comprehensive-guide-for-analysts/
    internal static readonly ChunkModel[] GeeksForGeeksAboutDataModelingHeaders =
    [
        new ChunkModel
        {
            Index = 25,
            ChunkType = ChunkType.Title,
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
            ChunkType = ChunkType.Title,
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
            ChunkType = ChunkType.Title,
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
            ChunkType = ChunkType.Title,
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
            ChunkType = ChunkType.Title,
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
            ChunkType = ChunkType.Title,
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
            ChunkType = ChunkType.Title,
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
            ChunkType = ChunkType.Title,
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
            ChunkType = ChunkType.Title,
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
            ChunkType = ChunkType.Title,
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
            ChunkType = ChunkType.Title,
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
            ChunkType = ChunkType.Title,
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
            ChunkType = ChunkType.Title,
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
            ChunkType = ChunkType.Title,
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
            ChunkType = ChunkType.Title,
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
            ChunkType = ChunkType.Title,
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
            ChunkType = ChunkType.Title,
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
