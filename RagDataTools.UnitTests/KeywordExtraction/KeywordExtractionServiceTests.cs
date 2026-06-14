using FluentAssertions;
using RagDataTools.Chunkers.Models;
using RagDataTools.Chunkers.Models.Enums;
using RagDataTools.KeywordExtraction;

namespace RagDataTools.UnitTests.KeywordExtraction;

public class KeywordExtractionServiceTests
{
    [Test]
    public async Task ExtractAsync_ShouldAttachKeywordsToEachChunk()
    {
        var service = new KeywordExtractionService(new FixedKeywordBackend(
            ["alpha", "beta"],
            ["gamma"]));

        var chunks = CreateChunks("alpha beta alpha", "gamma delta");

        var extracted = await service.ExtractAsync(chunks, new KeywordExtractionOptions
        {
            Timeout = TimeSpan.FromSeconds(1),
            MaxKeywordsPerChunk = 5,
        });

        extracted[0].GetKeywords().Should().BeEquivalentTo(["alpha", "beta"]);
        extracted[1].GetKeywords().Should().BeEquivalentTo(["gamma"]);
    }

    [Test]
    public async Task ExtractAsync_ShouldTimeoutWhenBackendDoesNotComplete()
    {
        var service = new KeywordExtractionService(new SlowKeywordBackend());

        var act = async () => await service.ExtractAsync(
            CreateChunks("slow text"),
            new KeywordExtractionOptions
            {
                Timeout = TimeSpan.FromMilliseconds(25),
            });

        await act.Should().ThrowAsync<TimeoutException>()
            .WithMessage("*timed out*");
    }

    [Test]
    public async Task ExtractAsync_MinimalTextReturnsEmptyKeywordsAndUnavailableBackendSurfacesError()
    {
        var heuristicService = new KeywordExtractionService();
        var minimalChunks = CreateChunks("the and of");

        var extracted = await heuristicService.ExtractAsync(
            minimalChunks,
            new KeywordExtractionOptions
            {
                Timeout = TimeSpan.FromSeconds(1),
            });

        extracted[0].GetKeywords().Should().BeEmpty();

        var unavailableService = new KeywordExtractionService(new UnavailableKeywordBackend());
        var act = async () => await unavailableService.ExtractAsync(
            CreateChunks("alpha beta"),
            new KeywordExtractionOptions
            {
                Timeout = TimeSpan.FromSeconds(1),
            });

        var exception = await act.Should().ThrowAsync<InvalidOperationException>();
        exception.Which.Message.Should().Contain("unavailable");
    }

    private static ChunkModel[] CreateChunks(params string[] texts)
    {
        var chunks = new ChunkModel[texts.Length];
        for (var i = 0; i < texts.Length; i++)
        {
            chunks[i] = new ChunkModel
            {
                Index = i,
                ChunkType = ChunkType.TextChunk,
                RawContent = texts[i],
                Data = new Dictionary<string, object>(),
                RelatedChunksIndexes = new Dictionary<RelationshipType, List<int>>(),
            };
        }

        return chunks;
    }

    private sealed class FixedKeywordBackend : IKeywordExtractionBackend
    {
        private readonly string[][] keywordsByChunk;

        public FixedKeywordBackend(params string[][] keywordsByChunk)
        {
            this.keywordsByChunk = keywordsByChunk;
        }

        public Task<string[]> ExtractAsync(ChunkModel chunk, KeywordExtractionOptions options, CancellationToken cancellationToken = default)
        {
            var keywords = chunk.Index < keywordsByChunk.Length ? keywordsByChunk[chunk.Index] : Array.Empty<string>();
            return Task.FromResult(keywords);
        }
    }

    private sealed class SlowKeywordBackend : IKeywordExtractionBackend
    {
        public async Task<string[]> ExtractAsync(ChunkModel chunk, KeywordExtractionOptions options, CancellationToken cancellationToken = default)
        {
            await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
            return ["slow"];
        }
    }

    private sealed class UnavailableKeywordBackend : IKeywordExtractionBackend
    {
        public Task<string[]> ExtractAsync(ChunkModel chunk, KeywordExtractionOptions options, CancellationToken cancellationToken = default)
        {
            throw new InvalidOperationException("Keyword backend unavailable.");
        }
    }
}
