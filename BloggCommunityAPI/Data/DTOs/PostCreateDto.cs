namespace BloggCommunityAPI.Data.DTOs
{
    public class PostCreateDto
    {
        public string Title { get; set; }=string.Empty;

        public string Text {  get; set; }=string.Empty;

        public int CategoryId { get; set; }
    }
}
