const { ObjectId } = require("mongodb");
const Product = require("../models/product");
// const User = require("../models/user");

exports.getAddProduct = (req, res, next) => {
  res.render("admin/edit-product", {
    pageTitle: "Add Product",
    path: "/admin/add-product",
    editing: false,
  });
};

exports.postAddProduct = async (req, res, next) => {
  // this works also because of association at app.js
  // User.findByPk(1)
  //   .then((user) => {
  //     console.log(user);
  //     user.createProduct({
  //       title: req.body.title,
  //       imageUrl: req.body.imageUrl,
  //       price: req.body.price,
  //       description: req.body.description,
  //     });
  //   })
  const product = new Product(
    req.body.title,
    req.body.price,
    req.body.description,
    req.body.imageUrl,
    null,
    req.user._id
  );

  try {
    var result = await product.saveAsync();
    console.log("product created");
    res.redirect("/admin/products");
  } catch (error) {
    console.log(error);
  }
};

exports.getEditProduct = (req, res, next) => {
  const editMode = req.query.edit;
  if (!editMode) return res.redirect("/");

  Product.findById(req.params.productId)
    .then((product) => {
      if (!product) return res.redirect("/");

      res.render("admin/edit-product", {
        product: product,
        pageTitle: "Edit Product",
        path: "/admin/edit-product",
        editing: editMode,
      });
    })
    .catch((err) => console.log(err));
};

exports.postEditProduct = async (req, res, next) => {
  const { title, price, description, imageUrl, productId } = req.body;
  const product = new Product(
    title,
    price,
    description,
    imageUrl,
    new ObjectId(productId)
  );
  try {
    await product.updateAsync();
    console.log("product updated");
  } catch (error) {
    console.log(error);
  } finally {
    res.redirect("/admin/products");
  }
};

exports.getProducts = (req, res, next) => {
  Product.fetchAll()
    .then((products) => {
      res.render("admin/products", {
        prods: products,
        pageTitle: "Admin Products",
        path: "/admin/products",
      });
    })
    .catch((err) => console.log(err));
};

exports.postDeleteProduct = async (req, res, next) => {
  try {
    await Product.deleteById(req.body.productId);
    console.log("product destroyed");
    res.redirect("/admin/products");
  } catch (error) {
    console.log(error);
  }
};
