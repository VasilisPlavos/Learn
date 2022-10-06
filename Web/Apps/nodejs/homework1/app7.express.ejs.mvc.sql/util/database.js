const mysql = require("mysql2");

const pool = mysql.createPool({
  host: "",
  database: "",
  user: "",
  password: "",
});

module.exports = pool.promise();
