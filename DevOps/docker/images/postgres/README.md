Source: https://node-postgres.com/

1. run:
```
npm install pg
npm install --save-dev @types/pg
```

2. run a postgres server with docker:

```
docker run -d --name node-postgres-demo -e POSTGRES_PASSWORD=postgres -e POSTGRES_USER=postgres -e POSTGRES_DB=node-postgres-demo -p 5432:5432 postgres
```

3. Connecting database to Node.js server
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