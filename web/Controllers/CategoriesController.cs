using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web.Models;
using web.Models.DTOs;
using web.Models.Entities;

namespace web.Controllers
{
    [Route("categories")]
    public class CategoriesController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<ProductsController> _logger;

        public CategoriesController(ILogger<ProductsController> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }

        // ДОДАВАННЯ КАТЕГОРІЇ
        [HttpPost("add")]
        public async Task<IActionResult> AddCategory([FromBody] CategoryDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Створюємо нову категорію
            var category = new Category
            {
                Name = dto.Name,
            };

            _context.Categories.Add(category);

            await _context.SaveChangesAsync();

            return Ok(new { message = "Category added." });
        }

        // ЗМІНА КАТЕГОРІЇ 
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Знаходимо нашу категорію
            Category? category = await _context.Categories.FirstOrDefaultAsync(i => i.Id == id);

            if (category == null) return NotFound(new { message = "Category not found." });

            category.Name = dto.Name;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Category updated." });
        }

        // ВИДАЛЕННЯ КАТЕГОРІЇ 
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            // Знаходимо нашу категорію
            Category? category = await _context.Categories.FirstOrDefaultAsync(i => i.Id == id);

            if (category == null) return NotFound(new { message = "Category not found." });

            _context.Categories.Remove(category);

            await _context.SaveChangesAsync();

            return Ok(new { message = "Category deleted." });
        }

        public IActionResult Error()
        {
            return View(
                new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                }
            );
        }
    }
}
