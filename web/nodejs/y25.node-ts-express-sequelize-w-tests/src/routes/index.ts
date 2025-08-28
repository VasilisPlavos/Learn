import { Router } from "express";
import jokesRouter from "./jokes";

function createApiRouter(): Router {
    const router = Router();
    router.use('/jokes', jokesRouter);
    return router;
}

export const BaseRouter = createApiRouter();
export const DevRouter = createApiRouter();
export const TestRouter = createApiRouter();