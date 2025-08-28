import { NextFunction, Request, Response } from "express";

export const errorHandler = (err: Error, req: Request, res: Response, next: NextFunction) => {
  res.status(500).json({ error: 'An internal server error occurred' });
  console.error(err);
};