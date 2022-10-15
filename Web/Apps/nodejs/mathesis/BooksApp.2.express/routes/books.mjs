import express from "express";
import { BookList } from "../bookList.mjs";

const router = express.Router();
const bookList = new BookList();

router.get("/books", async (req, res) => {
  await bookList.loadBooksFromFileAsync();
  res.render("bookList", { books: bookList.myBooks.books });
});

router.get("/books/add", (req, res) => res.render("addbookform"));

router.post("/books", async (req, res) => {
  const book = {
    title: req.body["title"],
    author: req.body["author"],
  };
  await bookList.addBookToFileAsync(book);
  res.redirect("/books");
});

export { router };
