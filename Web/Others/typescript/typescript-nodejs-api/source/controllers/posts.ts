import { Request, Response, NextFunction } from "express";
import axios, { AxiosResponse } from "axios";

interface Post
{
    id: Number;

    // This ( | null ) in userId means that the userId can be nullable
    // In C# it is:
    // int? userId;
    userId: Number | null;
    title: String;

    // This (?) in body means that you can create a Post without declare it.
    // Example: 
    // let post:Post = { id:1, userId:2, title: "" }; // this is valid
    // let post:Post = { id:1, userId:2 }; // this is not valid
    body?: String;
}

const getPosts = async (req: Request, res: Response, next: NextFunction) =>
{
    let result: AxiosResponse = await axios.get(`https://jsonplaceholder.typicode.com/posts`);
    let posts: [Post] = result.data;
    return res.status(200).json({ message: posts });
}

const getPost = async (req: Request, res: Response, next: NextFunction) =>
{
    let id: string = req.params.id;
    let result: AxiosResponse = await axios.get(`https://jsonplaceholder.typicode.com/posts/${id}`);

    // In C# it is: 
    // Post post = result.data;
    let post: Post = result.data; 
    return res.status(200).json({ message: post });
}

const updatePost = async (req: Request, res: Response, next: NextFunction) =>
{
    let id: string = req.params.id;
    let title: string = req.body.title;
    let body: string = req.body.title ?? null;
    let response: AxiosResponse = await axios.put(`https://jsonplaceholder.typicode.com/posts/${id}`, {
        ...(title && { title }),
        ...(body && { body })
        });

    return res.status(200).json({ message: response.data });
}

const deletePost = async (req: Request, res: Response, next: NextFunction) =>
{
    let id: string = req.params.id;
    let response: AxiosResponse = await axios.delete(`https://jsonplaceholder.typicode.com/posts/${id}`);
    return res.status(200).json({ message: `post ${id} deleted` });
}

const addPost = async (req: Request, res: Response, next: NextFunction) =>
{
    let title: string = req.body.title;
    let body: string = req.body.title ?? null;
    let response : AxiosResponse = await axios.post(`https://jsonplaceholder.typicode.com/posts`, {
        ...(title && { title }),
        ...(body && { body })
    });

    return res.status(200).json({ message: response.data });
}

export default { getPosts, getPost, updatePost, deletePost, addPost };