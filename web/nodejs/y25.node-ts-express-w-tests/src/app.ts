
import express from 'express';
import cors from "cors";

// import routes
import jokesRouter from './routes/jokes';
import { errorHandler } from './middlewares/errors';

const app = express();

app.use(express.json());
app.use(cors({
    // origin: [
    //     'https://your-frontend-domain.com'
    // ]
}));

// Routes
app.use('/api/v1/jokes', jokesRouter);

// Error handling
app.use(errorHandler);


export default app;