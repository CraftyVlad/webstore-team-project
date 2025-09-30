public class ReviewDto
{
    public int? Id { get; set; }
    public int ItemId { get; set; }
    public string Owner { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public int Rating { get; set; }
}
