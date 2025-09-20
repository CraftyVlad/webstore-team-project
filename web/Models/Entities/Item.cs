namespace web.Models.Entities
{
    public class Item
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> Images { get; set; }
        public string Owner { get; set; }
        public List<string>? Buyers { get; set; }
        public List<Category>? Categories { get; set; } = new List<Category>();
        public List<ItemCategory>? ItemCategories { get; set; } = new List<ItemCategory>();
        public List<Review>? Reviews { get; set; }
        public decimal PriceEth { get; set; }
        public int Stock { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
