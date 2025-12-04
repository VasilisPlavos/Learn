import express, { NextFunction, Request, Response } from 'express';
import { TaskService } from '../services/task.service';
import { Task } from '../dtos/task.dto';

export const tasksRouter = express.Router();
const taskService = new TaskService();

tasksRouter.get('/tasks', async (req: Request, res: Response, next: NextFunction) => {
    var result = await taskService.getTasks();
    res.status(200).json(result);
});

tasksRouter.get('/tasks/:id', async (req: Request, res: Response, next: NextFunction) => {
    const { id } = req.params;
    let result = await taskService.getTask(id);
    res.status(200).json(result);
})

tasksRouter.post('/tasks/', async (req: Request, res: Response, next: NextFunction) => {
    const task : Task = req.body;
    let result = await taskService.createTask(task);
    res.status(200).json(result);
})