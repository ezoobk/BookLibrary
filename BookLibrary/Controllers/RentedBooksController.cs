using AutoMapper;
using BookLibrary.Data;
using BookLibrary.Dto;
using BookLibrary.Interface;
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
        private readonly IRentedBookRepository _rentedBook;
        private readonly IMapper _mapper;

        public RentedBooksController(IRentedBookRepository rentedBook, IMapper mapper)
        {
            _rentedBook = rentedBook;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("book-rent")]
        public IActionResult BookRent([FromQuery] int bookId, [FromQuery] int memberId, [FromBody] RentingDto rentingDto)
        {
            if (!_rentedBook.bookExists(bookId))
                return BadRequest("book is not found");

            if (!_rentedBook.memberExists(memberId))
                return BadRequest("member is not found");


            int rNum = _rentedBook.getReceiptNum();

            var rent = new RentedBook
            {
                bookId = bookId,
                memberId = memberId,
                receipt = rNum,
                rentDate = rentingDto.rentDate,
                rentDue = rentingDto.rentDate.AddDays(30),
                Ruternd = false,
            };

            var rntedBookMap = _mapper.Map<RentedBook>(rent);

            if (!_rentedBook.rentBook(rntedBookMap))
            {
                ModelState.AddModelError("", "Somthing Went Wrong");
                return StatusCode(500, ModelState);
            }

            _rentedBook.rentReturnBook(bookId, 0);

            return Ok("Successfully created");

        }

        [HttpPut]
        [Route("Ruturn-Book")]
        public IActionResult RuturnBook([FromQuery] int receipt, int memberId, int bookId)
        {
            if (!_rentedBook.RentedBookExists(receipt, memberId, bookId))
                return NotFound();

            if (!_rentedBook.rentReturnBook(bookId, receipt))
                return BadRequest();

            return Ok("Successfully Ruturnrd");
        }

        [HttpGet]
        [Route("get-rented-books")]
        public IActionResult GetRentedBooks()
        {
            var rentedBooks = _mapper.Map<List<RentBookDto>>(_rentedBook.GetRentedBooks());

            if (rentedBooks == null)
                return BadRequest("no rented books");

            return Ok(rentedBooks);
        }

        [HttpGet]
        [Route("get-rented-books-by-member")]
        public IActionResult GetRentedBooksByMember([FromQuery] int memberId)
        {
            if(!_rentedBook.RentedBookExists(null, memberId, null))
                return BadRequest("there are no rented books by this member");

            var books = _mapper.Map<List<rentDto>>(_rentedBook.GetRentedBooksByMember(memberId));

            if (books == null)
                return BadRequest("something went wrong");

            return Ok(books);
        }

        [HttpGet]
        [Route("get-rented-members-by-book")]
        public IActionResult GetRentedMembersByBook([FromQuery] int bookId)
        {
            if(!_rentedBook.RentedBookExists(null, null, bookId))
                return BadRequest("there are no members rented this book");

            var members = _mapper.Map<List<rentDto>>(_rentedBook.GetMembersByRentedBooks(bookId));

            if (members == null)
                return BadRequest("something went wrong");

            return Ok(members);

        }

        [HttpGet]
        [Route("get-rent-due-date")]
        public IActionResult GetRentDueDate()
        {
            if(!_rentedBook.RentedBookExists(null, null, null))
                return BadRequest("no rented books");

            var rented = _mapper.Map<List<rentDto>>(_rentedBook.GetRentDueBooks());

            if (rented == null)
                return BadRequest("something went wrong");

            return Ok(rented);
        }


    }
}
