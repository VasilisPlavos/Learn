import express from "express";
import { body, validationResult } from "express-validator";
import * as UserService from "../services/user.service.mjs";

const router = express.Router();

router.post("/users", async (req, res) => {
  var username = req.body["username"];
  var password = req.body["password"];

  var user = await UserService.authenticateUserAsync(username, password);
  if (!user) {
    res.redirect("/");
    return;
  }

  req.session.username = user.username;
  req.session.userId = user.id;
  res.redirect("/books");
});

router.get("/users/logout", (req, res) => {
  req.session.destroy();

  // i do this temporary to kill the instance
  // of the BookList(filename=username)
  // update2: probably not needed anymore!
  res.redirect("/books");
});

router.get("/users/register", (req, res) => {
  res.render("register");
});

router.post(
  "/users/register",
  body("password")
    .trim()

    // validator
    .isLength({ min: 4, max: 10 })
    .withMessage("password chars min 4, max 10")

    // custom validator
    .custom((value, metaContext) => {
      if (value == metaContext.req.body.username) {
        throw new Error("username and password must be different");
      }
      return true;
    }),

  async (req, res) => {
    const errors = validationResult(req);
    if (!errors.isEmpty()) {
      res.render("register", { messages: errors.mapped() });
      return;
    }

    var username = req.body.username;
    var password = req.body.password;
    var user = await UserService.createAsync(username, password);
    if (!user) {
      res.render("register", { messages: "error try again" });
      return;
    }

    // req.session.username = user.username;
    res.redirect("/");
  }
);

export { router };
