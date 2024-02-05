namespace BookLibrary.Models
{
    public class RentedBook
    {
        public int Id { get; set; }
        public int bookId { get; set; }
        public Book book { get; set; }
        public int memberId { get; set; }
        public Member member { get; set; }

        public DateTime rentDate { get; set; }
        public DateTime rentDue { get; set; }
        public bool Ruternd { get; set; }

    }
}
