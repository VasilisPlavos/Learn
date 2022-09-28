import fs from "fs/promises";
const fileName = "myBooks.json";

class BookList {
  myBooks = { books: [] };

  async loadBooksFromFileAsync() {
    try {
      const data = await fs.readFile(fileName, "utf-8");
      this.myBooks = JSON.parse(data);
    } catch (error) {
      throw error;
    }
  }

  addBookToFile(newBook) {
    if (!this.isBookInList(newBook)) {
      this.myBooks.books.push(newBook);

      try {
        fs.writeFile(fileName, JSON.stringify(this.myBooks), { flag: "w+" });
      } catch (error) {
        throw error;
      }
    }
  }

  isBookInList(book) {
    let bookFound = this.myBooks.books.find(
      (x) => x.title === book.title && x.author === book.author
    );

    return bookFound;
  }
}
const bookList = new BookList();
await bookList.loadBooksFromFileAsync();
bookList.addBookToFile({ "title": "Start wars 3", "author": "Στάνισλαβ Λεμ" });
await bookList.loadBooksFromFileAsync();
console.log(bookList.myBooks);
