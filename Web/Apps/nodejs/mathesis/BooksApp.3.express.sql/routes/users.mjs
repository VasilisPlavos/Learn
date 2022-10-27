import express from "express";

const router = express.Router();

router.post("/users", (req, res) => {
  req.session.username = req.body["username"];
  res.redirect("/books");
});

router.get("/users/logout", (req, res) => {
  req.session.destroy();

  // i do this temporary to kill the instance
  // of the BookList(filename=username)
  res.redirect("/books");
});

export { router };
