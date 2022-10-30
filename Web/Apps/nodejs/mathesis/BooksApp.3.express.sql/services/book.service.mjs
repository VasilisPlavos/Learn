import { Book, UserBooks } from "../data/database.mjs";

async function createBookAsync(userId, bookDto) {
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

async function getBookDtoByIdAsync(userId, bookId) {
  var book = await Book.findByPk(bookId);
  book = book.toJSON();

  var comment = await UserBooks.findOne({
    attributes: ["userComment"],
    where: { userId: userId, bookId: bookId },
  });

  var bookDto = {
    id: bookId,
    author: book.author,
    title: book.title,
    comment: comment.dataValues.userComment,
  };

  return bookDto;
}

async function removeFavoriteAsync(userId, bookId) {
  await UserBooks.destroy({ where: { userId: userId, bookId: bookId } });
}

async function showBookListAsync(userId) {
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

async function createOrUpdateCommentAsync(bookId, userId, comment) {
  if (!bookId || !userId || !comment) return;

  var userBook = await UserBooks.findOne({
    where: {
      userId: userId,
      bookId: bookId,
    },
  });

  if (!userBook) {
    await UserBooks.create({
      where: {
        userId: userId,
        bookId: bookId,
        userComment: comment ?? null,
      },
    });
  } else {
    await UserBooks.update(
      { userComment: comment ?? null },
      {
        where: {
          userId: userId,
          bookId: bookId,
        },
      }
    );
  }
}

export {
  createBookAsync,
  createOrUpdateCommentAsync,
  getBookDtoByIdAsync,
  removeFavoriteAsync,
  showBookListAsync,
};
