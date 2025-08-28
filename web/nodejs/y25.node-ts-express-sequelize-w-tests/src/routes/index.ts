import { Router } from "express";
import { jokesRouter } from "./jokes";
import { Environment } from "../data/databases";

function createApiRouter(env: Environment = 'dev'): Router {
    const router = Router();
    router.use('/jokes', jokesRouter(env));
    return router;
}
export const BaseRouter = createApiRouter('prod');
export const DevRouter = createApiRouter('dev');
export const TestRouter = createApiRouter();