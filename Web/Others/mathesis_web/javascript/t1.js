let myNum = '1';
console.log(typeof myNum);

// one line comment


/*
many
lines
comment
*/

let x = 10;


for (var i = 0; i < 10; i++){
    console.log(i)
}

console.log(i)

let a = 5;
let b = 10;

console.log(`a+b=${a+b} ενώ 2a+b=${2*a+b}`);

console.log('a+b=' + (a + b) + " ενώ 2a+b=" + (2*a + b))




let word = 'TheSsAloniKi'
console.log(firstCapitalOthersLow(word));
word = "ATH765;Athens El.Venizelos"; // toprint ATH: Athens El. Venizelos
console.log(athens(word));



function firstCapitalOthersLow(word){
    var allSmall = word.toLowerCase();
    var firstCapital = word[0].toUpperCase();
    var rightWord = firstCapital + allSmall.slice(1);
    return rightWord;
}

function athens(word){
    var firstPart = word.slice(0,3);
    var secondPart = word.split(';')[1] // or word.slice(word.indexOf(";")+1);
    var rightWord = `${firstPart}: ${secondPart}`
    return rightWord;
}