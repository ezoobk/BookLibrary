using AutoMapper;
using BookLibrary.Data;
using BookLibrary.Dto;
using BookLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoruController : Controller
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CategoruController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("Get-Categories")]
        public IActionResult GetCategories()
        {
            var categories = _context.bookCategories;

            if (categories is null)
                return NotFound("Categories Not Found");

            return Ok(categories);
        }

        [HttpGet("{categoryId}")]
        public IActionResult getCategory(int categoryId)
        {
            var category = _context.bookCategories.Where(p => p.Id == categoryId).FirstOrDefault();

            if (category is null)
                return NotFound("category Not Found");

            return Ok(category);
        }

        [HttpPost]
        [Route("Add-category")]
        public IActionResult AddCategory([FromBody] CategoryDto categoryDto)
        {
            var categoryName = _context.bookCategories.Where(p => p.category == categoryDto.category).FirstOrDefault();

            if (categoryName is null)
            {
                var category = new BookCategory
                {
                    category = categoryDto.category
                };
                _context.bookCategories.Add(category);
                _context.SaveChanges();
            }

            return Ok();
        }

        [HttpPut]
        [Route("update-category")]
        public IActionResult updateauthor([FromBody] CategoryDto categoryDto, [FromQuery] int categoryId)
        {
            var categoryDb = _context.bookCategories.Where(p => p.Id == categoryId).FirstOrDefault();
            if (categoryDb is null)
                return NotFound("category is Not Found");

            categoryDb.category = categoryDto.category;

            _context.SaveChanges();

            return Ok(categoryDb);
        }

        [HttpDelete]
        [Route("delete-category")]
        public async Task<ActionResult<List<BookAuthor>>> deletecategory(int id)
        {
            var categoryDb = await _context.bookCategories.FindAsync(id);

            if (categoryDb is null)
                return NotFound("Author is Not Found");

            _context.bookCategories.Remove(categoryDb);
            await _context.SaveChangesAsync();

            return Ok(categoryDb);
        }

    }
}
