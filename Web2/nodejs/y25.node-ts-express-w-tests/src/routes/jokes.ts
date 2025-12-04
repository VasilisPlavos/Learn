import axios from 'axios';
import { NextFunction, Request, Response, Router } from "express";

const router = Router();

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
        return res.status(200).json(response.data);
    } catch (error) {
        next(error);
        return res.status(500).json({ error: 'Error fetching joke from external API' });
    }
});

export default router;