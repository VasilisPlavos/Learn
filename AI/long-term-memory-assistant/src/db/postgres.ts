import { Pool } from 'pg';
import config from '../config';

const pool = new Pool({
    user: config.postgres.user,
    host: config.postgres.host,
    database: config.postgres.database,
    password: config.postgres.password,
    port: config.postgres.port,
    max: 20, // Max number of clients in the pool
    idleTimeoutMillis: 30000, // How long a client is allowed to remain idle before being closed
    connectionTimeoutMillis: 2000, // How long to wait for a connection from the pool
});

pool.on('connect', () => {
    console.log('Connected to PostgreSQL database');
});

pool.on('error', (err, client) => {
    console.error('Unexpected error on idle PostgreSQL client', err);
    process.exit(-1); // Exit if the pool encounters a critical error
});

// Function to test the connection
export const testDbConnection = async () => {
    try {
        await pool.query('SELECT NOW()');
        console.log('PostgreSQL connection test successful.');
    } catch (error) {
        console.error('Error connecting to PostgreSQL:', error);
        throw error; // Re-throw error to be handled by the caller
    }
};

// Example function to create a table (run once during setup)
export const setupUserFactsTable = async () => {
    const client = await pool.connect();
    try {
        await client.query(`
            CREATE TABLE IF NOT EXISTS user_facts (
                id SERIAL PRIMARY KEY,
                user_id VARCHAR(255) NOT NULL, -- Identifier for the user
                fact_key VARCHAR(255) NOT NULL, -- e.g., 'name', 'preference_color'
                fact_value TEXT NOT NULL,
                created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
                updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
                UNIQUE(user_id, fact_key) -- Ensure each user has only one value per key
            );
        `);
        console.log('User facts table checked/created successfully.');
    } catch (error) {
        console.error('Error setting up user_facts table:', error);
    } finally {
        client.release();
    }
};

// Example function to save/update a user fact
export const saveUserFact = async (userId: string, key: string, value: string) => {
    const query = `
        INSERT INTO user_facts (user_id, fact_key, fact_value)
        VALUES ($1, $2, $3)
        ON CONFLICT (user_id, fact_key)
        DO UPDATE SET fact_value = EXCLUDED.fact_value, updated_at = CURRENT_TIMESTAMP;
    `;
    try {
        await pool.query(query, [userId, key, value]);
        console.log(`Fact '${key}' for user '${userId}' saved/updated.`);
    } catch (error) {
        console.error(`Error saving fact for user ${userId}:`, error);
        throw error;
    }
};

// Example function to retrieve user facts
export const getUserFacts = async (userId: string): Promise<{ [key: string]: string }> => {
    const query = `SELECT fact_key, fact_value FROM user_facts WHERE user_id = $1;`;
    try {
        const result = await pool.query(query, [userId]);
        const facts: { [key: string]: string } = {};
        result.rows.forEach(row => {
            facts[row.fact_key] = row.fact_value;
        });
        return facts;
    } catch (error) {
        console.error(`Error retrieving facts for user ${userId}:`, error);
        throw error;
    }
};


export default pool;
