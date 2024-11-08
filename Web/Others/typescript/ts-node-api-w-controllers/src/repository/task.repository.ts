import { Task } from "../dtos/task.dto";
import { randomUUID } from "crypto";

export class TaskRepository {
    private tasksDb: Task[] = 
    [
        { completed: true, id: "1", title: "Preparation for the interview" },
        { completed: false, id: "2", title: "Nodejs theory", description: "Google: Nodejs questions for interviews and have a quick look" },
        { completed: true, id: "3", title: "Prepare lunch", description: "Visit Migros to take something to cook" }
    ]

    public async add(task: Task) : Promise<Task> {
        task.id = randomUUID();
        this.tasksDb.push(task);
        return task;
    }

    public async firstOrDefault(id: string) : Promise<Task | null> {
        var task = this.tasksDb.find(x => x.id == id);
        return task ?? null;
    }

    public async toList() : Promise <Task[]> {
        return this.tasksDb;
    }
}