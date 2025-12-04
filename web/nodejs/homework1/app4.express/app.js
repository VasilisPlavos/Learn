const path = require('path');

const express = require('express');
const bodyParser = require('body-parser');

const app = express();

const adminData = require('./routes/admin');
const shopRoutes = require('./routes/shop');

app.use(bodyParser.urlencoded({extended: false}));
app.use(express.static(path.join(__dirname, 'public')));

app.use('/admin', adminData.routes);
app.use(shopRoutes);

// catch all
app.use('/', (req, res, next) => {
    res.status(404).sendFile(path.join(__dirname, './views', '404.html'));
});

console.log('running at http://localhost:3000/');

app.listen(3000);