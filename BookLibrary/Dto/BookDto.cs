using BookLibrary.Models;

namespace BookLibrary.Dto
{
    public class BookDto
    {
        public int Id { get; set; }
        public string bookName { get; set; }
        public DateTime releaseDate { get; set; }
        public int quantity { get; set; }
    }
}
