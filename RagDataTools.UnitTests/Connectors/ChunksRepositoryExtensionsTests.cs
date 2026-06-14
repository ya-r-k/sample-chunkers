using FluentAssertions;
using RagDataTools.Chunkers.Models;
using RagDataTools.Chunkers.Models.Enums;
using RagDataTools.Connectors.Extensions;
using RagDataTools.Connectors.Interfaces;

namespace RagDataTools.UnitTests.Connectors;

public class ChunksRepositoryExtensionsTests
{
    [Test]
    public async Task SaveAsync_ShouldForwardScopeAndChunks()
    {
        var repository = new FakeChunksRepository();
        var chunks = new[]
        {
            new ChunkModel
            {
                Index = 1,
                ChunkType = ChunkType.TextChunk,
                RawContent = "alpha",
                Data = new Dictionary<string, object>(),
                RelatedChunksIndexes = new Dictionary<RelationshipType, List<int>>(),
            },
        };

        await repository.SaveAsync("doc-1", chunks);

        repository.ScopeId.Should().Be("doc-1");
        repository.CapturedChunks.Should().BeEquivalentTo(chunks);
    }

    private sealed class FakeChunksRepository : IChunksRepository<string, string>
    {
        public string? ScopeId { get; private set; }
        public IReadOnlyList<ChunkModel> CapturedChunks { get; private set; } = [];

        public Task AddAsync(string[] flags, params ChunkModel[] chunks)
        {
            ScopeId = string.Join(":", flags);
            CapturedChunks = chunks;
            return Task.CompletedTask;
        }

        public Task AddAsync(string scopeId, params ChunkModel[] chunks)
        {
            ScopeId = scopeId;
            CapturedChunks = chunks;
            return Task.CompletedTask;
        }

        public Task<IDictionary<int, string>> GetIndexesIdsPairsByFlagAsync(string flag) => throw new NotSupportedException();
        public Task<IDictionary<int, string>> GetIndexesIdsPairsByScopeIdAsync(string scopeId) => throw new NotSupportedException();
        public Task RemoveFlagFromAllDataAsync(string flag) => throw new NotSupportedException();
        public Task RemoveScopeAsync(string scopeId) => throw new NotSupportedException();
    }
}
