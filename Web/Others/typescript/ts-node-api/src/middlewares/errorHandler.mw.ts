import { Request, Response, NextFunction } from "express";

export function errorHandler(error: any, req: Request, res: Response, next: NextFunction) {
    console.error(error.stack); // Logging the error

    if (error instanceof Error) {
        res.status(500).send(error.message);
    }
}