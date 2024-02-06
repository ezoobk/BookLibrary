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
    public class AuthorController : Controller
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public AuthorController(IAuthorRepository authorRepository, IMapper mapper, DataContext context)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BookAuthor>))]
        public IActionResult getAuthors()
        {
            var authors = _mapper.Map<List<AuthorDto>>(_authorRepository.GetBookAuthors());

            if (authors is null)
                return NotFound("Authoers Not Found");

            return Ok(authors);
        }

        [HttpGet("{authoerId}")]
        public IActionResult getAuthor(int authoerId)
        {
            if (!_authorRepository.AuthorExists(authoerId))
                return NotFound();

            var author = _mapper.Map<AuthorDto>(_authorRepository.GetAuthor(authoerId));

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(author);
        }

        [HttpPost]
        [Route("Create-Authoer")]
        public IActionResult CreateAuthor([FromBody] AuthorDto authorCreate)
        {
            if (authorCreate == null)
                return BadRequest();

            var author = _authorRepository.GetBookAuthors()
                .Where(a => a.author.Trim().ToUpper() == authorCreate.author.Trim().ToUpper())
                .FirstOrDefault();

            if (author != null)
            {
                ModelState.AddModelError("", "Author Alrady Exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var authorMap = _mapper.Map<BookAuthor>(authorCreate);

            if(!_authorRepository.CreateAuthor(authorMap))
            {
                ModelState.AddModelError("", "Somthing Went Wrong");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");

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
