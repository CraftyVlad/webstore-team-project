using System.ComponentModel.DataAnnotations.Schema;

namespace web.Models.Entities
{
    public class ItemCategory
    {
        [ForeignKey("Item")]
        public int ItemId { get; set; }
        public Item Item { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
