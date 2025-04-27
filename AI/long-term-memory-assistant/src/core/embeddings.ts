import { GoogleGenerativeAI, TaskType } from '@google/generative-ai';
import config from '../config';

// Initialize the Google Generative AI client
const genAI = new GoogleGenerativeAI(config.gemini.apiKey!); // Non-null assertion as we validate apiKey in config

const embeddingModel = genAI.getGenerativeModel({ model: config.gemini.embeddingModel });

/**
 * Generates an embedding for the given text using the configured Gemini model.
 * @param text The text to embed.
 * @returns A promise that resolves to an array of numbers representing the embedding.
 * @throws Throws an error if the embedding generation fails.
 */
export const getEmbedding = async (text: string): Promise<number[]> => {
    try {
        // Gemini API might require specifying the task type for embeddings
        const result = await embeddingModel.embedContent(
            {
                content: { parts: [{ text }], role: "user"}, // Structure expected by embedContent
                taskType: TaskType.RETRIEVAL_DOCUMENT // Or RETRIEVAL_QUERY, SEMANTIC_SIMILARITY depending on use case
                // Choose the task type that best fits how you'll use the embeddings.
                // RETRIEVAL_DOCUMENT is often suitable for storing documents/chat history.
            }
        );
        const embedding = result.embedding;
        if (!embedding || !embedding.values) {
            throw new Error('Failed to generate embedding: No embedding values returned.');
        }
        return embedding.values;
    } catch (error) {
        console.error('Error generating embedding:', error);
        // Consider more specific error handling or logging
        throw new Error(`Failed to generate embedding: ${error instanceof Error ? error.message : String(error)}`);
    }
};

// Example usage (optional, for testing)
// async function testEmbedding() {
//     try {
//         const embedding = await getEmbedding("Hello, world!");
//         console.log("Generated embedding:", embedding.slice(0, 10), "..."); // Log first 10 values
//         console.log("Embedding dimension:", embedding.length);
//     } catch (error) {
//         console.error("Embedding test failed:", error);
//     }
// }
// testEmbedding(); // Uncomment to run test if needed
