const Product = require("../models/product");
const User = require("../models/user");

exports.getAddProduct = (req, res, next) => {
  res.render("admin/edit-product", {
    pageTitle: "Add Product",
    path: "/admin/add-product",
    editing: false,
  });
};

exports.postAddProduct = (req, res, next) => {
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

  Product.create({
    title: req.body.title,
    imageUrl: req.body.imageUrl,
    price: req.body.price,
    description: req.body.description,
    userId: req.user.id,
  })
    .then(() => {
      console.log("product created");
      res.redirect("/admin/products");
    })
    .catch((err) => console.log(err));
};

exports.getEditProduct = (req, res, next) => {
  const editMode = req.query.edit;
  if (!editMode) return res.redirect("/");

  Product.findByPk(req.params.productId)
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

exports.postEditProduct = (req, res, next) => {
  Product.findByPk(req.body.productId)
    .then((product) => {
      product.title = req.body.title;
      product.price = req.body.price;
      product.imageUrl = req.body.imageUrl;
      product.description = req.body.description;
      return product.save();
    })
    .then(() => console.log("product saved"))
    .catch((err) => console.log(err))
    .finally(() => res.redirect("/admin/products"));
};

exports.getProducts = (req, res, next) => {
  Product.findAll()
    .then((products) => {
      res.render("admin/products", {
        prods: products,
        pageTitle: "Admin Products",
        path: "/admin/products",
      });
    })
    .catch((err) => console.log(err));
};

exports.postDeleteProduct = (req, res, next) => {
  // this works also
  //  Product.destroy({ where: { id: req.body.productId } })

  Product.findByPk(req.body.productId)
    .then((product) => {
      return product.destroy();
    })
    .then(() => {
      console.log("product destroyed");
      res.redirect("/admin/products");
    })
    .catch((err) => console.log(err));
};
