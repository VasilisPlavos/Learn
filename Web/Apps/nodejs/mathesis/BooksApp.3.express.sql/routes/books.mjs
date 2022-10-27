import express from "express";
import { BookList } from "../bookList.mjs";
import { body, validationResult } from "express-validator";

const router = express.Router();
var bookList = null;

router.get("/books", (req, res, next) => {
  if (!auth(req, res)) return;
  req.session.hitCounter++;

  // this function is async but we don't need to wait for it
  // because the response for the client included inside
  showBookListAsync(req, res, next);
});

router.get("/books/add", (req, res) => {
  if (!auth(req, res)) return;
  res.render("addbookform");
});

router.post(
  "/books",
  body("title")
    .isAlpha()
    .trim()
    .withMessage("chars only")
    .isLength({ min: 5 })
    .withMessage("at least 5 chars"),
  async (req, res) => {
    if (!auth(req, res)) return;
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

function auth(req, res) {
  if (!req.session.username) {
    bookList = null;
    res.redirect("/");
    return false;
  }

  res.locals.username = req.session.username;
  if (!bookList) bookList = new BookList(req.session.username);
  return true;
}

async function showBookListAsync(req, res, next) {
  try {
    await bookList.loadBooksFromFileAsync();
  } catch (error) {
    next(error); // error example. to activate it just make "bookList = null" before "bookList.loadBooksFromFileAsync();"
    return;
  }
  res.render("bookList", {
    books: bookList.myBooks.books,
    // we don't need this because of res.locals.username -> they are similar
    // username: req.session.username,
  });
}

export { router };
