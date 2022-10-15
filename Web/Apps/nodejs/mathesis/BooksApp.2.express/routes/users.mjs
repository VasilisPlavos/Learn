import express from "express";

const router = express.Router();

router.post("/users", (req, res) => {
  req.session.username = req.body["username"];
  res.redirect("/books");
});

router.get("/users/logout", (req, res) => {
  req.session.destroy();
  res.redirect("/");
});

export { router };
