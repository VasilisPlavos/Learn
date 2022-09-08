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

var book = new Book(10,2);
var taxableBook = new TaxableBook(10, 2);
console.log(book.finalPrice(), taxableBook.finalPrice()); //should print 40 
