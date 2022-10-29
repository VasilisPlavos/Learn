import { Book, UserBooks } from "../data/database.mjs";

async function createAsync(userId, bookDto) {
  if (!bookDto.title || !bookDto.author) return;

  var book = await Book.findOrCreate({
    where: {
      title: bookDto.title,
      author: bookDto.author,
    },
  });

  await UserBooks.findOrCreate({
    where: {
      userId: userId,
      bookId: book[0].dataValues.id,
      userComment: bookDto.comments ?? null,
    },
  });
}

async function removeFavoriteAsync(userId, bookId) {
  await UserBooks.destroy({ where: { userId: userId, bookId: bookId } });
}

async function showBoolListAsync(userId) {
  if (!userId) {
    console.log("error: username not exist");
    return;
  }

  var bookListQueryResults = await UserBooks.findAll({
    attributes: ["bookId"],
    where: { userId: userId },
  });

  var selectedBooksIds = bookListQueryResults.map((x) => x.dataValues.bookId);
  let books = await Book.findAll({ where: { id: selectedBooksIds } });
  return books;
}

export { createAsync, removeFavoriteAsync, showBoolListAsync };
