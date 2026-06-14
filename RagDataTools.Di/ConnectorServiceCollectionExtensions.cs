using Microsoft.Extensions.DependencyInjection;
using RagDataTools.Connectors.Neo4j.Repositories;
using RagDataTools.Connectors.Qdrant.Repositories;
using RagDataTools.KeywordExtraction;

namespace RagDataTools.Di;

/// <summary>
/// DI helpers for connector repositories.
/// </summary>
public static class ConnectorServiceCollectionExtensions
{
    /// <summary>
    /// Registers connector repositories used for chunk persistence.
    /// </summary>
    public static IServiceCollection AddRagDataToolsConnectors(this IServiceCollection services)
    {
        services.AddScoped<Neo4jChunksRepository>();
        services.AddScoped<QdrantChunksRepository>();
        return services;
    }

    /// <summary>
    /// Registers keyword extraction services and the default heuristic backend.
    /// </summary>
    public static IServiceCollection AddRagDataToolsKeywordExtraction(this IServiceCollection services)
    {
        services.AddSingleton<IKeywordExtractionBackend, HeuristicKeywordExtractionBackend>();
        services.AddSingleton<IKeywordExtractor, KeywordExtractionService>();
        return services;
    }
}
