namespace BloggCommunityAPI.Data.DTOs
{
    public class CommentCreateDto
    {
        public int PostId { get; set; }

        public string Text { get; set; } = string.Empty;
    }
}
