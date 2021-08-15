// kw: https://www.google.com/search?q=js+self+invoking+function

function a() {
    console.log(1);
}

a();

(function () {
    console.log(2);
}());


var c = 3;
(function (c) {
    console.log(c);
}(c));


// example

// this
function myFunction(a, b) {
    return a * b;
  }
//   can work like this
var x = function (a, b) {return a * b};

console.log(x(4,5));
// end of example