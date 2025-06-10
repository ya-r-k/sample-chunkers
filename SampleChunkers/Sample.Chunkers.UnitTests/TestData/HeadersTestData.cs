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
            Data = new HeaderData
            {
                Level = 1,
                Text = "Data Modeling: A Comprehensive Guide for Analysts",
            },
        },
        new ChunkModel
        {
            Index = 26,
            ChunkType = ChunkType.Title,
            RelatedChunksIndexes = [],
            RawContent = "## **What is Data Modeling?**",
            Data = new HeaderData
            {
                Level = 2,
                Text = "**What is Data Modeling?**",
            },
        },
        new ChunkModel
        {
            Index = 27,
            ChunkType = ChunkType.Title,
            RelatedChunksIndexes = [],
            RawContent = "### **What is Data Model?**",
            Data = new HeaderData
            {
                Level = 3,
                Text = "**What is Data Model?**",
            },
        },
        new ChunkModel
        {
            Index = 28,
            ChunkType = ChunkType.Title,
            RelatedChunksIndexes = [],
            RawContent = "## Types of Data Models",
            Data = new HeaderData
            {
                Level = 2,
                Text = "Types of Data Models",
            },
        },
        new ChunkModel
        {
            Index = 29,
            ChunkType = ChunkType.Title,
            RelatedChunksIndexes = [],
            RawContent = "## Data Modeling Process",
            Data = new HeaderData
            {
                Level = 2,
                Text = "Data Modeling Process",
            },
        },
        new ChunkModel
        {
            Index = 30,
            ChunkType = ChunkType.Title,
            RelatedChunksIndexes = [],
            RawContent = "## Types of Data Modeling",
            Data = new HeaderData
            {
                Level = 2,
                Text = "Types of Data Modeling",
            },
        },
        new ChunkModel
        {
            Index = 31,
            ChunkType = ChunkType.Title,
            RelatedChunksIndexes = [],
            RawContent = "## Benefits of Data Modeling",
            Data = new HeaderData
            {
                Level = 2,
                Text = "Benefits of Data Modeling",
            },
        },
        new ChunkModel
        {
            Index = 32,
            ChunkType = ChunkType.Title,
            RelatedChunksIndexes = [],
            RawContent = "## Conclusion",
            Data = new HeaderData
            {
                Level = 2,
                Text = "Conclusion",
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
            Data = new HeaderData
            {
                Level = 1,
                Text = "Basic Operators in Relational Algebra",
            },
        },
        new ChunkModel
        {
            Index = 39,
            ChunkType = ChunkType.Title,
            RelatedChunksIndexes = [],
            RawContent = "## ***1. Selection operator (*****σ*****)***",
            Data = new HeaderData
            {
                Level = 2,
                Text = "***1. Selection operator (*****σ*****)***",
            },
        },
        new ChunkModel
        {
            Index = 40,
            ChunkType = ChunkType.Title,
            RelatedChunksIndexes = [],
            RawContent = "## ***2. Projection Operator (*****∏*****)***",
            Data = new HeaderData
            {
                Level = 2,
                Text = "***2. Projection Operator (*****∏*****)***",
            },
        },
        new ChunkModel
        {
            Index = 41,
            ChunkType = ChunkType.Title,
            RelatedChunksIndexes = [],
            RawContent = "## ***3. Cross Product(X)***",
            Data = new HeaderData
            {
                Level = 2,
                Text = "***3. Cross Product(X)***",
            },
        },
        new ChunkModel
        {
            Index = 42,
            ChunkType = ChunkType.Title,
            RelatedChunksIndexes = [],
            RawContent = "## ***4. Union (U)***",
            Data = new HeaderData
            {
                Level = 2,
                Text = "***4. Union (U)***",
            },
        },
        new ChunkModel
        {
            Index = 43,
            ChunkType = ChunkType.Title,
            RelatedChunksIndexes = [],
            RawContent = "## ***5. Minus (-) or Set Difference***",
            Data = new HeaderData
            {
                Level = 2,
                Text = "***5. Minus (-) or Set Difference***",
            },
        },
        new ChunkModel
        {
            Index = 44,
            ChunkType = ChunkType.Title,
            RelatedChunksIndexes = [],
            RawContent = "## ***6. Rename(*****ρ*****)***",
            Data = new HeaderData
            {
                Level = 2,
                Text = "***6. Rename(*****ρ*****)***",
            },
        },
        new ChunkModel
        {
            Index = 45,
            ChunkType = ChunkType.Title,
            RelatedChunksIndexes = [],
            RawContent = "### Extended Relational Algebra Operators",
            Data = new HeaderData
            {
                Level = 3,
                Text = "Extended Relational Algebra Operators",
            },
        },
        new ChunkModel
        {
            Index = 46,
            ChunkType = ChunkType.Title,
            RelatedChunksIndexes = [],
            RawContent = "## Conclusion",
            Data = new HeaderData
            {
                Level = 2,
                Text = "Conclusion",
            },
        },
    ];
}
