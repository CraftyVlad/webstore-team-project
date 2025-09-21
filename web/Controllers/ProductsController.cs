using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web.Models;
using web.Models.Entities;

namespace web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ILogger<ProductsController> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }

        
        public IActionResult GetProducts()
        {
            List<Item> products = _context.Items.ToList();

            return View(products);
        }

        [Route("GetProducts/{id:int}")]
		public IActionResult GetProducts(int id)
		{
            Item product = _context.Items
                .Include(p => p.Reviews)
                    .ThenInclude(r => r.Replies)!
                        .ThenInclude(c => c.Replies)
                .FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            foreach (var review in product.Reviews)
            {
                review.Replies = review.Replies
                    .Where(c => c.ParentId == null)
                    .ToList();
            }

            return View("GetProduct", product);
		}



		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
