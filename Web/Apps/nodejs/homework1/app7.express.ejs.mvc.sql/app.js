const path = require("path");
const express = require("express");
const db = require("./util/database");

const errorController = require("./controllers/error");

const app = express();

app.set("view engine", "ejs");
app.set("views", "views");

const adminRoutes = require("./routes/admin");
const shopRoutes = require("./routes/shop");

db.execute("SELECT id FROM products LIMIT 1")
  .then((result) => {
    if (result) console.log("db connected");
  })
  .catch((err) => console.log("db not connected", err));

app.use(express.urlencoded({ extended: false }));
app.use(express.static(path.join(__dirname, "public")));

app.use("/admin", adminRoutes);
app.use(shopRoutes);

app.use(errorController.get404);

app.listen(3000);
