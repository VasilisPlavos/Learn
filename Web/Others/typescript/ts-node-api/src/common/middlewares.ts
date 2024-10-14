import { Request, Response, NextFunction } from "express";
import HttpException from "./http-exception";

export const errorHandler = (error: HttpException, req: Request, res: Response, next: NextFunction) => {
    console.error(error.stack);
    const status = error.statusCode || error.status || 500;
    res.status(status).send(error);
};

export const notFoundHandler = (request: Request, response: Response, next: NextFunction) => {
    const message = "Resource not found";
    response.status(404).send(message);
  };