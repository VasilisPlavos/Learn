var x = 0;

while (x < 10) {
    // console.log(x)
    x++;
}


var o = 1;
{
    var o = 2;
}

// console.log(o);

var y = 10;

// console.log(x,y);
if ((x == y)) {
    // console.log(1);
}

var b = new Boolean(false);


var fruittype = 'Bananas';


switch (fruittype) {
    case 'Oranges':
        console.log('orange');
        break;
    case 'Apples':
        console.log('apple');
        break;
    default:
        console.log(`Sorry, we are out of ${fruittype}.`);
}

