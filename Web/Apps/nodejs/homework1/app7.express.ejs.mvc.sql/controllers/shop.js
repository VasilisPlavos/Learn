const Product = require("../models/product");

exports.getProducts = (req, res, next) => {
  Product.findAll()
    .then((products) => {
      res.render("shop/product-list", {
        prods: products,
        pageTitle: "All Products",
        path: "/products",
      });
    })
    .catch((err) => console.log(err));
};

exports.getProduct = (req, res, next) => {
  Product.findOne({ where: { id: req.params.productId } })
    // Product.findByPk(req.params.productId) // this works also
    .then((product) => {
      res.render("shop/product-detail", {
        product: product,
        pageTitle: product.title,
        path: "/products",
      });
    })
    .catch((err) => console.log(err));
};

exports.getIndex = (req, res, next) => {
  Product.findAll()
    .then((products) => {
      res.render("shop/index", {
        prods: products,
        pageTitle: "Shop",
        path: "/",
      });
    })
    .catch((err) => {
      console.log(err);
    });
};

exports.getCart = (req, res, next) => {
  req.user.getCart().then((cart) => {
    cart.getProducts().then((products) => {
      res.render("shop/cart", {
        path: "/cart",
        pageTitle: "Your Cart",
        products: products,
      });
    });
  });
};

exports.postCart = async (req, res, next) => {
  var product = await Product.findByPk(req.body.productId);
  if (!product) return null;

  var cart = await req.user.getCart();
  var cartSelectedProducts = await cart.getProducts({
    where: { id: product.id },
  });

  var quantity = 0;
  if (cartSelectedProducts[0]) {
    quantity = cartSelectedProducts[0].CartItem.quantity;
  }

  await cart.addProduct(product, { through: { quantity: ++quantity } });
  res.redirect("/cart");
};

exports.postCartDeleteProduct = async (req, res, next) => {
  var cart = await req.user.getCart();
  var cartProductsToDelete = await cart.getProducts({
    where: { id: req.body.productId },
  });

  for (const product of cartProductsToDelete) {
    await product.CartItem.destroy();
  }

  res.redirect("/cart");
};

exports.postOrder = async (req, res, next) => {
  var cart = await req.user.getCart();
  var products = await cart.getProducts();
  var order = await req.user.createOrder();

  // prepare products
  var preparedProducts = products.map((x) => {
    x.orderItem = { quantity: x.CartItem.quantity };
    return x;
  });

  await order.addProducts(preparedProducts);
  await cart.setProducts(null); // clear cart
  res.redirect("/orders");
};

exports.getOrders = async (req, res, next) => {
  var orders = await req.user.getOrders({ include: ["products"] });
  res.render("shop/orders", {
    path: "/orders",
    pageTitle: "Your Orders",
    orders: orders,
  });
};
