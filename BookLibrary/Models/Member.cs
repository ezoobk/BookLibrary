namespace BookLibrary.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string fullName { get; set; }
        public DateTime joiningDate { get; set; }
        public int phoneNum { get; set; }
        public ICollection<RentedBook> RentedBooks { get; set; }

    }
}
