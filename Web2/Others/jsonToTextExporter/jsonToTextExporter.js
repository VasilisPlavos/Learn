let parsedJson = require('./imoti-gd-props.json');
const fs = require('fs');
let offers = parsedJson.offers;

var listOfPropertyIds = "";
offers.forEach(offer => {
    var propertyId = offer.src_id;
    listOfPropertyIds = 
    `${listOfPropertyIds}    ${propertyId}
    `
});

fs.writeFile(`helloworld.txt`, listOfPropertyIds, function (err) {
    if (err) return console.log(err);
    console.log('Hello World > helloworld.txt');
  });