using BookLibrary.Data;
using BookLibrary.Interface;
using BookLibrary.Models;

namespace BookLibrary.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }

        public bool CategoryExists(int categoryid)
        {
            return _context.bookCategories.Any(c => c.Id == categoryid);
        }

        public bool CreateCategory(BookCategory category)
        {
            throw new NotImplementedException();
        }

        public ICollection<BookCategory> GetBookCategories()
        {
            return _context.bookCategories.ToList();
        }

        public BookCategory GetCategory(int categoryid)
        {
            return _context.bookCategories.Where(c => c.Id == categoryid).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
