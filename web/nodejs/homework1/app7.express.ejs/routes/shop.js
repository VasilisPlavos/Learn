const express = require('express');

const adminData = require('./admin');

const router = express.Router();

router.get('/', (req, res, next) => {
    console.log('in the mid');
    next();
});


router.get('/', (req, res, next) => {
    const products = adminData.products;
    console.log(products);
    res.render('shop', {
        prods: products,
        pageTitle: 'Shop'
    });
});




module.exports = router;