import { BooksRepository } from "./BooksRepository.mjs";

var booksDb = new BooksRepository();
async function runAsync() {
  const books = await booksDb.getAllAsync();
  var book = { title: "Start wars 5", author: "Στάνισλαβ Λεμ" };
  if (books.some((x) => x.title == book.title)) return;
  books.push(book);
  if (!(await booksDb.UpdateAsync(books))) return;
  console.log(books);
}

await runAsync();
