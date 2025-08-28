import { Sequelize } from "sequelize";
import { JokeDef } from "../entities/Jokes";

const devDbSequelize = new Sequelize('sqlite::memory:');
const devDb = { Joke: devDbSequelize.define('joke', JokeDef), sequelize: devDbSequelize }

const prodDbSequelize = new Sequelize('sqlite::memory:');
const prodDb = { Joke: prodDbSequelize.define('joke', JokeDef), sequelize: prodDbSequelize }

const db = { prod: prodDb, dev: devDb };

const initDatabases = async () => {
    const environments = ['dev', 'prod'] as const;
    for (const env of environments) {
        try {
            await db[env].sequelize.authenticate();
            console.log(`✅ ${env} database connected successfully.`);
            await db[env].sequelize.sync({ force: true });
            console.log(`🔄 ${env} database synced successfully.`);
        } catch (error) {
            console.error(`❌ An error occurred with the ${env} database:`, error);
        }
    }
}

export { db, initDatabases }