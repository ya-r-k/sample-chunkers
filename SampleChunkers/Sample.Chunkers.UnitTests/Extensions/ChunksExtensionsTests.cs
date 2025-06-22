using FluentAssertions;
using Sample.Chunkers.Enums;
using Sample.Chunkers.Extensions;
using Sample.Chunkers.Models;
using Sample.Chunkers.UnitTests.TestData;

namespace Sample.Chunkers.UnitTests.Extensions;

public class ChunksExtensionsTests
{
    [Test]
    public void BuildRelationsGraph_WithRealWorldTextWithInfoBlocks_ShouldReturnCorrectHeadersRelations()
    {
        // Arrange
        var text = ArticlesTestData.ArticleWithMathInfoBlocks;
        var expectedTopicsRelations = RelationsTestData.ArticleWithMathInfoBlocks;

        // Act
        var chunks = text.ExtractSemanticChunksDeeply(200, SemanticsType.Sentence, 0.5, withTables: true, withCodeBlocks: true, withImages: true, withLinks: true);
        var relationships = chunks.BuildRelationsGraph();

        // Assert
        relationships.Intersect(expectedTopicsRelations).Should().BeEquivalentTo(expectedTopicsRelations);
    }

    [Test]
    public void BuildRelationsGraph_WithRealWorldText_ShouldReturnCorrectChunksRelations()
    {
        // Arrange
        var text = ArticlesTestData.DevToRealWorldArticleText;
        var expectedResult = RelationsTestData.DevToRealWorldArticleChunksRelations;

        // Act
        var chunks = text.ExtractSemanticChunksDeeply(200, SemanticsType.Sentence, 0.5);
        var relationships = chunks.BuildRelationsGraph();

        // Assert
        relationships.Should().BeEquivalentTo(expectedResult);
    }

    [Test]
    public void BuildRelationsGraph_WithRealWorldSecondText_ShouldReturnCorrectChunksRelations()
    {
        // Arrange
        var text = ArticlesTestData.GeeksForGeeksTextAboutDataModeling;
        var expectedResult = RelationsTestData.GeeksForGeeksAboutDataModelingChunksRelations;

        // Act
        var chunks = text.ExtractSemanticChunksDeeply(200, SemanticsType.Sentence, 0.5);
        var relationsips = chunks.BuildRelationsGraph();

        // Assert
        relationsips.Should().BeEquivalentTo(expectedResult);
    }

    [Test]
    public void BuildRelationsGraph_WithRealWorldTextsCollection_ShouldReturnCorrectChunksRelations()
    {
        // Arrange
        var texts = new Dictionary<int, string>
        {
            // https://dev.to/alex_ricciardi/recursion-in-programming-techniques-benefits-and-limitations-java-3o4p
            [0] = ArticlesTestData.DevToRealWorldArticleText,
            // https://www.geeksforgeeks.org/data-modeling-a-comprehensive-guide-for-analysts/
            [1] = ArticlesTestData.GeeksForGeeksTextAboutDataModeling,
            // https://www.geeksforgeeks.org/basic-operators-in-relational-algebra-2/
            [2] = ArticlesTestData.GeeksForGeeksTextAboutRelationalAlgebra,
        };

        var secondShiftValue = CodeBlocksTestData.DevToRealWorldArticleCodeBlocks.Length +
                               LinksTestData.DevToRealWorldArticleLinks.Length +
                               TextChunkTestData.DevToRealWorldArticleTextChunks.Length;
        var thirdShiftValue = secondShiftValue +
                              ImageLinksTestData.GeeksForGeeksAboutDataModelingImageLinks.Length +
                              LinksTestData.GeeksForGeeksAboutDataModelingLinks.Length +
                              HeadersTestData.GeeksForGeeksAboutDataModelingHeaders.Length +
                              TextChunkTestData.GeeksForGeeksAboutDataModelingTextChunks.Length;
        var indexShiftValues = new[]
        {
            0,
            secondShiftValue,
            thirdShiftValue,
        };

        var expectedResult = new Dictionary<int, List<RelationshipModel>>
        {
            [0] = RelationsTestData.DevToRealWorldArticleChunksRelations,
            [1] = RelationsTestData.GeeksForGeeksAboutDataModelingChunksRelations,
            [2] = RelationsTestData.GeeksForGeeksAboutRelationalAlgebraChunksRelations,
        }.Select(x => new
        {
            x.Key,
            Value = x.Value.Select(y => new RelationshipModel
            {
                FirstChunkIndex = y.FirstChunkIndex + indexShiftValues[x.Key],
                SecondChunkIndex = y.SecondChunkIndex + indexShiftValues[x.Key],
                RelationshipType = y.RelationshipType,
            }).ToList()
        }).ToDictionary(x => x.Key, x => x.Value);

        // Act
        var chunks = texts.ExtractSemanticChunksDeeply(200, SemanticsType.Sentence, 0.5);
        var relationsips = chunks.BuildRelationsGraph();

        // Assert
        relationsips.Should().BeEquivalentTo(expectedResult);
    }
}
