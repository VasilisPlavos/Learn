import { connect } from "amqplib";

const connection = await connect("amqp://localhost");

const channel = await connection.createChannel();

const queue = "messages";
const message = "Hello Woorld!";

await channel.assertQueue(queue, { durable: false });

await channel.sendToQueue(queue, Buffer.from(message));


for (let i = 0; i < 100; i++) {
  await channel.sendToQueue(queue, Buffer.from(i.toString()));
}