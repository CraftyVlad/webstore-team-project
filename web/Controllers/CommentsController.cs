using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web.Models;
using web.Models.DTOs;
using web.Models.Entities;

namespace web.Controllers
{
    [Route("comments")]
    public class CommentsController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<ProductsController> _logger;

        public CommentsController(ILogger<ProductsController> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }

        // ДОДАВАННЯ КОМЕНТАРЯ
        [HttpPost("add")]
        public async Task<IActionResult> AddComment([FromBody] CommentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Знаходимо рев'ю
            var review = await _context
                .Reviews.Include(r => r.Replies)
                .FirstOrDefaultAsync(r => r.Id == dto.ReviewId);

            if (review == null)
                return NotFound("Review not found.");

            Comment parent = null;

            // Знаходимо комент на який відповідаємо
            if (dto.ParentId != null)
            {
                parent = await _context
                    .Comments.Include(c => c.Replies)
                    .FirstOrDefaultAsync(c => c.Id == dto.ReviewId);

                if (review == null)
                    return NotFound("Comment to reply not found.");
            }
            

            // Створюємо новий коммент
            var comment = new Comment
            {
                Owner = dto.Owner,
                Content = dto.Content,
                ParentId = dto.ParentId,
                CreatedAt = DateTime.UtcNow,
            };

            review.Replies.Add(comment);
            if (parent != null) { parent.Replies.Add(comment); }

            await _context.SaveChangesAsync();

            return Ok(new { message = "Comment added." });
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
