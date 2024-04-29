# HackathonIA

## Pre-requisites

### Qdrant

For local development we are using Qdrant as a vector database.

`docker run -p 6333:6333 -p 6334:6334 qdrant/qdrant`

You can access to the dashboard, navigating to: http://localhost:6333/dashboard

## Embeddings

The project do following steps:

- Read several markdowns containing documentation about backups
- Create embeddings for those markdowns
- Insert the embeddings into a Qdrant database

## Chat

With this project you can ask a question and the system will respond with the most similar question in the database that has been created with the embeddings project.

### Run locally

`python .\main.py`

#### Example

```
¿Dónde puedo encontrar un backup de la base de datos de parámetros?
```