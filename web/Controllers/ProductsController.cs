using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web.Models;
using web.Models.DTOs;
using web.Models.Entities;

namespace web.Controllers
{
    [Route("products")]
    public class ProductsController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ILogger<ProductsController> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }

        // ДЛЯ ГОЛОВНОЇ СТОРІНКИ
        public IActionResult GetProducts()
        {
            List<Item> products = _context.Items.ToList();

            return View(products);
        }

        // ДЛЯ СТОРІНКИ ПРОДУКТУ
        [Route("GetProducts/{id:int}")]
        public IActionResult GetProduct(int id)
        {
            Item product = _context
                .Items.Include(p => p.Reviews)
                .ThenInclude(r => r.Replies)!
                .ThenInclude(c => c.Replies)
                .FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();

            foreach (var review in product.Reviews)
            {
                review.Replies = review.Replies.Where(c => c.ParentId == null).ToList();
            }

            return View("GetProduct", product);
        }

        // ДОДАВАННЯ ПРОДУКТУ 
        [HttpPost("add")]
        public async Task<IActionResult> AddProduct([FromBody] ItemDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Створюємо новий продукт
            var product = new Item
            {
                Owner = dto.Owner,
                Title = dto.Title,
                Description = dto.Description,
                PriceEth = dto.PriceEth,
                Stock = dto.Stock,
                Images = dto.Images,
            };


            // Додаєм категорії, створюючи зв'язки ItemCategory у бд (автоматично)
            if (dto.Categories == null)
            {
                foreach (var c in dto.Categories!)
                {
                    var category = await _context.Categories
                        .FirstOrDefaultAsync(x => x.Name == c.Name);

                    if (category != null)
                    {
                        product.Categories!.Add(category);
                    }
                }
            }

            _context.Items.Add(product);

            await _context.SaveChangesAsync();

            return Ok(new { message = "Product added." });
        }

        // ЗМІНА ПРОДУКТУ 
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ItemDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            // Знаходимо НАШ продукт
            Item? product = await _context.Items.FirstOrDefaultAsync(i => i.Id == id);

            if (product == null) return NotFound(new { message = "Product not found." });

            product.Title = dto.Title;
            product.Description = dto.Description;
            product.PriceEth = dto.PriceEth;
            product.Stock = dto.Stock;
            product.Images = dto.Images;

            // Зміна категорій
            var categories_ids = dto.Categories.Select(c => c.Id);
            product.Categories.RemoveAll(c => !categories_ids.Contains(c.Id));

            foreach (var i in categories_ids)
            {
                if (product.Categories.Any(c => c.Id == i))
                {
                    var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == i);
                    if (category != null)
                        product.Categories.Add(category);
                }
            }

            await _context.SaveChangesAsync();

            return Ok(new { message = "Product updated." });
        }

        // ВИДАЛЕННЯ ПРОДУКТУ 
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            // Знаходимо НАШ продукт
            Item? product = await _context.Items.FirstOrDefaultAsync(i => i.Id == id);

            if (product == null) return NotFound(new { message = "Product not found." });

            _context.Items.Remove(product);

            await _context.SaveChangesAsync();

            return Ok(new { message = "Product deleted." });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
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
