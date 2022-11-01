import { body, validationResult } from "express-validator";

const bookValidator = [
  body("title")
    .isAlphanumeric()
    .trim()
    .withMessage("chars and numbers only")
    .isLength({ min: 2 })
    .withMessage("at least 2 chars"),
  (req, res, next) => {
    const errors = validationResult(req);
    if (errors.isEmpty()) {
      next();
    } else {
      var error = errors.mapped();
      res.render("addbookform", { error: error });
    }
  },
];

export { bookValidator }