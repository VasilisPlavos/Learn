const Order = require("../models/order");
const Product = require("../models/product");

exports.getProducts = (req, res, next) => {
  Product.fetchAll()
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
  Product.findById(req.params.productId)
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
  Product.fetchAll()
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

exports.getCart = async (req, res, next) => {
  var productIds = req.user.cart.items.map((i) => {
    return i.productId;
  });
  var cartProducts = await Product.findAll(productIds);
  var products = cartProducts.map(p => {
    return {
      ...p, quantity: 1
    }
  })

  res.render("shop/cart", {
    path: "/cart",
    pageTitle: "Your Cart",
    products: products,
  });
};

exports.postCart = async (req, res, next) => {
  const prodId = req.body.productId;
  var product = await Product.findById(prodId);
  req.user.addToCart(product);

  // var product = await Product.findById(req.body.productId);
  // if (!product) return null;

  // var cart = await req.user.getCart();
  // var cartSelectedProducts = await cart.getProducts({
  //   where: { id: product._id },
  // });

  // var quantity = 0;
  // if (cartSelectedProducts[0]) {
  //   quantity = cartSelectedProducts[0].CartItem.quantity;
  // }

  // await cart.addProduct(product, { through: { quantity: ++quantity } });
  res.redirect("/cart");
};

exports.postCartDeleteProduct = async (req, res, next) => {
  req.user.DeleteFromCart(req.body.productId);
  res.redirect("/cart");
};

exports.postOrder = async (req, res, next) => {
  await req.user.addOrder();

  // var cart = await req.user.getCart();
  // var products = await cart.getProducts();
  // var order = await req.user.createOrder();

  // // prepare products
  // var preparedProducts = products.map((x) => {
  //   x.orderItem = { quantity: x.CartItem.quantity };
  //   return x;
  // });

  // await order.addProducts(preparedProducts);
  // await cart.setProducts(null); // clear cart
  res.redirect("/orders");
};

exports.getOrders = async (req, res, next) => {
  var orders = await Order.getOrders(req.user._id);
  // var


  res.render("shop/orders", {
    path: "/orders",
    pageTitle: "Your Orders",
    orders: orders,
  });
};
