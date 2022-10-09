const { Sequelize } = require("sequelize");
const sequelize = new Sequelize(
  "u567828318_node_complete",
  "u567828318_wmjn647k",
  "wMJn6&%&47KZpMgy6",
  {
    host: "45.84.205.102",
    dialect: "mysql",
  }
);

module.exports = sequelize;

// connect to database without sequelize
// const mysql = require("mysql2");

// const pool = mysql.createPool({
//   host: "",
//   database: "",
//   user: "",
//   password: "",
// });

// module.exports = pool.promise();
