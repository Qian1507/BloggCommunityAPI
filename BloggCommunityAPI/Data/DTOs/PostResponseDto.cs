namespace BloggCommunityAPI.Data.DTOs
{
    public class PostResponseDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Text {  get; set; }  =string.Empty;

        public DateTime CreatedAt { get; set; }= DateTime.UtcNow;

        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }

        public int UserId { get; set; }
        public string? UserName { get; set; }

        public List<CommentResponseDto> Comments { get; set; } = new();
    }
}
