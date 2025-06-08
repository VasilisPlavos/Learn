import { ResponsePackage } from "../dtos/responsePackage.dto";
import { Task } from "../dtos/task.dto";
import { TaskRepository } from "../repository/task.repository";

const taskRepo = new TaskRepository();

export class TaskService {
  public async createTask(task: Task): Promise<ResponsePackage<Task>> {
    let createdTask = await taskRepo.add(task);

    if (!createdTask) return { success: false, message: "task not created" };
    return { success: true, data: createdTask };
  }

  public async getTask(id: string): Promise<ResponsePackage<Task | null>> {
    let task = await taskRepo.firstOrDefault(id);

    if (!task) return { success: false, message: "task not found" };
    return { success: true, data: task };
  }

  public async getTasks(): Promise<ResponsePackage<Task[]>> {
    let result = await taskRepo.toList();
    return { success: true, data: result };
  }
}
