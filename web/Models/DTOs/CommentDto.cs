namespace web.Models.DTOs
{
    public class CommentDto
    {
        public string Content { get; set; }
        public string Owner { get; set; }
        public int ReviewId { get; set; }
        public int? ParentId { get; set; }
    }
}
