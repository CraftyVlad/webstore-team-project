using System.ComponentModel.DataAnnotations;

namespace web.Models.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Owner { get; set; }
        [Range(0, 10)]
        public int Rating { get; set; }
        public Item Item { get; set; }
        public int ItemId { get; set; }
        public List<Comment>? Replies { get; set; } = new List<Comment>();
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
