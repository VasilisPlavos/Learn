import dotenv from 'dotenv';
import path from 'path';

// Load environment variables from .env file
dotenv.config({ path: path.resolve(__dirname, '../../.env') });

interface Config {
    postgres: {
        user?: string;
        password?: string;
        database?: string;
        host?: string;
        port: number;
    };
    qdrant: {
        url?: string;
        apiKey?: string;
        collectionName: string;
    };
    gemini: {
        apiKey?: string;
        embeddingModel: string;
        generationModel: string;
    };
    server: {
        port: number;
    };
}

const config: Config = {
    postgres: {
        user: process.env.POSTGRES_USER,
        password: process.env.POSTGRES_PASSWORD,
        database: process.env.POSTGRES_DB,
        host: process.env.POSTGRES_HOST,
        port: parseInt(process.env.POSTGRES_PORT || '5432', 10),
    },
    qdrant: {
        url: process.env.QDRANT_URL,
        apiKey: process.env.QDRANT_API_KEY, // Handle optional API key
        collectionName: process.env.QDRANT_COLLECTION_NAME || 'chat_history',
    },
    gemini: {
        apiKey: process.env.GEMINI_API_KEY,
        embeddingModel: process.env.GEMINI_EMBEDDING_MODEL || 'models/embedding-001',
        generationModel: process.env.GEMINI_GENERATION_MODEL || 'models/gemini-1.5-pro-latest',
    },
    server: {
        port: parseInt(process.env.PORT || '3000', 10),
    },
};

// Validate essential configuration
if (!config.postgres.user || !config.postgres.password || !config.postgres.database) {
    console.error('Missing essential PostgreSQL configuration in .env file');
    process.exit(1);
}

if (!config.qdrant.url) {
    console.error('Missing QDRANT_URL configuration in .env file');
    process.exit(1);
}

if (!config.gemini.apiKey) {
    console.error('Missing GEMINI_API_KEY configuration in .env file');
    process.exit(1);
}


export default config;
