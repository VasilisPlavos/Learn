import { Sequelize } from 'sequelize';
import { JokeDef } from "../entities/Jokes";

const envs = ['dev', 'prod'] as const;
export type Environment = typeof envs[number];

const createDatabase = (env: Environment) => {
    const sequelize = new Sequelize('sqlite::memory:');
    return {
        Joke: sequelize.define('joke', JokeDef),
        sequelize: sequelize
    }
}

const db = { prod: createDatabase('prod'), dev: createDatabase("dev") } as const;

const initDatabases = async () => {
    for (const env of Object.keys(db) as Environment[]) {
        try {
            await db[env].sequelize.authenticate();
            console.log(`✅ ${env} database connected successfully.`);
            
            if (env === 'prod') continue;
            // await db[env].sequelize.sync({ force: true });
            // console.log(`🔄 ${env} database synced successfully.`);

        } catch (error) {
            console.error(`❌ An error occurred with the ${env} database:`, error);
        }
    }
}

export { db, initDatabases }