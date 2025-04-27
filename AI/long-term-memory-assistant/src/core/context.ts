import { getUserFacts } from '../db/postgres';
import { searchRelevantChatHistory } from '../db/qdrant';
import { getEmbedding } from './embeddings';

interface RetrievedContext {
    userFacts: { [key: string]: string };
    relevantHistory: string[];
}

/**
 * Retrieves relevant context for a given user and query.
 * Fetches user-specific facts from PostgreSQL and semantically similar chat history from Qdrant.
 * @param userId The ID of the user making the query.
 * @param currentQuery The current message or query from the user.
 * @param historyLimit The maximum number of relevant history messages to retrieve.
 * @returns A promise that resolves to an object containing user facts and relevant history.
 * @throws Throws an error if context retrieval fails.
 */
export const retrieveContext = async (
    userId: string,
    currentQuery: string,
    historyLimit: number = 5
): Promise<RetrievedContext> => {
    console.log(`Retrieving context for user ${userId} based on query: "${currentQuery}"`);
    try {
        // 1. Fetch user facts from PostgreSQL
        const userFactsPromise = getUserFacts(userId);

        // 2. Generate embedding for the current query
        const queryEmbeddingPromise = getEmbedding(currentQuery); // Use appropriate task type if needed

        // Wait for both promises concurrently
        const [userFacts, queryEmbedding] = await Promise.all([userFactsPromise, queryEmbeddingPromise]);

        // 3. Search for relevant chat history in Qdrant using the query embedding
        const relevantHistory = await searchRelevantChatHistory(userId, queryEmbedding, historyLimit);

        console.log(`Retrieved ${Object.keys(userFacts).length} user facts and ${relevantHistory.length} relevant history messages.`);

        return {
            userFacts,
            relevantHistory,
        };
    } catch (error) {
        console.error(`Error retrieving context for user ${userId}:`, error);
        // Depending on the desired behavior, you might return partial context or re-throw
        throw new Error(`Failed to retrieve context: ${error instanceof Error ? error.message : String(error)}`);
    }
};
