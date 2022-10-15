import express from "express";
import { BookList } from "../bookList.mjs";
import { body, validationResult } from "express-validator";

const router = express.Router();
const bookList = new BookList();

router.get("/books", (req, res) => {
  if (!auth(req, res)) return;
  req.session.hitCounter++;

  // this function is async but we don't need to wait for it
  // because the response for the client included inside
  showBookListAsync(req, res);
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
  if (!req.session.username) res.redirect("/");
  res.locals.username = req.session.username;
  return !!req.session.username;
}

async function showBookListAsync(req, res) {
  await bookList.loadBooksFromFileAsync();
  res.render("bookList", {
    books: bookList.myBooks.books,

    // we don't need this because of res.locals.username -> they are similar
    // username: req.session.username,
  });
}

export { router };
