console.log(1);

let text: string = "text here";
console.log(text);

let textFromFunction: string = getTextFromFunction(null);
textFromFunction = getTextFromFunction(1);
textFromFunction = getTextFromFunction(`${text}ee!`);

function getTextFromFunction(value: string | null | number): string {
  if (!value) return "";
  return value.toString();
}

console.log(textFromFunction);

let product1: Product = { id: 1, productName: "book1" };
let product2: Product = { id: 2, productName: "book2" };
let products: Product[] = [];
products.push(product1);
products.push(product2);
console.log(products);

export interface Product {
  id: number;
  productName: string;
  productCode?: string;
  description?: string;
  price?: number;
}
