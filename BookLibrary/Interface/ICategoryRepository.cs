using BookLibrary.Data;
using BookLibrary.Models;

namespace BookLibrary.Interface
{
    public interface ICategoryRepository
    {
        ICollection<BookCategory> GetBookCategories();
        BookCategory GetCategory(int categoryid);
        bool CategoryExists(int categoryid);
        bool CreateCategory(BookCategory category);
        bool Save();
    }
}
