import { DataTypes, Model, ModelAttributes } from "sequelize";

interface JokeAttributes {
    id?: number;
    value: string;
}

interface JokeModel extends Model<JokeAttributes>, JokeAttributes {}

export const JokeDef: ModelAttributes<JokeModel, JokeAttributes> = {
    id: {
        type: DataTypes.BIGINT.UNSIGNED,
        autoIncrement: true,
        primaryKey: true,
    },
    value: { type: DataTypes.TEXT, allowNull: true }
}