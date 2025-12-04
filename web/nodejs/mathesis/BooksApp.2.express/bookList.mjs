import fs from "fs/promises";

class BookList {
  myBooks = { books: [] };

  constructor(username) {
    if (!username) {
      var message = "username is empty";
      console.log(message);
      throw new Error(message);
    }
    this.fileName = `myBooks_${username}.json`;
  }

  async loadBooksFromFileAsync() {
    try {
      const data = await fs.readFile(this.fileName, {
        flag: "a+",
        encoding: "utf-8",
      });
      if (!data) return;
      this.myBooks = JSON.parse(data);
    } catch (error) {
      throw error;
    }
  }

  async addBookToFileAsync(newBook) {
    if (this.isBookInList(newBook)) return;
    this.myBooks.books.push(newBook);
    await fs.writeFile(this.fileName, JSON.stringify(this.myBooks)); // { flag: "w+" });
    return;
  }

  isBookInList(book) {
    let bookFound = this.myBooks.books.find(
      (x) => x.title === book.title && x.author === book.author
    );

    return bookFound;
  }
}

export { BookList };
