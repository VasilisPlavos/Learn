import app from './app';
import { initDatabases } from './data/databases';

const PORT = process.env.PORT || 3001;

const startServer = async (): Promise<void> => {
    try {
        await initDatabases();
        app.listen(PORT, () => {
            console.log(`🚀 Server running at http://localhost:${PORT}`);
            console.log(`📚 API endpoints:`);
            console.log(`   - Production: http://localhost:${PORT}/api/v1/jokes`);
            console.log(`   - Development: http://localhost:${PORT}/api-dev/v1/jokes`);
            console.log(`   - Health check: http://localhost:${PORT}/health`);
        });

    } catch (error) {
        console.error('Failed to start server:', error);
        process.exit(1);
    }
};

startServer();
