import axios from 'axios';
import { NextFunction, Request, Response, Router } from "express";

import { db } from '../data/databases';

const router = Router();

router.get('/', async (req: Request, res: Response, next: NextFunction) => {
    const jokesList = await db.dev.Joke.findAll();
    return res.status(200).json(jokesList);
})

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
        const joke = await db.dev.Joke.create({ value: response.data.value })
        return res.status(200).json(joke);
    } catch (error) {
        next(error);
        return res.status(500).json({ error: 'Error fetching joke from external API' });
    }
});

export default router;