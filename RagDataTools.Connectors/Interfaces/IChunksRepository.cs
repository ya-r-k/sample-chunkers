using RagDataTools.Chunkers.Models;

namespace RagDataTools.Connectors.Interfaces;

public interface IChunksRepository<TFlag, TId>
    where TFlag : notnull
    where TId : notnull
{
    Task AddAsync(TFlag[] flags, params ChunkModel[] chunks);

    Task AddAsync(string scopeId, params ChunkModel[] chunks);

    Task<IDictionary<int, TId>> GetIndexesIdsPairsByFlagAsync(TFlag flag);

    Task<IDictionary<int, TId>> GetIndexesIdsPairsByScopeIdAsync(string scopeId);

    Task RemoveFlagFromAllDataAsync(TFlag flag);

    Task RemoveScopeAsync(string scopeId);
}
