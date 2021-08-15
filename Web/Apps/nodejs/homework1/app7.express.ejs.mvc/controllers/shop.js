const Product = require('../models/product');


exports.getProducts = (req, res, next) => {
    Product.fetchAll(products => {
        console.log(3, products);
        res.render('shop/product-list', {
            prods: products,
            pageTitle: 'Products'
        });
    });
};

exports.getIndex = (req, res, next) => {
    Product.fetchAll(products => {
        console.log(3, products);
        res.render('shop/index', {
            prods: products,
            pageTitle: 'Shop'
        });
    });
};

exports.getCart = (req, res, next) => {
    res.render('shop/cart', {
        pageTitle: 'Cart'
    });
};

exports.getOrders = (req, res, next) => {
    res.render('shop/orders', {
        pageTitle: 'Orders'
    });
};

exports.getCheckout = (req, res, next) => {
    res.render('shop/checkout', {
        pageTitle: 'Checkout'
    });
};