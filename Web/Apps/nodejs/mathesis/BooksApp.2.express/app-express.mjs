import express from "express";
import { engine } from "express-handlebars";
import { router as booksExampleRouter } from "./routes/books-example.mjs";
import { router as booksRouter } from "./routes/books.mjs";

const app = express();
app.use(express.static("public")); // set folder for static files
app.use(express.urlencoded({ extended: true }));

app.engine("hbs", engine({ extname: ".hbs" }));
app.set("view engine", "hbs");

app.use(booksExampleRouter);
app.use(booksRouter);

app.get("/", (req, res) => res.send("API is running..."));
app.use((req, res) => res.status(404).send("404 not found"));

app.listen(3000, () => console.log(`app running: https://localhost:3000`));
