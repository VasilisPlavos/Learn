import express from "express";
import * as BookService from "../services/book.service.mjs";
import * as Validator from "../validator/validations.mjs";

const router = express.Router();

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

router.get("/books/edit/:bookId", async (req, res) => {
  if (!auth(req, res)) return;
  const userId = req.session.userId;
  const bookId = req.params.bookId;
  var book = await BookService.getBookDtoByIdAsync(userId, bookId);
  res.render("editbookform", { book });
});

router.post("/books/edit/:bookId", async (req, res) => {
  if (!auth(req, res)) return;

  const bookId = req.params.bookId;
  const userId = req.session.userId;
  const comment = req.body["comments"];
  await BookService.createOrUpdateCommentAsync(bookId, userId, comment);
  res.redirect("/books");
});

router.get("/books/remove/:bookId", async (req, res) => {
  if (!auth(req, res)) return;
  const userId = req.session.userId;
  const bookId = req.params.bookId;
  await BookService.removeFavoriteAsync(userId, bookId);
  res.redirect("/books");
});

router.post("/books", Validator.bookValidator, async (req, res) => {
  if (!auth(req, res)) return;

  // const title = req.body["title"];
  // const author = req.body["author"];
  const userId = req.session.userId;
  const bookDto = {
    title: req.body["title"],
    author: req.body["author"],
    comments: req.body["comments"],
  };
  await BookService.createAsync(userId, bookDto);

  res.redirect("/books");
});

function auth(req, res) {
  if (!req.session.username || !req.session.userId) {
    res.redirect("/");
    return false;
  }

  res.locals.username = req.session.username;
  return true;
}

async function showBookListAsync(req, res, next) {
  var books = await BookService.showBookListAsync(req.session.userId);
  res.render("bookList", {
    books: books,
    // we don't need this because of res.locals.username -> they are similar
    // username: req.session.username,
  });
}

export { router };
