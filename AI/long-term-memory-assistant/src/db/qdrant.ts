import { QdrantClient } from "@qdrant/js-client-rest";
import config from '../config';
import { getEmbedding } from '../core/embeddings'; // We'll create this soon

// Initialize Qdrant client
const qdrantClient = new QdrantClient({
    url: config.qdrant.url,
    apiKey: config.qdrant.apiKey, // Optional API key
});

const COLLECTION_NAME = config.qdrant.collectionName;

// Function to ensure the collection exists in Qdrant
export const ensureChatHistoryCollection = async () => {
    try {
        const collections = await qdrantClient.getCollections();
        const collectionExists = collections.collections.some(c => c.name === COLLECTION_NAME);

        if (!collectionExists) {
            console.log(`Collection '${COLLECTION_NAME}' not found. Creating...`);
            // Determine vector size based on the embedding model
            // We need a dummy text to get the embedding dimension
            const dummyEmbedding = await getEmbedding('test');
            const vectorSize = dummyEmbedding.length;

            if (vectorSize === 0) {
                throw new Error('Could not determine embedding vector size.');
            }

            await qdrantClient.createCollection(COLLECTION_NAME, {
                vectors: {
                    size: vectorSize,
                    distance: 'Cosine', // Cosine similarity is common for text embeddings
                },
            });
            console.log(`Collection '${COLLECTION_NAME}' created successfully with vector size ${vectorSize}.`);
        } else {
            console.log(`Collection '${COLLECTION_NAME}' already exists.`);
        }
    } catch (error) {
        console.error('Error ensuring Qdrant collection:', error);
        throw error;
    }
};

// Function to add a chat message (with embedding) to Qdrant
export const addChatMessageToVectorDB = async (userId: string, messageId: string, text: string, embedding: number[]) => {
    try {
        await qdrantClient.upsert(COLLECTION_NAME, {
            wait: true, // Wait for the operation to complete
            points: [
                {
                    id: messageId, // Unique ID for the point (e.g., message UUID)
                    vector: embedding,
                    payload: { // Store metadata along with the vector
                        userId: userId,
                        text: text,
                        timestamp: new Date().toISOString(),
                    },
                },
            ],
        });
        console.log(`Message ${messageId} added to Qdrant for user ${userId}.`);
    } catch (error) {
        console.error(`Error adding message to Qdrant for user ${userId}:`, error);
        throw error;
    }
};

// Function to search for relevant chat history in Qdrant
export const searchRelevantChatHistory = async (userId: string, queryEmbedding: number[], limit: number = 5): Promise<string[]> => {
    try {
        const searchResult = await qdrantClient.search(COLLECTION_NAME, {
            vector: queryEmbedding,
            limit: limit,
            filter: { // Optional: Filter results by userId if storing multiple users' history
                must: [
                    {
                        key: 'userId',
                        match: {
                            value: userId,
                        },
                    },
                ],
            },
            with_payload: true, // Retrieve the payload (including the original text)
        });

        // Extract the text from the search results
        const relevantHistory = searchResult
            .map(result => {
                // Safely access the text property within the payload
                if (result.payload && typeof result.payload === 'object' && 'text' in result.payload) {
                    return result.payload.text as string;
                }
                return undefined; // Return undefined if text is not found
            })
            .filter((text): text is string => typeof text === 'string' && text.length > 0); // Filter out undefined or empty strings
        console.log(`Found ${relevantHistory.length} relevant history messages for user ${userId}.`);
        return relevantHistory;

    } catch (error) {
        console.error(`Error searching chat history in Qdrant for user ${userId}:`, error);
        throw error;
    }
};


export default qdrantClient;
