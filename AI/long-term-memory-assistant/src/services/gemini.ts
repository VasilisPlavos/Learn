import { GoogleGenerativeAI, HarmCategory, HarmBlockThreshold, Content } from '@google/generative-ai';
import config from '../config';
import { retrieveContext } from '../core/context';
import { getEmbedding } from '../core/embeddings';
import { addChatMessageToVectorDB } from '../db/qdrant';
import { saveUserFact } from '../db/postgres'; // Import function to save facts

// Initialize the Google Generative AI client
const genAI = new GoogleGenerativeAI(config.gemini.apiKey!);
const generationModel = genAI.getGenerativeModel({
    model: config.gemini.generationModel,
    // Safety settings can be adjusted as needed
    safetySettings: [
        { category: HarmCategory.HARM_CATEGORY_HARASSMENT, threshold: HarmBlockThreshold.BLOCK_MEDIUM_AND_ABOVE },
        { category: HarmCategory.HARM_CATEGORY_HATE_SPEECH, threshold: HarmBlockThreshold.BLOCK_MEDIUM_AND_ABOVE },
        { category: HarmCategory.HARM_CATEGORY_SEXUALLY_EXPLICIT, threshold: HarmBlockThreshold.BLOCK_MEDIUM_AND_ABOVE },
        { category: HarmCategory.HARM_CATEGORY_DANGEROUS_CONTENT, threshold: HarmBlockThreshold.BLOCK_MEDIUM_AND_ABOVE },
    ],
});

/**
 * Constructs the prompt for the Gemini model, injecting retrieved context.
 * @param userFacts Key-value pairs of user facts.
 * @param relevantHistory Array of relevant past conversation snippets.
 * @param currentQuery The user's current query.
 * @returns The constructed prompt string.
 */
const constructPrompt = (
    userFacts: { [key: string]: string },
    relevantHistory: string[],
    currentQuery: string
): string => {
    let prompt = "You are a helpful personal assistant with long-term memory.\n\n";

    // Inject User Facts
    if (Object.keys(userFacts).length > 0) {
        prompt += "Here is some information about the user you should remember:\n";
        for (const [key, value] of Object.entries(userFacts)) {
            prompt += `- ${key}: ${value}\n`;
        }
        prompt += "\n";
    }

    // Inject Relevant Chat History (Semantic Memory)
    if (relevantHistory.length > 0) {
        prompt += "Here are some relevant snippets from past conversations:\n";
        relevantHistory.forEach((snippet, index) => {
            prompt += `- Snippet ${index + 1}: ${snippet}\n`;
        });
        prompt += "\n";
    }

    // Add the Current Query
    prompt += `Based on the above context (if any) and your general knowledge, please respond to the user's current message:\nUser: ${currentQuery}\nAssistant:`;

    return prompt;
};

/**
 * Processes a user query, retrieves context, generates a response using Gemini,
 * and stores the interaction for future context.
 * @param userId The ID of the user.
 * @param query The user's message.
 * @returns A promise that resolves to the assistant's response string.
 */
export const generateAssistantResponse = async (userId: string, query: string): Promise<string> => {
    try {
        // 1. Retrieve context (facts and history)
        const { userFacts, relevantHistory } = await retrieveContext(userId, query);

        // 2. Construct the prompt
        const prompt = constructPrompt(userFacts, relevantHistory, query);
        console.log(`Constructed Prompt for Gemini:\n---\n${prompt}\n---`);

        // 3. Generate response using Gemini
        const result = await generationModel.generateContent(prompt);
        const response = result.response;
        const assistantResponseText = response.text();

        if (!assistantResponseText) {
            console.warn("Gemini response was empty or blocked.");
            // Check for safety feedback if needed: console.log(JSON.stringify(response.promptFeedback));
            return "I'm sorry, I couldn't generate a response for that.";
        }

        console.log(`Gemini Raw Response: ${assistantResponseText}`);

        // --- Post-response processing ---

        // 4. Store the current interaction (query + response) in the vector DB for semantic memory
        // Combine query and response for context, or store separately if needed
        const interactionText = `User: ${query}\nAssistant: ${assistantResponseText}`;
        const interactionEmbedding = await getEmbedding(interactionText); // Embed the combined text
        const messageId = crypto.randomUUID(); // Generate a unique ID for this interaction

        // Use await here to ensure storage completes before returning response
        await addChatMessageToVectorDB(userId, messageId, interactionText, interactionEmbedding);

        // 5. (Optional) Extract and save new facts learned from the interaction
        // This is a complex task (requires another LLM call or rule-based extraction)
        // Example placeholder: If the user says "My favorite color is blue", save it.
        if (query.toLowerCase().includes("my favorite color is")) {
            const color = query.split("my favorite color is")[1]?.trim().split('.')[0]; // Basic extraction
            if (color) {
                await saveUserFact(userId, 'favorite_color', color);
            }
        }
        // Add more sophisticated fact extraction logic here if needed.

        return assistantResponseText;

    } catch (error) {
        console.error(`Error generating assistant response for user ${userId}:`, error);
        // Provide a user-friendly error message
        return "I encountered an error trying to process your request. Please try again later.";
    }
};
