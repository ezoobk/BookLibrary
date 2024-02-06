using BookLibrary.Data;
using BookLibrary.Interface;
using BookLibrary.Models;
using BookLibrary.Dto;

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

        public bool CreateAuthor(BookAuthor author)
        {
            _context.Add(author);
            return Save();
        }

        public BookAuthor GetAuthor(int id)
        {
            return _context.bookAuthors.Where(p => p.Id == id).FirstOrDefault();
        }

        public ICollection<BookAuthor> GetBookAuthors()
        {
            return _context.bookAuthors.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
