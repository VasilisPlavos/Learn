import express from "express";
import { BookList } from "../bookList.mjs";
import { body, validationResult } from "express-validator";

const router = express.Router();
const bookList = new BookList();

router.get("/books", async (req, res) => {
  req.session.hitCounter++;
  await bookList.loadBooksFromFileAsync();
  res.render("bookList", { books: bookList.myBooks.books });
});

router.get("/books/add", (req, res) => res.render("addbookform"));

router.post(
  "/books",
  body("title")
  .isAlpha().trim().withMessage("chars only")
  .isLength({ min: 5 }).withMessage("at least 5 chars"),
  async (req, res) => {
    const errors = validationResult(req);
    if (errors.errors?.length > 0) {
      var error = errors.mapped();
      res.render("addbookform", { error: error });
      return;
    }

    const book = {
      title: req.body["title"],
      author: req.body["author"],
    };
    await bookList.addBookToFileAsync(book);
    res.redirect("/books");
  }
);

export { router };
