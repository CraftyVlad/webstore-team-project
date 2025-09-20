namespace web.Models.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // many-to-many
        public List<Item>? Items { get; set; } = new List<Item>();
        public List<ItemCategory>? ItemCategories { get; set; } = new List<ItemCategory>();
    }
}
