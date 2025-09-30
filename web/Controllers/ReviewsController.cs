using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web.Models;
using web.Models.DTOs;
using web.Models.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace web.Controllers
{
    [Route("reviews")]
    public class ReviewsController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<ProductsController> _logger;

        public ReviewsController(ILogger<ProductsController> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }


        // ДОДАВАННЯ РЕВ'Ю
        [HttpPost("add")]
        public async Task<IActionResult> AddReview([FromBody] ReviewDto dto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Знаходимо продукт
            var product = await _context
                .Items.Include(p => p.Reviews)
                .FirstOrDefaultAsync(p => p.Id == dto.ItemId);

            if (product == null)
                return NotFound("Product not found.");

            // Створюємо новий відгук
            var review = new Review
            {
                Owner = dto.Owner,
                Content = dto.Content,
                Rating = dto.Rating,
                CreatedAt = DateTime.UtcNow,
            };

            product.Reviews.Add(review);

            await _context.SaveChangesAsync();

            return Ok(new { message = "Review added." });
        }

        // Чесно не знаю чи треба міняти рев'ю, тіпа ти перше поставив 10/10 і написав що класно, а потім ставиш 1/10 і пишеш що це кал
        // ЗМІНА РЕВ'Ю 
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateReview(int id, [FromBody] ReviewDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            // Знаходимо неше рев'ю
            Review? review = await _context.Reviews.FirstOrDefaultAsync(i => i.Id == id);

            if (review == null) return NotFound(new { message = "Review not found." });

            review.Rating = dto.Rating; // ?
            review.Content = dto.Content;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Review updated." });
        }

        // ВИДАЛЕННЯ РЕВ'Ю 
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            // Знаходимо неше рев'ю
            Review? review = await _context.Reviews.FirstOrDefaultAsync(i => i.Id == id);

            if (review == null) return NotFound(new { message = "Review not found." });

            _context.Reviews.Remove(review);

            await _context.SaveChangesAsync();

            return Ok(new { message = "Review deleted." });
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
