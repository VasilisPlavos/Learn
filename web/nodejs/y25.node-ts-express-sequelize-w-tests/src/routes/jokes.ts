import axios from "axios";
import { NextFunction, Request, Response, Router } from "express";

import { db, Environment } from "../data/databases";

export function jokesRouter(env: Environment) {

    const router = Router();

    router.get('/', async (req: Request, res: Response, next: NextFunction) => {
        try {
            const jokesList = await db[env].Joke.findAll();
            return res.status(200).json(jokesList);
        } catch (error) {
            next(error);
        }
    });

    router.get('/:id', async (req: Request, res: Response, next: NextFunction) => {

        const { id } = req.params;
        if (id === '2') return res.status(200).json({ result: 2 });

        if (id === '3') {
            try {
                throw new Error("eeee");
            } catch (error) {
                return next(error);
            }
        }

        try {
            const response = await axios.get('https://api.chucknorris.io/jokes/random');
            const joke = await db[env].Joke.create({ value: response.data.value });
            return res.status(200).json(joke);
        } catch (error) {
            return next(error);
        }
    });

    return router;
}
