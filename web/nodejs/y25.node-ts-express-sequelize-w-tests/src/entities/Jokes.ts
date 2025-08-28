import { DataTypes } from "sequelize";

export const JokeDef = {
    id: {
        type: DataTypes.BIGINT.UNSIGNED,
        allowNull: false,
        autoIncrement: true,
        primaryKey: true,
    },
    value: { type: DataTypes.TEXT, allowNull: true }
}