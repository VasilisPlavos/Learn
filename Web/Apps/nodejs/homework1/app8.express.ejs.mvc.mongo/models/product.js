const { DataTypes } = require("sequelize");
const sequelize = require("../util/database");

const Product = sequelize.define(
  "product",
  {
    id: {
      type: DataTypes.INTEGER,
      autoIncrement: true,
      allowNull: false,
      primaryKey: true,
    },
    title: DataTypes.STRING,
    imageUrl: DataTypes.STRING,
    description: DataTypes.STRING,
    price: DataTypes.DOUBLE
  },
  { timestamps: false }
);

module.exports = Product;
