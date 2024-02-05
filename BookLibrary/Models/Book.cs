namespace BookLibrary.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string bookName { get; set; }
        public DateTime releaseDate { get; set; }
        public BookCategory bookCategory { get; set; }
        public BookAuthor bookAuthor { get; set; }
        public int quantity { get; set; }
        public ICollection<RentedBook> RentedBooks { get; set; }
    }
}
