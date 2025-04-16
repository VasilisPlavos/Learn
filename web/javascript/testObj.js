// source https://smoothprogramming.com/javascript/array-vs-object-vs-json-in-javascript/

var arr = [1,2,3]



var obj = {firstname : "Hiral", lastname : "Patel"};
obj.middlename = "nareshkumar";
obj["country"] = "india";

console.log("Is this array? " +  Array.isArray(Object.keys(obj)));

console.log('');
console.log('');

console.log("this is not JSON. This is JS Object:", obj);

console.log('');
console.log('');

var JSONfromObj = JSON.stringify(obj);
console.log("this is JSON: " + JSONfromObj);

console.log('');
console.log('');

var objFromJSON = JSON.parse(JSONfromObj);
console.log("THis is object again:", objFromJSON);

console.log('');
console.log('');


let animalObj = {
    cat: 'meow',
    dog: 'woof'
  };

let dog = 'cat';
let sound = animalObj[dog];
console.log(sound);

sound = animalObj.dog;
console.log(sound);

console.log('');
console.log('');




// checkObj({gift: "pony", pet: "kitten", bed: "sleigh"}, "gift") should return "pony".

function checkObj(obj, checkProp) {
    // Only change code below this line
    if (obj.hasOwnProperty(checkProp)){
      return obj[checkProp];
    } return "Not found";
    // Only change code above this line
  }
  


obj = {gift: "pony", pet: "kitten", bed: "sleigh"};
var t = checkObj(obj, "petj");
console.log(t)