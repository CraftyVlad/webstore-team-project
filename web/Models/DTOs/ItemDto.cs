using Microsoft.AspNetCore.Mvc;
using web.Models.Entities;

namespace web.Models.DTOs
{
    public class ItemDto
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> Images { get; set; }
        public List<CategoryDto>? Categories { get; set; } = new List<CategoryDto>();
        public string Owner { get; set; }
        public decimal PriceEth { get; set; }
        public int Stock { get; set; }
    }
}
