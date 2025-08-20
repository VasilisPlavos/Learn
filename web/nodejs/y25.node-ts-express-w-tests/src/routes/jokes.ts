import axios from 'axios';
import { Request, Response, Router } from "express";

const router = Router();

router.get('/:id', async (req: Request, res: Response) => {

    const { id } = req.params;
    if (id === '2') return res.status(200).json({ result: 2 });

    try {
        const response = await axios.get('https://api.chucknorris.io/jokes/random');
        return res.status(200).json(response.data);
    } catch (error) {
        return res.status(500).json({ error: 'Error fetching joke from external API' });
    }
});

export default router;