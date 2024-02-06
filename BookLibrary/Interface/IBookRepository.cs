using BookLibrary.Dto;
using BookLibrary.Models;

namespace BookLibrary.Interface
{
    public interface IBookRepository
    {
        ICollection<Book> GetBooks();
        ICollection<Book> GetAvailableBooks();
        ICollection<Book> GetAuthorBooks(BookAuthor Author);
        Book GetBook(int id);
        Book GetBookByName(string name);
        Book GetBook(DateTime releaseDate);
        bool BookExists(int bookId);
        bool InsertBook(Book book);
        bool UpdateBook(int bookId, BookDto book);
        bool DeleteBook(int bookId);
        bool Save();

    }
}
