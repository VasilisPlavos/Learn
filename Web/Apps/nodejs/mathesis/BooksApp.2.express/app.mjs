import "dotenv/config";
import express from "express";
import { engine } from "express-handlebars";
import session from "express-session";
import createMemoryStore from "memorystore";

// routers
import { router as booksExampleRouter } from "./routes/books-example.mjs";
import { router as booksRouter } from "./routes/books.mjs";
import { router as usersRouter } from "./routes/users.mjs";

const MemoryStore = createMemoryStore(session);
const myBooksSession = session({
  secret: process.env.SESSION_SECRET,
  store: new MemoryStore({ checkPeriod: 86400000 }),
  resave: false,
  saveUninitialized: true,
  name: "myBooks-sid", // default is connect.sid
  cookie: { maxAge: 1000 * 60 * 20 }, // 20 minutes
});

const app = express();

app.use(myBooksSession);
app.use(express.static("public")); // set folder for static files
app.use(express.urlencoded({ extended: true }));

app.engine("hbs", engine({ extname: ".hbs" }));
app.set("view engine", "hbs");

app.use(booksExampleRouter);
app.use(booksRouter);
app.use(usersRouter);

app.get("/", (req, res) => res.render("home"));
app.use((req, res) => res.status(404).send("404 not found"));

app.route("/b").all((req, res) => {
  console.log(req);
});

const PORT = process.env.PORT || 3000;
app.listen(PORT, () => console.log(`app running: http://localhost:${PORT}`));
