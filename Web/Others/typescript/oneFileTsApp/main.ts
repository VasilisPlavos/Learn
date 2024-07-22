class Book {
  constructor(public basePrice: number, public taxRate: number) {}

  finalPrice(): number {
    return this.basePrice * 2;
  }
}

class TaxableBook extends Book {
  constructor(public basePrice: number, public taxRate: number) {
    super(basePrice, taxRate);
  }

  finalPrice(): number {
    var helperBook = new Book(this.basePrice, this.taxRate);
    var price = helperBook.finalPrice();
    price = price*2*this.taxRate;
    return price;
  }
}

function getTextFromFunction(value: string | null | number): string {
  if (!value) return "";
  return value.toString();
}

export interface Product {
  id: number;
  productName: string;
  productCode?: string;
  description?: string;
  price?: number;
}

var book = new Book(10,2);
var taxableBook = new TaxableBook(10, 2);
console.log(book.finalPrice(), taxableBook.finalPrice()); //should print 40 

console.log(1);

let text: string = "text here";
console.log(text);

let textFromFunction: string = getTextFromFunction(1);
textFromFunction = getTextFromFunction(null);
textFromFunction = getTextFromFunction(`${text}ee!`);
console.log(textFromFunction);

let product1: Product = { id: 1, productName: "book1" };
let product2: Product = { id: 2, productName: "book2" };
let products: Product[] = [];
products.push(product1);
products.push(product2);
console.log(products);
