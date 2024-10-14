import express, { Request, Response } from "express";
import * as ItemsService from './items.service';
/**
 * Required External Modules and Interfaces
 */

/**
 * Router Definition
 */
export const itemsRouter = express.Router();

/**
 * Controller Definitions
 */

// GET items
itemsRouter.get("/", async (req: Request, res: Response, next: express.NextFunction) => {
    const items = await ItemsService.findAllAsync();
    res.status(200).send(items);
})

// GET items/:id
itemsRouter.get("/:id", async (req: Request, res: Response, next: express.NextFunction) => {
    const item = await ItemsService.findByIdAsync(+req.params.id);

    if (!item) res.status(404).send("item not found");
    res.status(200).send(item);
})


// POST items
itemsRouter.post("/", async (req: Request, res: Response, next: express.NextFunction) => {
    let item = await ItemsService.createAsync(req.body);
    res.status(201).send(item);
})

// PUT items
itemsRouter.put("/", async (req: Request, res: Response, next: express.NextFunction) => {
    try {
        let item = await ItemsService.updateAsync(req.body);
        res.status(200).send(item);
    } catch (error) {
        res.status(500).send(error.message);
    }
})

// DELETE items/:id
itemsRouter.delete("/:id", async (req: Request, res: Response, next: express.NextFunction) => {
    await ItemsService.removeAsync(+req.params.id);
    res.sendStatus(204); 
})