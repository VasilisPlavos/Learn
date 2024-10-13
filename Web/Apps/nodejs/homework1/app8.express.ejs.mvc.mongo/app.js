const path = require("path");
const express = require("express");
const { mongoConnect } = require("./util/database");

const errorController = require("./controllers/error");

const app = express();

app.set("view engine", "ejs");
app.set("views", "views");

const adminRoutes = require("./routes/admin");
const shopRoutes = require("./routes/shop");
const User = require("./models/user");

app.use(express.urlencoded({ extended: false }));
app.use(express.static(path.join(__dirname, "public")));

app.use((req, res, next) => {
  User.findById("670b41f00723d5a47d3db89b")
    .then((user) => {
      req.user = new User(user.name, user.email, user.cart, user._id);
    })
    .then(() => next())
    .catch((err) => console.log(err));
});

app.use("/admin", adminRoutes);
app.use(shopRoutes);

app.use(errorController.get404);

mongoConnect().then(() => {
  app.listen(3200);
});
