using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
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

        // ДОДАВАННЯ КОМЕНТАРЯ
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
