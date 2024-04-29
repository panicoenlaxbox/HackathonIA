using Azure;
using Azure.AI.OpenAI;
using Embeddings;
using Qdrant.Client;
using Qdrant.Client.Grpc;

Configuration.Load();
await CreateCollectionAsync();
await InsertEmbeddingsAsync();
//await Query.QueryAsync();
return;

async Task CreateCollectionAsync()
{
    var qdrantClient = new QdrantClient(Configuration.QdrantHost!, Configuration.QdrantPort);
    await qdrantClient.CreateCollectionAsync(Configuration.CollectionName,
        new VectorParams { Size = 1536, Distance = Distance.Cosine });
}

async Task InsertEmbeddingsAsync()
{
    OpenAIClient openAiClient = new(new Uri(Configuration.Endpoint!), new AzureKeyCredential(Configuration.ApiKey!));
    var qdrantClient = new QdrantClient(Configuration.QdrantHost!, Configuration.QdrantPort);
    ulong id = 1;
    foreach (var (filename, content) in MarkdownReader.ReadAll())
    {
        await InsertEmbeddingAsync(id++, openAiClient, qdrantClient, content, filename);
    }
}

async Task InsertEmbeddingAsync(ulong id, OpenAIClient openAiClient, QdrantClient qdrantClient, string text, string filename)
{
    EmbeddingsOptions embeddingOptions = new()
    {
        DeploymentName = Configuration.EmbeddingsDeploymentName,
        Input = { text }
    };

    var embedding = openAiClient.GetEmbeddings(embeddingOptions);

    var vectors = embedding.Value.Data.Select(data => new PointStruct
    {
        Id = id,
        Vectors = data.Embedding.ToArray(),
        Payload = { ["original"] = text, ["reference"] = filename }
    }).ToList();
    await qdrantClient.UpsertAsync(Configuration.CollectionName, vectors);
}