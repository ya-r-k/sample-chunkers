using FluentAssertions;
using Sample.Chunkers.Enums;
using Sample.Chunkers.Extensions;
using Sample.Chunkers.Models;
using Sample.Chunkers.UnitTests.TestData;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Sample.Chunkers.UnitTests.Extensions;

public class ChunksExtensionsTests
{
    [Test]
    public void BuildRelationsGraph_WithRealWorldText_ShouldReturnCorrectChunksRelations()
    {
        // Arrange
        var text = ArticlesTestData.DevToRealWorldArticleText;
        var expectedResult = RelationsTestData.DevToRealWorldArticleChunksRelations;

        // Act
        var chunks = text.ExtractSemanticChunksDeeply(200, SemanticsType.Sentence, 0.5);
        var relationsips = chunks.BuildRelationsGraph();

        // Assert
        relationsips.Should().BeEquivalentTo(expectedResult);
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
        var texts = new[]
        {
            // https://dev.to/alex_ricciardi/recursion-in-programming-techniques-benefits-and-limitations-java-3o4p
            ArticlesTestData.DevToRealWorldArticleText,
            // https://www.geeksforgeeks.org/data-modeling-a-comprehensive-guide-for-analysts/
            ArticlesTestData.GeeksForGeeksTextAboutDataModeling,
            // https://www.geeksforgeeks.org/basic-operators-in-relational-algebra-2/
            ArticlesTestData.GeeksForGeeksTextAboutRelationalAlgebra,
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

        var rawRelations = new Dictionary<int, List<RelationshipModel>>
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
        var textChunksSequence = rawRelations.Select(x => new
        {
            x.Key,
            Value = x.Value.Where(x => x.RelationshipType == RelationshipType.HasNextChunk)
                .ToList()
        }).ToDictionary(x => x.Key, x => x.Value);
        var titlesRelations = rawRelations.Select(x => new
        {
            x.Key,
            Value = x.Value.Where(x => x.RelationshipType == RelationshipType.HasNextTopic || x.RelationshipType == RelationshipType.HasFirstSubtopic)
                .ToList()
        }).ToDictionary(x => x.Key, x => x.Value);
        rawRelations = rawRelations.Select(x => new
        {
            x.Key,
            Value = x.Value.Except(textChunksSequence[x.Key])
                           .Except(titlesRelations[x.Key])
                           .ToList()
        }).ToDictionary(x => x.Key, x => x.Value);

        var expectedResult = textChunksSequence[0]
            .Concat(textChunksSequence[1])
            .Concat(textChunksSequence[2])
            .Concat(titlesRelations[1])
            .Concat(titlesRelations[2])
            .Concat(rawRelations[0])
            .Concat(rawRelations[1])
            .Concat(rawRelations[2])
            .ToList();

        // Act
        var chunks = texts.ExtractSemanticChunksDeeply(200, SemanticsType.Sentence, 0.5);
        var relationsips = chunks.BuildRelationsGraph();

        // Assert
        relationsips.Should().BeEquivalentTo(expectedResult);
    }
}
