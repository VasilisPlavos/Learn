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
  id: {
    type: DataTypes.INTEGER,
    autoIncrement: true,
    allowNull: false,
    primaryKey: true,
  },
  username: DataTypes.STRING,
  passwordHashed: DataTypes.STRING,
});

const Book = sequelize.define("Book", {
  id: {
    type: DataTypes.INTEGER,
    autoIncrement: true,
    allowNull: false,
    primaryKey: true,
  },
  title: DataTypes.STRING,
  author: DataTypes.STRING,
});

const UserBooks = sequelize.define("UserBooks", {
  id: {
    type: DataTypes.INTEGER,
    autoIncrement: true,
    allowNull: false,
    primaryKey: true,
  },
  userId: { type: DataTypes.INTEGER },
  bookId: { type: DataTypes.INTEGER },
  userComment: { type: DataTypes.STRING },
});

await sequelize.sync({ alter: true });

// await User.findOrCreate({ where: { username: "janedoe" } });
// const users = await User.findAll();
// console.log(users);

export { User, Book, UserBooks };
