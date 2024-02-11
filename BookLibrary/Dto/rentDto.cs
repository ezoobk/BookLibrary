namespace BookLibrary.Dto
{
    public class rentDto
    {
        public int Receipt { get; set; }
        public string FullName { get; set; }
        public string BookName { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime RentDue { get; set; }

    }
}
