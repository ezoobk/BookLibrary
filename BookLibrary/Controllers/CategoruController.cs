using AutoMapper;
using BookLibrary.Data;
using BookLibrary.Dto;
using BookLibrary.Interface;
using BookLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoruController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoruController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("Get-Categories")]
        public IActionResult GetCategories()
        {
            var categories = _mapper.Map<List<CategoryDto>>(_categoryRepository.GetBookCategories());

            if (categories is null)
                return NotFound();

            return Ok(categories);
        }

        [HttpGet("{categoryId}")]
        public IActionResult getCategory(int categoryId)
        {
            var category = _mapper.Map<CategoryDto>(_categoryRepository.GetCategory(categoryId));

            if (category is null)
                return NotFound("category Not Found");

            return Ok(category);
        }

        [HttpPost]
        [Route("Add-category")]
        public IActionResult AddCategory([FromBody] CategoryDto categoryCreate)
        {
            if (categoryCreate == null)
                return BadRequest();

            var newCategory = _categoryRepository.GetBookCategories()
                .Where(c => c.category.Trim().ToUpper() == categoryCreate.category.Trim().ToUpper())
                .FirstOrDefault();

            if (newCategory != null)
            {
                ModelState.AddModelError("", "Category Alrady Exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryMap = _mapper.Map<BookCategory>(categoryCreate);

            if (!_categoryRepository.CreateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Somthing Went Wrong");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        //[HttpPut]
        //[Route("update-category")]
        //public IActionResult updateauthor([FromBody] CategoryDto categoryDto, [FromQuery] int categoryId)
        //{
        //    var categoryDb = _context.bookCategories.Where(p => p.Id == categoryId).FirstOrDefault();
        //    if (categoryDb is null)
        //        return NotFound("category is Not Found");

        //    categoryDb.category = categoryDto.category;

        //    _context.SaveChanges();

        //    return Ok(categoryDb);
        //}

        //[HttpDelete]
        //[Route("delete-category")]
        //public async Task<ActionResult<List<BookAuthor>>> deletecategory(int id)
        //{
        //    var categoryDb = await _context.bookCategories.FindAsync(id);

        //    if (categoryDb is null)
        //        return NotFound("Author is Not Found");

        //    _context.bookCategories.Remove(categoryDb);
        //    await _context.SaveChangesAsync();

        //    return Ok(categoryDb);
        //}

    }
}
