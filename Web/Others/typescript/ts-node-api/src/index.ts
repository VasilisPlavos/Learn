/**
 * Required External Modules
 */


import * as dotenv from "dotenv";
import express from "express";
import helmet from "helmet";
import cors from "cors";
import { itemsRouter } from "./items/items.router";
import { errorHandler } from "./middlewares/errorHandler.mw";

dotenv.config();


/**
 * App Variables
 */

if (!process.env.PORT) process.exit(1);
const PORT: number = +process.env.PORT;
const app = express();

/**
 *  App Configuration
 */
app.use(helmet());
app.use(cors());
app.use(express.json());
app.use("/api/items", itemsRouter);
app.use(errorHandler);

/**
 * Server Activation
 */
app.listen(PORT, () => {
    console.log(`Listening on http://localhost:${PORT}`);
})