import express from "express";
import * as dotenv from "dotenv";
import DiscordService from "./services/DiscordService";

dotenv.config();

const app = express();
app.use(express.json());

// Initialize Discord Service
DiscordService.init(process.env.DISCORD_BOT_TOKEN!, process.env.DISCORD_CHANNEL_ID!);

app.get("/", (req, res) => {
  res.send("Hello, World!");
});

app.post('/api/messages', async (req, res) => {
  const { content } = req.body;

  if (!content) {
    res.status(400).json({ error: "Message is required" });
    return;
  }

  const messageSent = await DiscordService.SendMessage(content);
  res.status(200).json({ success: true, messageSent: messageSent });
});

app.get('/api/messages/:id', async (req, res) => {
  try {
    const message = await DiscordService.GetMessage(req.params.id);
    res.json({ id: message.id, content: message.content });
  } catch (err) {
    res.status(404).json({ error: 'Message not found' });
  }
});

app.get('/api/messages', async (req, res) => {
  try {
    const messages = await DiscordService.GetAllMessages();
    res.json(messages.map(x => ({ id: x.id, content: x.content, message: x })));
  } catch (err) {
    res.status(404).json({ error: 'Message not found' });
  }
});

app.put('/api/messages/:id', async (req, res) => {
  try {
    const { content } = req.body;
    const edited = await DiscordService.EditMessage(req.params.id, content);
    res.json({ id: edited.id, content: edited.content });
  } catch (err) {
    res.status(400).json({ error: 'Failed to update message' });
  }
});

app.delete('/api/messages/:id', async (req, res) => {
  try {
    await DiscordService.DeleteMessage(req.params.id);
    res.status(204).send();
  } catch (err) {
    res.status(404).json({ error: 'Message not found or already deleted' });
  }
});




app.listen(process.env.PORT || 3000, () => {
  console.log(`Server is running on port http://localhost:${process.env.PORT || 3000}/`);
});