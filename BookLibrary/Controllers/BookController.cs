using AutoMapper;
using BookLibrary.Data;
using BookLibrary.Dto;
using BookLibrary.Interface;
using BookLibrary.Models;
using BookLibrary.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;


        public BookController(IBookRepository bookRepository, IMapper mapper)
        {

            _bookRepository = bookRepository;
            _mapper = mapper;

        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Book>))]
        public IActionResult GetBooks()
        {
            var books = _mapper.Map<List<BookDto>>(_bookRepository.GetBooks());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(books);
        }

        [HttpGet("{bookId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Book>))]
        [ProducesResponseType(400)]
        public IActionResult getBook(int bookId)
        {
            if (!_bookRepository.BookExists(bookId))
                return NotFound();

            var book = _mapper.Map<BookDto>(_bookRepository.GetBook(bookId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(book);
        }

        //[HttpGet("{bookname}")]
        //[ProducesResponseType(200, Type = typeof(IEnumerable<Book>))]
        //[ProducesResponseType(400)]
        //public IActionResult getBookByName(string bookName)
        //{
        //    if (!_bookRepository.BookExists(bookName))
        //        return NotFound();

        //    var book = _mapper.Map<BookDto>(_bookRepository.GetBook(bookName));

        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    return Ok(book);
        //}

        [HttpGet]
        [Route("Get-available-Books")]
        public IActionResult GetAvailableBooks()
        {
            var books = _bookRepository.GetAvailableBooks();
            var bookDtos = _mapper.Map<List<BookDto>>(books);

            if (bookDtos.Count == 0)
                return NotFound("No Books available");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(bookDtos);
        }

        //[HttpPost]
        //[Route("Insert-book")]
        //public IActionResult InsertBook(int categoryId, int authorId, [FromBody] BookDto book)
        //{
        //    var success = _bookRepository.InsertBook(categoryId, authorId, book);

        //    if (success)
        //        return Ok("Book inserted successfully");
        //    else
        //        return BadRequest("Failed to insert book");

        //}

        [HttpPut]
        [Route("update-book")]
        public IActionResult updateProuduct([FromQuery] int bookId, [FromBody] BookDto bookDto)
        {
            var book = _bookRepository.UpdateBook(bookId, bookDto);

            if (book)
                return Ok("Book Was Updated successfully");
            else
                return BadRequest("Failed to Update book");

        }

        [HttpDelete]
        [Route("delete-Book")]
        public IActionResult deleteProuduct([FromQuery] int bookId)
        {
            var book = _bookRepository.DeleteBook(bookId);

            if (book)
                return Ok("Book Was Deleted successfully");
            else
                return BadRequest("Failed to Delete book");

        }

        //[HttpGet]
        //[Route("Get-available-Books")]
        //public IActionResult GetAuthorBooks([FromQuery] int authorId)
        //{
        //    var book = _mapper.Map<List<BookDto>>(book);

        //    if (bookDtos.Count == 0)
        //        return NotFound("No Books available");

        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    return Ok(bookDtos);
        //}



    }

}
