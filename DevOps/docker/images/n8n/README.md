# n8n setup

docker volume create n8n_data

docker run -it --name n8n -p 5678:5678 -e DB_TYPE=postgresdb -e DB_POSTGRESDB_DATABASE=<POSTGRES_DATABASE> -e DB_POSTGRESDB_HOST=<POSTGRES_HOST> -e DB_POSTGRESDB_PORT=<POSTGRES_PORT> -e DB_POSTGRESDB_USER=<POSTGRES_USER> -e DB_POSTGRESDB_PASSWORD=<POSTGRES_PASSWORD> -e N8N_DEFAULT_BINARY_DATA_MODE=filesystem -v .\n8n_shared:/data/n8n_shared -v n8n_data:/home/node/.n8n --restart always docker.n8n.io/n8nio/n8n

## to update

```cmd
docker pull docker.n8n.io/n8nio/n8n

docker ps -a

# Stop the container with the `<container_id>`
docker stop <container_id>

# Remove the container with the `<container_id>`
docker rm <container_id>

docker run ...
```
