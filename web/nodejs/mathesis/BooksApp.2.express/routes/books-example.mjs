import express from "express";
const router = express.Router();

// router.use(express.urlencoded({ extended: true }));

// GET: /books-example?anyKey=anyValue
router.get("/books-example", (req, res) => {
  res.send(req.query);
  console.log("get books-example: ", req.query);
});

// this works because of app use urlencoded
// POST: /books-example -> x-www-form-urlencoded -> KEY: anyKey AND VALUE:anyValue
router.post("/books-example", (req, res) => {
  res.send(req.body);
  console.log(3, req.body);
});

// books-example/:anyValue/:anyValue
router.get("/books-example/:title/:author", (req, res) => {
  res.send(req.params);
  console.log(`title: ${req.params["title"]}`);
  console.log(`author: ${req.params["author"]}`);
});

export { router };
