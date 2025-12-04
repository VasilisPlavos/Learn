import { Router } from "express";
import { jokesRouter } from "./jokes";
import { Environment } from "../data/databases";

export function createApiRouter(env: Environment = 'dev'): Router {
    const router = Router();
    router.use('/jokes', jokesRouter(env));
    return router;
}
