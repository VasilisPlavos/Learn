import { Router, Request, Response } from 'express';
import { generateAssistantResponse } from '../services/gemini';
import { saveUserFact } from '../db/postgres'; // For potentially saving facts via API

const assistantRoutes = Router();

// Interface for the expected request body
interface AssistantRequestBody {
    userId: string;
    query: string;
}

// POST /assistant/query
assistantRoutes.post('/query', async (req: Request<{}, {}, AssistantRequestBody>, res: Response) => {
    const { userId, query } = req.body;

    // Basic validation
    if (!userId || !query) {
        res.status(400).json({ error: 'Missing userId or query in request body' });
        return; // Exit the function
    }

    try {
        console.log(`Received query from user ${userId}: "${query}"`);
        const responseText = await generateAssistantResponse(userId, query);
        res.status(200).json({ response: responseText });
    } catch (error) {
        console.error(`Error handling /assistant/query for user ${userId}:`, error);
        res.status(500).json({ error: 'Internal server error while processing your query.' });
    }
});

// Example route to manually add/update a user fact (optional)
interface FactRequestBody {
    userId: string;
    key: string;
    value: string;
}
assistantRoutes.post('/fact', async (req: Request<{}, {}, FactRequestBody>, res: Response) => {
    const { userId, key, value } = req.body;

    if (!userId || !key || value === undefined) {
        res.status(400).json({ error: 'Missing userId, key, or value in request body' });
        return; // Exit the function
    }

    try {
        await saveUserFact(userId, key, value);
        res.status(200).json({ message: `Fact '${key}' for user '${userId}' saved successfully.` });
    } catch (error) {
        console.error(`Error handling /assistant/fact for user ${userId}:`, error);
        res.status(500).json({ error: 'Internal server error while saving fact.' });
    }
});


export default assistantRoutes;
