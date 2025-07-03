Source: https://node-postgres.com/

1. run:
```
npm install pg
npm install --save-dev @types/pg
```

2. run a postgres server with docker:

```
docker run -d --name node-postgres-demo -e POSTGRES_PASSWORD=postgres -e POSTGRES_USER=postgres -e POSTGRES_DB=node-postgres-demo -p 3532:5432 postgres
```

3. run pgadmin via docker [[source]](https://medium.com/@marvinjungre/get-postgresql-and-pgadmin-4-up-and-running-with-docker-4a8d81048aea):

```
docker run --name pgadmin-container -p 3580:80 -e 'PGADMIN_DEFAULT_EMAIL=user@mail.com' -e 'PGADMIN_DEFAULT_PASSWORD=XXXXXXXXXXXX' -d dpage/pgadmin4
```
- Note: Host name/address: `host.docker.internal` (on macOS/Windows) or your host machine's IP address (on Linux; often `172.17.0.1`)


4. Connecting database to Node.js server
```
import { Pool } from "pg";
import { env } from "./env";

const pool = new Pool({
  host: env.POSTGRES_HOST,
  user: env.POSTGRES_USER,
  password: env.POSTGRES_PASSWORD,
  database: env.POSTGRES_DB,
  port: env.POSTGRES_PORT,
  idleTimeoutMillis: 30000,
});

export default pool;
```
OR
```
import { Pool } from "pg";

const pool = new Pool({
  host: "localhost",
  user: "postgres",
  password: "postgres",
  database: "node-postgres-demo",
  port: 5432,
  idleTimeoutMillis: 30000,
});

export default pool;
```