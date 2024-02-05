namespace BookLibrary.Models
{
    public class BookCategory
    {
        public int Id { get; set; }
        public string category { get; set; }
        public ICollection<Book> books { get; set; }
    }
}
