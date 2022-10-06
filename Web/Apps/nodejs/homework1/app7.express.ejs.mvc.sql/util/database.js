const { Sequelize } = require("sequelize");
const sequelize = new Sequelize(
  "",
  "",
  "",
  {
    host: "",
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
