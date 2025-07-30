docker volume create n8n_data

docker run -it --name n8n -p 3578:5678 -e DB_TYPE=postgresdb -e DB_POSTGRESDB_DATABASE=<POSTGRES_DATABASE> -e DB_POSTGRESDB_HOST=<POSTGRES_HOST> -e DB_POSTGRESDB_PORT=<POSTGRES_PORT> -e DB_POSTGRESDB_USER=<POSTGRES_USER> -e -e DB_POSTGRESDB_PASSWORD=<POSTGRES_PASSWORD> -v n8n_data:/home/node/.n8n --restart always docker.n8n.io/n8nio/n8n