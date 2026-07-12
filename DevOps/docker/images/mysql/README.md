# MySQL server and phpMyAdmin and Nodejs installation

Source: <https://hub.docker.com/_/mysql>

1. run:

```console
npm install mysql2
npm install --save-dev @types/mysql
```

1. run a mysql server with docker:

```console
docker run -d --name mysql-server -e MYSQL_ROOT_PASSWORD="rootpass" -e MYSQL_USER="user" -e MYSQL_PASSWORD="pass" -e MYSQL_DATABASE=mydb -p 3306:3306 -v mysql-data:/var/lib/mysql --restart always mysql
```

1. run phpMyAdmin via docker:

```console
docker run --name phpmyadmin -p 8080:80 -e PMA_HOST="host.docker.internal" -e PMA_PORT=3306 --restart always -d phpmyadmin
```

- Note: Host name/address: `host.docker.internal` (on macOS/Windows) or your host machine's IP address (on Linux; often `172.17.0.1`). When running via the compose file both containers share a network, so `PMA_HOST` is simply the service name `mysql-server`.
- phpMyAdmin is available at <http://localhost:8080>
- To Update phpMyAdmin: delete it, then run `docker pull phpmyadmin` and repeat step 3

1. Connecting database to Node.js server

```console
import mysql from "mysql2/promise";
import { env } from "./env";

const pool = mysql.createPool({
  host: env.MYSQL_HOST,
  user: env.MYSQL_USER,
  password: env.MYSQL_PASSWORD,
  database: env.MYSQL_DATABASE,
  port: env.MYSQL_PORT,
  waitForConnections: true,
  connectionLimit: 10,
});

export default pool;
```

OR

```console
import mysql from "mysql2/promise";

const pool = mysql.createPool({
  host: "localhost",
  user: "user",
  password: "pass",
  database: "mydb",
  port: 3306,
  waitForConnections: true,
  connectionLimit: 10,
});

export default pool;
```
