namespace BloggCommunityAPI.Data.DTOs
{
    public class CommentResponseDto
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public int UserId { get; set; }
        public int BlogPostId { get; set; }
    }
}
