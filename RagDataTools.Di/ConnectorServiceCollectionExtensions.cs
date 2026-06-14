using Microsoft.Extensions.DependencyInjection;
using RagDataTools.Connectors.Neo4j.Repositories;
using RagDataTools.Connectors.Qdrant.Repositories;

namespace RagDataTools.Di;

/// <summary>
/// DI helpers for connector repositories.
/// </summary>
public static class ConnectorServiceCollectionExtensions
{
    public static IServiceCollection AddRagDataToolsConnectors(this IServiceCollection services)
    {
        services.AddScoped<Neo4jChunksRepository>();
        services.AddScoped<QdrantChunksRepository>();
        return services;
    }
}
