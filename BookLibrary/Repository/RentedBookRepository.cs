using BookLibrary.Data;
using BookLibrary.Dto;
using BookLibrary.Interface;
using BookLibrary.Models;

namespace BookLibrary.Repository
{
    public class RentedBookRepository : IRentedBookRepository
    {
        private readonly DataContext _context;

        public RentedBookRepository(DataContext contxet)
        {
            _context = contxet;
        }

        public bool bookExists(int bookId)
        {
            return _context.books.Any(b => b.Id == bookId && b.quantity > 0);
        }

        public ICollection<rentDto> GetMembersByRentedBooks(int bookId)
        {
            var result = _context.books
                .Join(_context.rentedBooks
                .Where(r => r.bookId == bookId && r.Ruternd == false),
                b => b.Id,
                r => r.bookId,
                (b, r) => new { Book = b, RentedBook = r })
                .Join(_context.members,
                joinedTables => joinedTables.RentedBook.memberId,
                m => m.Id,
                (joinedTables, m) => new rentDto
                {
                    Receipt = joinedTables.RentedBook.receipt,
                    FullName = m.fullName,
                    BookName = joinedTables.Book.bookName,
                    RentDate = joinedTables.RentedBook.rentDate,
                    RentDue = joinedTables.RentedBook.rentDue,
                })
                .ToList();

            return result;

        }

        public int getReceiptNum()
        {
            int maxReceipt = 0;

            var books = GetRentedBooks();

            if (books.Any())
            {
                maxReceipt = books.Max(n => n.receipt);

                if (maxReceipt == 0)
                    maxReceipt = 1;
                else
                    maxReceipt = maxReceipt + 1;
            }
            return maxReceipt;
        }

        public ICollection<rentDto> GetRentDueBooks()
        {
            return _context.rentedBooks
                .Where(r => r.rentDue < DateTime.Now)
                .Select(r => new rentDto
                {
                    Receipt = r.receipt,
                    FullName = r.member.fullName,
                    BookName = r.book.bookName,
                    RentDate = r.rentDate,
                    RentDue = r.rentDue,
                })
                .ToList();
        }

        public RentedBook GetRentedBook(int receiptNum)
        {
            return _context.rentedBooks.Where(r => r.receipt == receiptNum).FirstOrDefault();
        }

        public ICollection<RentedBook> GetRentedBooks()
        {
            return _context.rentedBooks.Where(r => r.Ruternd == false).ToList();
        }

        public ICollection<rentDto> GetRentedBooksByMember(int memberId)
        {
            var result = _context.members
                .Join(_context.rentedBooks
                .Where(r => r.memberId == memberId && r.Ruternd == false),
                m => m.Id,
                r => r.memberId,
                (m, r) => new { Member = m, RentedBook = r })
                .Join(_context.books,
                joinedTables => joinedTables.RentedBook.bookId,
                b => b.Id,
                (joinedTables, b) => new rentDto
                {
                    Receipt = joinedTables.RentedBook.receipt,
                    FullName = joinedTables.Member.fullName,
                    BookName = b.bookName,
                    RentDate = joinedTables.RentedBook.rentDate,
                    RentDue = joinedTables.RentedBook.rentDue,
                })
                .ToList();

            return result;
        }

        public bool memberExists(int memberId)
        {
            return _context.members.Any(b => b.Id == memberId);
        }

        public bool rentBook(RentedBook rentedBook)
        {
            _context.Add(rentedBook);
            return Save();
        }

        public bool RentedBookExists(int? receiptNum, int? memberId, int? bookId)
        {
            if (receiptNum == null & memberId != null && bookId == null)
            {
                return _context.rentedBooks.Any(r => r.memberId == memberId && r.Ruternd == false);
            }
            else if (receiptNum == null & memberId == null && bookId != null)
            {
                return _context.rentedBooks.Any(r => r.bookId == bookId && r.Ruternd == false);
            }
            else if (receiptNum == null & memberId == null & bookId == null)
            {
                return _context.rentedBooks.Any(r => r.Ruternd == false);
            }
            else
            {
                return _context.rentedBooks.Any(r => r.receipt == receiptNum && r.memberId == memberId && r.Ruternd == false);
            }
        }

        public bool rentReturnBook(int bookId, int receiptNum)
        {
            var book = _context.books.Where(b => b.Id == bookId).FirstOrDefault();

            if (receiptNum != 0)
            {
                book.quantity = book.quantity + 1;

                var rent = _context.rentedBooks.Where(r => r.receipt == receiptNum).FirstOrDefault();
                rent.Ruternd = true;
            }
            else
                book.quantity = book.quantity - 1;


            return Save();

        }

        public bool returnBook(RentedBook rentedBook)
        {
            rentedBook.Ruternd = true;
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
