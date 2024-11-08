# main tools for api
1. npm init -y
2. npm i express dotenv cors
4. npm i -D @types/express @types/cors ts-node typescript

# postgress

## 1. docker
1. postgres `docker run --name  postgres-db -e POSTGRES_USER=admingristle4026 -e POSTGRES_PASSWORD=pass -e POSTGRES_DB=tasks_db_prod -p 6070:5432 -d postgres`
2. pgadmin `docker run --name pgadmin -p 6071:80 -e PGADMIN_DEFAULT_EMAIL=v1@plavos.com -e PGADMIN_DEFAULT_PASSWORD=J#RW%lyT^VaeQ4V8t -d dpage/pgadmin4`


## 2. npm
* `npm install pg`
* `npm install --save-dev @types/pg typescript ts-node`