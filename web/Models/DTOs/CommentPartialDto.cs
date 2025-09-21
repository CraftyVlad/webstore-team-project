using web.Models.Entities;

namespace web.Models.DTOs;

public class CommentPartialDto
{
	public List<Comment> Comments { get; set; } = default!;
	public int Margin { get; set; } = 4;
}