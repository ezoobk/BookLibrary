using AutoMapper;
using BookLibrary.Data;
using BookLibrary.Dto;
using BookLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : Controller
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public AuthorController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BookAuthor>))]
        public IActionResult getAuthors()
        {
            var authors = _context.bookAuthors;

            if (authors is null)
                return NotFound("Authoer Not Found");

            return Ok(authors);
        }

        [HttpGet("{authoerId}")]
        public IActionResult getAuthor(int authoerId)
        {
            var author = _context.bookAuthors.Where(p => p.Id == authoerId).FirstOrDefault();

            if (author is null)
                return NotFound("Authoer Not Found");

            return Ok(author);
        }

        [HttpPost]
        [Route("Add-Authoer")]
        public IActionResult AddAuthore([FromBody] AuthorDto authorDto)
        {
            var authorName = _context.bookAuthors.Where(p => p.author == authorDto.author).FirstOrDefault();

            if (authorName is null)
            {
                var author = new BookAuthor
                {
                    author = authorDto.author
                };
                _context.bookAuthors.Add(author);
                _context.SaveChanges();
            }

            return Ok();
        }

        [HttpPut]
        [Route("update-author")]
        public IActionResult updateauthor([FromBody] AuthorDto authorDto, [FromQuery] int authorId)
        {
            var AuthorDb = _context.bookAuthors.Where(p => p.Id == authorId).FirstOrDefault();
            if (AuthorDb is null)
                return NotFound("Author is Not Found");

            AuthorDb.author = authorDto.author;

            _context.SaveChanges();

            return Ok(AuthorDb);
        }

        [HttpDelete]
        [Route("delete-author")]
        public async Task<ActionResult<List<BookAuthor>>> deleteAuthor(int id)
        {
            var AuthorDb = await _context.bookAuthors.FindAsync(id);

            if (AuthorDb is null)
                return NotFound("Author is Not Found");

            _context.bookAuthors.Remove(AuthorDb);
            await _context.SaveChangesAsync();

            return Ok(AuthorDb);
        }



    }
}
