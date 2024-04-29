import json
import os

from dotenv import load_dotenv
from openai import AzureOpenAI
from qdrant_client import QdrantClient

load_dotenv()

AZURE_OPENAI_VERSION = "2024-02-01"
AZURE_OPENAI_KEY = os.getenv("AZURE_OPENAI_KEY")
AZURE_OPENAI_ENDPOINT = "https://aoi-23042024.openai.azure.com/"
QDRANT_HOST = "127.0.0.1"
QDRANT_PORT = 6333
QDRANT_COLLECTION = "collection-1"
EMBEDDING_MODEL = "text-embedding-ada-002-2"
MODEL = "gpt-35-turbo-1106"
SCORE = 0.8

def create_azure_openai_client() -> AzureOpenAI:
    return AzureOpenAI(
        api_version=AZURE_OPENAI_VERSION,
        api_key=AZURE_OPENAI_KEY,
        azure_endpoint=AZURE_OPENAI_ENDPOINT)


def create_qdrant_client() -> QdrantClient:
    return QdrantClient(host=QDRANT_HOST, port=QDRANT_PORT)


def get_system_messages() -> list:
    return [
        {
            "role": "system",
            "content": """
You are an IT documentation assistant.
Be concise when responding and provide also the name of the reference document that you have used.
We want to include a url to the reference document in the response.
In the user prompt, the relevant information to generate your response will already be included
If you don't have the information in the prompt, respond that you don't know"""
        }
    ]


def create_embedding(client: AzureOpenAI, prompt: str) -> list[float]:
    response = client.embeddings.create(
        input=prompt,
        model=EMBEDDING_MODEL
    )
    return response.data[0].embedding


def get_knowledge(azure_openai_client: AzureOpenAI, qdrant_client: QdrantClient, prompt: str) -> str | None:
    hits = qdrant_client.search(
        collection_name=QDRANT_COLLECTION,
        query_vector=create_embedding(azure_openai_client, prompt),
        limit=2)
    relevant_hits = [hit for hit in hits if hit.score > SCORE]
    if any(relevant_hits):
        return ("# This is the information to answer the question:\n" +
                "\n\n".join(
                    f"Reference: {hit.payload["reference"]}\nContent: {hit.payload["original"]}" for hit in
                    relevant_hits) + "\nRemember that you can only answer with the information that it's present in this prompt")


def get_url_reference(reference: str) -> str:
    return "https://docs.analyticalways.com/" + reference.rstrip(".md")


def main():    
    azure_openai_client = create_azure_openai_client()
    qdrant_client = create_qdrant_client()

    messages = get_system_messages()
    prompt = input("USER > ")

    while prompt != "exit":
        knowledge = get_knowledge(azure_openai_client, qdrant_client, prompt)

        if knowledge:
            prompt += "\n" + knowledge

        messages.append({
            "role": "user",
            "content": prompt
        })

        tools = [
            {
                "type": "function",
                "function": {
                    "name": "get_url_reference",
                    "description": "Returns the url associated with a markdown file, which has a .md extension",
                    "parameters": {
                        "type": "object",
                        "properties": {
                            "reference": {
                                "type": "string",
                                "description": "Markdown file name",
                            }
                        },
                        "required": ["reference"],
                    },
                },
            }
        ]

        response = azure_openai_client.chat.completions.create(
            model=MODEL,
            messages=messages,
            tools=tools,
            tool_choice="auto"
        )

        while response.choices[0].finish_reason != "stop":

            tool_calls = response.choices[0].message.tool_calls

            if tool_calls:
                available_functions = {
                    "get_url_reference": get_url_reference
                }

                messages.append(response.choices[0].message)

                for tool_call in tool_calls:
                    function_name = tool_call.function.name
                    function_to_call = available_functions[function_name]
                    function_args = json.loads(tool_call.function.arguments)
                    function_response = function_to_call(
                        reference=function_args.get("reference")
                    )
                    messages.append(
                        {
                            "tool_call_id": tool_call.id,
                            "role": "tool",
                            "name": function_name,
                            "content": function_response,
                        }
                    )

                response = azure_openai_client.chat.completions.create(
                    model=MODEL,
                    messages=messages,
                    tools=tools,
                    tool_choice="auto"
                )

        print("\nAI > ", response.choices[0].message.content)
        prompt = input("\nUSER > ")    


if __name__ == '__main__':
    main()
