/**
 * Required External Modules and Interfaces
 */
import express, { Request, Response, NextFunction } from "express";
import * as ItemsService from './items.service';
import { Item } from "./item.model";

/**
 * Router Definition
 */
export const itemsRouter = express.Router();

/**
 * Controller Definitions
 */

// GET items
itemsRouter.get("/", async (req: Request, res: Response, next: NextFunction) => {
    try {
        const items = await ItemsService.findAllAsync();
        res.status(200).json(items);
    } catch (error) { next(error); }
})

// GET items/:id
itemsRouter.get("/:id", async (req: Request, res: Response, next: NextFunction) => {
    try {
        const id = +req.params.id;
        if (isNaN(id)) {
            res.status(400).send("Invalid item id");
            return;
        }

        const item = await ItemsService.findByIdAsync(+req.params.id);
        if (!item) {
            res.status(404).send("item not found");
            return;
        }

        res.status(200).send(item);
    } catch (error) { next(error); }
})


// POST items
itemsRouter.post("/", async (req: Request, res: Response, next: NextFunction) => {
    try {
        let item: Item = req.body;
        if (!item) {
            res.status(400).send("Request body is missing");
            return;
        }

        item = await ItemsService.createAsync(item);
        res.status(201).json(item);
    } catch (error) { next(error); }
});

// PUT items
itemsRouter.put("/", async (req: Request, res: Response, next: NextFunction) => {
    try {
        let item: Item = req.body;
        await ItemsService.updateAsync(item);
        res.status(200).json(item);
    } catch (error) {
        next(error);
    }
});

// DELETE items/:id
itemsRouter.delete("/:id", async (req: Request, res: Response, next: NextFunction) => {
    try {
        const id = +req.params.id;
        if (isNaN(id)) {
            res.status(400).send("Invalid item id");
            return;
        }

        await ItemsService.removeAsync(id);
        res.sendStatus(204);
    } catch (error) { next(error); }
});