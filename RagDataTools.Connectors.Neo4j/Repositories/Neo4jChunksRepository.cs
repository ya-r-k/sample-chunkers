using System.Text.Json;
using Neo4j.Driver;
using RagDataTools.Chunkers.Models;
using RagDataTools.Connectors.Interfaces;

namespace RagDataTools.Connectors.Neo4j.Repositories;

public class Neo4jChunksRepository(IDriver driver) : IChunksRepository<string, string>
{
    private const string DefaultDatabase = "neo4j";
    private const string ChunkLabel = "Chunk";

    public Task AddAsync(string[] flags, params ChunkModel[] chunks)
    {
        var scopeId = ComposeScopeId(flags);
        return AddAsync(scopeId, chunks);
    }

    public async Task AddAsync(string scopeId, params ChunkModel[] chunks)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(scopeId);

        await using var session = driver.AsyncSession(o => o.WithDatabase(DefaultDatabase));

        await session.ExecuteWriteAsync(async tx =>
        {
            await tx.RunAsync(
                $@"MATCH (n:{ChunkLabel} {{scope_id: $scopeId}})
                   DETACH DELETE n",
                new { scopeId });

            var nodes = chunks.Select(chunk => new
            {
                scopeId,
                index = chunk.Index,
                chunkType = chunk.ChunkType.ToString(),
                rawContent = chunk.RawContent,
                pageNumber = chunk.GetPageNumber(),
                headingLevel = chunk.GetHeadingLevel(),
                keywords = chunk.GetKeywords(),
                dataJson = JsonSerializer.Serialize(chunk.Data),
                relationshipsJson = JsonSerializer.Serialize(chunk.RelatedChunksIndexes),
            }).ToArray();

            await tx.RunAsync(
                $@"UNWIND $nodes AS item
                   CREATE (n:{ChunkLabel} {{
                       scope_id: item.scopeId,
                       index: item.index,
                       chunk_type: item.chunkType,
                       raw_content: item.rawContent,
                       page_number: item.pageNumber,
                       heading_level: item.headingLevel,
                       keywords: item.keywords,
                       data_json: item.dataJson,
                       related_chunks_indexes_json: item.relationshipsJson
                   }})",
                new { nodes });
        });
    }

    public Task<IDictionary<int, string>> GetIndexesIdsPairsByFlagAsync(string flag)
        => GetIndexesIdsPairsByScopeIdAsync(flag);

    public async Task<IDictionary<int, string>> GetIndexesIdsPairsByScopeIdAsync(string scopeId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(scopeId);

        await using var session = driver.AsyncSession(o => o.WithDatabase(DefaultDatabase));

        return await session.ExecuteReadAsync(async tx =>
        {
            var cursor = await tx.RunAsync(
                $@"MATCH (n:{ChunkLabel} {{scope_id: $scopeId}})
                   RETURN n.index AS index, elementId(n) AS id
                   ORDER BY n.index",
                new { scopeId });

            var records = await cursor.ToListAsync();
            return records.ToDictionary(
                record => record["index"].As<int>(),
                record => record["id"].As<string>());
        });
    }

    public Task RemoveFlagFromAllDataAsync(string flag)
        => RemoveScopeAsync(flag);

    public async Task RemoveScopeAsync(string scopeId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(scopeId);

        await using var session = driver.AsyncSession(o => o.WithDatabase(DefaultDatabase));

        await session.ExecuteWriteAsync(async tx =>
        {
            await tx.RunAsync(
                $@"MATCH (n:{ChunkLabel} {{scope_id: $scopeId}})
                   DETACH DELETE n",
                new { scopeId });
        });
    }

    private static string ComposeScopeId(IEnumerable<string> flags)
    {
        var values = flags.Where(flag => !string.IsNullOrWhiteSpace(flag)).ToArray();
        return values.Length == 0 ? throw new ArgumentException("At least one scope flag is required.", nameof(flags))
            : values.Length == 1 ? values[0] : string.Join(":", values);
    }
}
