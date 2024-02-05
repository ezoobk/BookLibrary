using BookLibrary.Data;
using BookLibrary.Interface;
using BookLibrary.Models;

namespace BookLibrary.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private DataContext _context;
        public AuthorRepository(DataContext context)
        {
            _context = context;

        }
        public bool AuthorExists(int id)
        {
            return _context.bookAuthors.Any(c => c.Id == id);
        }

        public BookAuthor GetAuthor(int id)
        {
            return _context.bookAuthors.Where(p => p.Id == id).FirstOrDefault();
        }

        public ICollection<BookAuthor> GetBookAuthors()
        {
            return _context.bookAuthors.ToList();
        }

    }
}
