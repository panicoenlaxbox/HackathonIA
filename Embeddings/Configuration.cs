using Microsoft.Extensions.Configuration;

namespace Embeddings;

public static class Configuration
{
    public static void Load()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddUserSecrets(assembly: typeof(Configuration).Assembly)
            .Build();

        Endpoint = configuration["openAIEndpoint"]!;
        ApiKey = configuration["openAIApiKey"]!;
        EmbeddingsDeploymentName = configuration["embeddingsDeploymentName"]!;
        QdrantHost = configuration["qdrantHost"]!;
        var qdrantPort = configuration["qdrantPort"];
        QdrantPort = int.Parse(qdrantPort!);
    }

    public const string CollectionName = "collection-1";

    public static int QdrantPort { get; set; }

    public static string? QdrantHost { get; set; }

    public static string? EmbeddingsDeploymentName { get; set; }

    public static string? ApiKey { get; set; }

    public static string? Endpoint { get; set; }
}