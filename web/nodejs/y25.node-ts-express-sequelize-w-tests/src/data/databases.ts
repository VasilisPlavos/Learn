import { Sequelize } from 'sequelize';
import { JokeDef } from "../entities/Jokes";

const envs = ['dev', 'prod'] as const;
export type Environment = (typeof envs)[number];

const createDatabase = () => {
    const sequelize = new Sequelize('sqlite::memory:');
    const Joke = sequelize.define('joke', JokeDef);
    return { sequelize, Joke };
};

const db = { prod: createDatabase(), dev: createDatabase() }

const initDatabases = async (): Promise<void> => {
    for (const env of Object.keys(db) as Environment[]) {
        try {
            const { sequelize } = db[env];
            await sequelize.authenticate();
            console.log(`✅ ${env} database connected successfully.`);
            if (env === 'prod') continue;
            // await sequelize.sync({ force: true });
            // console.log(`🔄 ${env} database synced successfully.`);
        } catch (error) {
            console.error(`❌ An error occurred with the ${env} database:`, error);
        }
    }
};

export { db, initDatabases };