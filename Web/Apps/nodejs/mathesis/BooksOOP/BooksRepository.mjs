import fs from "fs/promises";
const fileName = "billBooks.json";

export class BooksRepository {
  async getAllAsync() {
    const data = await fs.readFile(fileName, "utf-8");
    return JSON.parse(data);
  }

  async UpdateAsync(books) {
    var booksStr = JSON.stringify(books);
    await fs.writeFile(fileName, booksStr, { flag: "w+" });
    return true;
  }
}
