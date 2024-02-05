using AutoMapper;
using BookLibrary.Data;
using BookLibrary.Dto;
using BookLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BookLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentedBooksController : Controller
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public RentedBooksController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("book-rent")]
        public IActionResult BookRent([FromBody] RentBookDto rentBookDto)
        {
            var book = _context.books.Where(p => p.Id == rentBookDto.bookId).FirstOrDefault();
            var member = _context.members.Where(p => p.Id == rentBookDto.memberId).FirstOrDefault();

            if (book == null || member == null)
                return BadRequest("member or book is not found");

            int rentedBooksCount = _context.rentedBooks.Count(c => c.bookId == rentBookDto.bookId && c.Ruternd == false);

            if ((int)book.quantity - rentedBooksCount <= 0)
                return NotFound("book is out");
            else
            {
                var rent = new RentedBook
                {
                    bookId = rentBookDto.bookId,
                    memberId = rentBookDto.memberId,
                    rentDate = rentBookDto.rentDate,
                    rentDue = rentBookDto.rentDue,
                    Ruternd = false,
                };

                _context.rentedBooks.Add(rent);
                _context.SaveChanges();

                return Ok(rent);

            }

        }

        [HttpPut]
        [Route("Ruturn-Book")]
        public IActionResult RuturnBook(int bookId, int memberId)
        {
            var book = _context.rentedBooks.Where(c => c.bookId == bookId && c.Ruternd == false && c.memberId == memberId).FirstOrDefault();

            if (book == null)
                return BadRequest("book is not found");
            if (DateTime.Now > book.rentDue)
                return BadRequest("book due time has passed");

            book.Ruternd = true;
            _context.SaveChanges();

            return Ok(book);

        }

        [HttpGet]
        [Route("get-rented-books")]
        public IActionResult GetRentedBooks()
        {
            var rentedBooks = _context.rentedBooks
                .Where(rb => rb.Ruternd == false)
                .Select(rb => new
                {
                    rb.Id,
                    BookTitle = rb.book.bookName, 
                    MemberName = rb.member.fullName,
                    RentDate = rb.rentDate,
                    RentDue = rb.rentDue,
                }).ToList();

            if (rentedBooks.Count == 0)
                return BadRequest("Rented books not found");

            return Ok(rentedBooks);

        }

        [HttpGet]
        [Route("get-rented-books-by-member")]
        public IActionResult GetRentedBooksByMember([FromQuery] int memberId)
        {
            var rentedBooks = _context.rentedBooks
                .Where(rb => rb.Ruternd == false && rb.memberId == memberId)
                .Select(rb => new
                {
                    rb.Id,
                    BookTitle = rb.book.bookName,
                    RentDate = rb.rentDate,
                    RentDue = rb.rentDue,
                }).ToList();

            if (rentedBooks.Count == 0)
                return BadRequest("member has no rented books");

            return Ok(rentedBooks);

        }

        [HttpGet]
        [Route("get-rented-members-by-book")]
        public IActionResult GetRentedMembersByBook([FromQuery] int bookId)
        {
            var rentedBooks = _context.rentedBooks
                .Where(rb => rb.Ruternd == false && rb.bookId == bookId)
                .Select(rb => new
                {
                    rb.Id,
                    MemberName = rb.member.fullName,
                    RentDate = rb.rentDate,
                    RentDue = rb.rentDue,
                }).ToList();

            if (rentedBooks.Count == 0)
                return BadRequest("book has nor been rented");

            return Ok(rentedBooks);

        }

        [HttpGet]
        [Route("get-rent-due-date")]
        public IActionResult GetRentDueDate()
        {
            var rentedBooks = _context.rentedBooks
                .Where(rb => rb.Ruternd == false && rb.rentDue < DateTime.Now)
                .Select(rb => new
                {
                    rb.Id,
                    BookTitle = rb.book.bookName,
                    MemberName = rb.member.fullName,
                    RentDate = rb.rentDate,
                    RentDue = rb.rentDue,
                }).ToList();

            if (rentedBooks.Count == 0)
                return BadRequest("no due date");

            return Ok(rentedBooks);

        }


    }
}
