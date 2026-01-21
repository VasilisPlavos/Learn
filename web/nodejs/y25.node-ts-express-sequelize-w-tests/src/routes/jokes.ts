import axios, { AxiosError } from 'axios';
import { NextFunction, Request, Response, Router } from 'express';

import { db, Environment } from '../data/databases';

const CHUCK_NORRIS_API = 'https://api.chucknorris.io/jokes/random';
interface CreateJokeBody { value?: string; }
interface ChuckNorrisJoke {
    value: string;
    id: string;
    url: string;
}

export function jokesRouter(env: Environment): Router {
    const router = Router();

    router.get('/', async (req: Request, res: Response, next: NextFunction) => {
        try {
            const jokes = await db[env].Joke.findAll({ order: [['created_at', 'DESC']] });
            return res.status(200).json(jokes);
        } catch (error) {
            const message = error instanceof Error ? error.message : 'Unknown error';
            next(new Error(`Failed to fetch joke: ${message}`));
        }
    });

    router.get('/:id', async (req: Request, res: Response, next: NextFunction) => {
        const id = req.params.id as string;
        if (id === '2') return res.status(200).json({ result: 2 });

        if (id === '3') {
            try {
                throw new Error("eeee");
            } catch (error) {
                return next(error);
            }
        }

        const numericId = parseInt(id, 10);
        if (isNaN(numericId) || numericId < 1) {
            throw new Error('Invalid joke ID: must be a positive integer');
        }

        try {
            const response = await axios.get<ChuckNorrisJoke>(CHUCK_NORRIS_API);
            const joke = await db[env].Joke.create({ value: response.data.value });
            return res.status(200).json(joke);
        } catch (error) {
            if (error instanceof Error && error.name === 'SequelizeValidationError') {
                return next(error);
            }
            const message = error instanceof Error ? error.message : 'Unknown error';
            return next(new Error(`Failed to create joke: ${message}`));
        }
    });

    router.post('/', async (req: Request, res: Response, next: NextFunction) => {
        try {
            let value = (req.body as CreateJokeBody).value;
            if (value === undefined) {
                const response = await axios.get<ChuckNorrisJoke>(CHUCK_NORRIS_API);
                value = response.data.value;
            }

            value = value.trim();
            const joke = await db[env].Joke.create({ value })
            res.status(202).json(joke);
        } catch (error) {
            if (error instanceof AxiosError) {
                throw new Error('Error fetching joke from external API');
            }
            return next(error);
        }
    })

    router.delete('/:id', async (req: Request, res: Response, next: NextFunction) => {
        const id = req.params.id as string;
        const numericId = parseInt(id, 10);

        if (isNaN(numericId) || numericId < 1) {
            throw new Error('Invalid joke ID: must be a positive integer');
        }

        const joke = await db[env].Joke.findByPk(id);
        try {
            await joke?.destroy();
        } catch (error) {
            const message = error instanceof Error ? error.message : 'Unknown error';
            throw new Error(`Failed to delete joke: ${message}`);
        }
    })

    return router;
}
