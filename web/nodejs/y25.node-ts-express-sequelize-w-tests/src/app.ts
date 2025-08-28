
import express from 'express';
import cors from "cors";

// import routes
import { errorHandler } from './middlewares/errors';
import { BaseRouter, DevRouter, TestRouter } from './routes';

const app = express();

app.use(express.json());
app.use(cors({
    // origin: [
    //     'https://your-frontend-domain.com'
    // ]
}));

app.use('/api/v1', BaseRouter);
app.use('/api-dev/v1', DevRouter);
app.use('/api-test/v1', TestRouter);

// Error handling
app.use(errorHandler);


export default app;