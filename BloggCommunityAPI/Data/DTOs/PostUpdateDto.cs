namespace BloggCommunityAPI.Data.DTOs
{
    public class PostUpdateDto
    {
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public int CategoryId { get; set; }
    }
}
