using BookLibrary.Models;

namespace BookLibrary.Interface
{
    public interface IAuthorRepository
    {
        ICollection<BookAuthor> GetBookAuthors();
        BookAuthor GetAuthor(int id);
        bool AuthorExists(int id);
    }
}
