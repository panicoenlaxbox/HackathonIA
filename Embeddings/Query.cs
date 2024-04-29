// ReSharper disable All
using Azure;
using Azure.AI.OpenAI;
using Qdrant.Client;

namespace Embeddings;

public class Query
{
    public static async Task QueryAsync()
    {
        OpenAIClient embeddingsOpenAIClient = new (new Uri(Configuration.Endpoint!), new AzureKeyCredential(Configuration.ApiKey!));
        EmbeddingsOptions embeddingOptions = new()
        {
            DeploymentName = Configuration.EmbeddingsDeploymentName,
            Input = { "¿hacemos backups de bases de datos de parámetros?"  },
        };

        var queryEmbedding = await embeddingsOpenAIClient.GetEmbeddingsAsync(embeddingOptions);
        var qdrantClient = new QdrantClient(Configuration.QdrantHost!,Configuration.QdrantPort);
        var queryResult = await qdrantClient.SearchAsync(
            Configuration.CollectionName,
            queryEmbedding.Value.Data[0].Embedding.ToArray(),
            limit: 5);

        var chatClient = new OpenAIClient(new Uri(Configuration.Endpoint!), new AzureKeyCredential(Configuration.ApiKey!));

        var prompt = @$"
Un usuario ha preguntado ¿hacemos backups de bases de datos de parámetros?.

Respondele usando la siguiente referencia, sin inventarte información:

{queryResult.First().Payload}
";

        var chatCompletionsOptions = new ChatCompletionsOptions
        {
            DeploymentName = "gpt-35t",
            Messages = { new ChatRequestUserMessage(prompt) },
        };
        Response<ChatCompletions> chatResponse = await chatClient.GetChatCompletionsAsync(chatCompletionsOptions);
        ChatChoice responseChoice = chatResponse.Value.Choices[0];
        Console.WriteLine(responseChoice.Message.Content);
    }
}