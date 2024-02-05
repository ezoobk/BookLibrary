namespace BookLibrary.Models
{
    public class BookAuthor
    {
        public int Id { get; set; }
        public string author { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
