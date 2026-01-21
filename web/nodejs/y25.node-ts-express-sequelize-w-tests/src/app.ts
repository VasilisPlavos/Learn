import express from 'express';
import cors from "cors";
import { createApiRouter } from './routes';
import { errorHandler } from './middlewares/errors';
import { Environment } from './data/databases';

const app = express();

app.use(express.json());
app.use(express.urlencoded({ extended: true }));
app.use(cors({
    // origin: [
    //     'https://your-frontend-domain.com'
    // ]
}));

app.get('/health', (_req, res) => {
    res.status(200).json({ status: 'ok', timestamp: new Date().toISOString() });
});

const environments: Environment[] = ['dev', 'prod']//, 'test']
for (const env of environments) {
    const apiPath = env === 'prod' ? '/api/v1' : `/api-${env}/v1`;
    app.use(apiPath, createApiRouter(env));
}

app.use(errorHandler);

export default app;