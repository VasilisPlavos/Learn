import express, { Express, Request, Response, NextFunction } from 'express';
import config from './config';
import apiRoutes from './routes';
import { testDbConnection, setupUserFactsTable } from './db/postgres';
import { ensureChatHistoryCollection } from './db/qdrant';

const app: Express = express();
const PORT = config.server.port;

// Middleware
app.use(express.json()); // Parse JSON request bodies

// Mount API routes
app.use('/api', apiRoutes); // Prefix all routes with /api

// Basic Error Handling Middleware (example)
app.use((err: Error, req: Request, res: Response, next: NextFunction) => {
    console.error("Unhandled error:", err.stack);
    res.status(500).json({ error: 'Something went wrong!' });
});

// Function to perform initial setup checks
const initializeApp = async () => {
    try {
        console.log('Initializing application...');

        // 1. Test PostgreSQL Connection
        await testDbConnection();

        // 2. Ensure PostgreSQL 'user_facts' table exists
        await setupUserFactsTable();

        // 3. Ensure Qdrant 'chat_history' collection exists
        // This requires the embedding model to be ready, which happens implicitly
        // when getEmbedding is called within ensureChatHistoryCollection.
        await ensureChatHistoryCollection();

        console.log('Database and vector store checks passed.');

        // Start the server only after successful initialization
        app.listen(PORT, () => {
            console.log(`Server listening on port ${PORT}`);
            console.log(`API available at http://localhost:${PORT}/api`);
            console.log(`Health check: http://localhost:${PORT}/api/health`);
        });

    } catch (error) {
        console.error('Application initialization failed:', error);
        process.exit(1); // Exit if essential setup fails
    }
};

// Start the initialization process
initializeApp();

// Optional: Graceful shutdown handling
process.on('SIGINT', () => {
    console.log('SIGINT signal received: closing HTTP server');
    // Add cleanup logic here (e.g., close DB connections) if needed
    process.exit(0);
});

process.on('SIGTERM', () => {
    console.log('SIGTERM signal received: closing HTTP server');
    // Add cleanup logic here
    process.exit(0);
});
