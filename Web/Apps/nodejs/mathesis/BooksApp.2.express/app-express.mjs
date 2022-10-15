import express from "express";
import { engine } from "express-handlebars";
import { BookList } from "./bookList.mjs";

const app = express();
app.use(express.static("public")); // set folder for static files
app.engine("hbs", engine({ extname: ".hbs" }));
app.set("view engine", "hbs");

const bookList = new BookList();

app.get("/books", async (req, res) => {
  await bookList.loadBooksFromFileAsync();
  res.render("bookList", { books: bookList.myBooks.books });
});

import booksExampleRouter from "./routes/books-example.mjs";
app.use(booksExampleRouter);

app.get("/", (req, res) => res.send("API is running..."));
app.use((req, res) => res.status(404, "fd").send("404 not found"));

app.listen(3000, () => console.log(`app running: https://localhost:3000`));
