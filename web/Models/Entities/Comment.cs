namespace web.Models.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public Review Review { get; set; }
        public int ReviewId { get; set; }
        public Comment? Parent { get; set; }
        public int? ParentId { get; set; }
        public List<Comment>? Replies { get; set; } = new List<Comment>();
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
