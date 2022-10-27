import { DataTypes, Sequelize, where } from "sequelize";

const sequelize = new Sequelize({
  host: "localhost",
  dialect: "sqlite",
  logging: false,
  storage: "./data/books.sqlite",
  define: {
    timestamps: false,
  },
});

const User = sequelize.define("User", {
  id: { type: DataTypes.INTEGER, primaryKey: true },
  username: DataTypes.STRING,
});

await sequelize.sync({ alter: true });
await User.findOrCreate({ where: { username: "janedoe" } });
const users = await User.findAll();
console.log(users);
