import express, { Request, Response, NextFunction } from "express";

export const healthRouter = express.Router();

healthRouter.get("/", async (req: Request, res: Response, next: NextFunction): Promise<void> => {
    res.status(200).json({ status: 'working' });
    return;
})