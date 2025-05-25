import { Client, GatewayIntentBits, Message, TextChannel } from 'discord.js';

class DiscordService {
    private static client: Client;
    private static channelId: string;

    public static async init(token: string, channelId: string): Promise<void> {
        this.client = new Client({
            intents: [GatewayIntentBits.Guilds, GatewayIntentBits.GuildMessages],
        });

        this.channelId = channelId;

        this.client.once('ready', () => {
            console.log(`Logged in as ${this.client.user?.tag}!`);
        });

        await this.client.login(token);
    }

    /**
     * Get a TextChannel by ID
     */
    private static async getChannel(id: string): Promise<TextChannel> {
        const channel = await this.client.channels.fetch(id);
        if (!channel || !channel.isTextBased()) {
            throw new Error('❌ Invalid or non-text channel');
        }
        return channel as TextChannel;
    }

    /**
     * Send a message to the Discord channel
     */
    public static async SendMessage(content: string, channelId : string = this.channelId): Promise<Message> {
        const channel = await this.getChannel(channelId);
        return await channel.send(content);
    }

    public static async GetAllMessages(limit: number = 100): Promise<Message[]> {
        const channel = await this.getChannel(this.channelId);
        const messages: Message[] = [];

        let lastMessageId: string | undefined;
        while (true) {
            const fetchedMessages = await channel.messages.fetch({ limit: 100, before: lastMessageId });
            if (fetchedMessages.size === 0) break; // No more messages to fetch

            messages.push(...fetchedMessages.values());

            // If we fetched less than 100, we reached the end
            if (fetchedMessages.size < 100) break;
            if (messages.length >= limit) break;

            lastMessageId = fetchedMessages.last()?.id;
        }

        return messages.slice(0, limit);
    }

    /**
     * Fetch a message by ID
     */
    public static async GetMessage(messageId: string): Promise<Message> {
        const channel = await this.getChannel(this.channelId);
        return await channel.messages.fetch(messageId);
    }

    /**
     * Edit a message by ID
     */
    public static async EditMessage(messageId: string, newContent: string): Promise<Message> {
        const message = await this.GetMessage(messageId);
        return await message.edit(newContent);
    }

    /**
     * Delete a message by ID
     */
    public static async DeleteMessage(messageId: string): Promise<void> {
        const message = await this.GetMessage(messageId);
        await message.delete();
    }
}

export default DiscordService;