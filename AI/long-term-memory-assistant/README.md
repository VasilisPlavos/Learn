# How to run

1. run a postgres server with docker:

```
docker run -d --name node-postgres-demo -e POSTGRES_PASSWORD=postgres -e POSTGRES_USER=postgres -e POSTGRES_DB=node-postgres-demo -p 5432:5432 postgres
```

2. Run the Qdrant Docker container:

```
docker run -d --name qdrant-demo -p 6333:6333 qdrant/qdrant
```

3. Run the app
```
npm install
npm run dev
```

4. Example http request
```
POST: http://localhost:3001/api/assistant/query
{
    "userId": "1",
    "query": "what is my fav col?"
}
```

