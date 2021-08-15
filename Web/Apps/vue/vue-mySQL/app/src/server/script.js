// https://www.youtube.com/watch?v=hGZX_SA7lYg
// https://www.npmjs.com/package/mysql

var express = require('express');
var mysql = require('mysql');
var app = express();
const dotenv = require('dotenv').config();

var q = "SELECT * FROM `users`"

var connection = mysql.createPool({
  connectionLimit: '50', // 50 connections per second
  host: process.env.MYSQL_HOST,
  user: process.env.MYSQL_USER,
  password: process.env.MYSQL_PASSWORD,
  database: process.env.MYSQL_DATABASE
});

app.get('/', function (req, resp) {
  connection.getConnection(function (error, tempCon) {
    if (!!error) {
      tempCon.release();
      console.log('app get error');
    } else {
      console.log('Connected!');

      tempCon.query(q, function (error, results, fields) {
        tempCon.release();
        if (!!error) {
          console.log('Error in query');
        } else {
          resp.json(results[0]);
        }
      });
    }

  })


})

console.log(1);
app.listen(process.env.MYSQL_PORT); // i test it on localhost:1337
console.log(2);