using BookLibrary.Data;
using BookLibrary.Interface;
using BookLibrary.Models;
using BookLibrary.Dto;

namespace BookLibrary.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly DataContext _context;

        public BookRepository(DataContext context)
        {
            _context = context;
        }
        public ICollection<Book> GetBooks()
        {
            return _context.books.OrderBy(p => p.Id).ToList();
        }
        public Book GetBook(int id)
        {
            return _context.books.Where(p => p.Id == id).FirstOrDefault();
        }
        public Book GetBookByName(string name)
        {
            return _context.books.Where(p => p.bookName == name).FirstOrDefault();
        }
        public Book GetBook(DateTime releaseDate)
        {
            return _context.books.Where(p => p.releaseDate == releaseDate).FirstOrDefault();
        }
        public bool BookExists(int bookId)
        {
            return _context.books.Any(p => p.Id == bookId);
        }

        public ICollection<Book> GetAvailableBooks()
        {
            return _context.books.Where(p => p.quantity > 0).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool InsertBook(int categoryId, int authorId, BookDto book)
        {
            var bookCategory = _context.bookCategories.FirstOrDefault(p => p.Id == categoryId);
            var bookAuthor = _context.bookAuthors.FirstOrDefault(p => p.Id == authorId);

            if (bookCategory == null || bookAuthor == null)
            {
                return false; // CategoryId or AuthorId not found, return false or throw an exception
            }

            var newBook = new Book
            {
                bookName = book.bookName,
                releaseDate = book.releaseDate,
                quantity = book.quantity,
                bookCategory = bookCategory,
                bookAuthor = bookAuthor
            };

            _context.Add(newBook);

            return Save();
        }

        public bool UpdateBook(int bookId, BookDto bookDto)
        {
            BookExists(bookId);

            if (!BookExists(bookId))
                return false;

            var book = _context.books.FirstOrDefault(p => p.Id == bookId);

            book.bookName = bookDto.bookName;
            book.releaseDate = bookDto.releaseDate;
            book.quantity = bookDto.quantity;

            return Save();
        }

        public bool DeleteBook(int bookId)
        {
            BookExists(bookId);

            if (!BookExists(bookId))
                return false;

            var book = _context.books.FirstOrDefault(p => p.Id == bookId);

            _context.Remove(book);

            return Save();

        }

        public ICollection<Book> GetAuthorBooks(BookAuthor author)
        {
            return _context.books.Where(p => p.bookAuthor == author).ToList();
        }
    }
}
