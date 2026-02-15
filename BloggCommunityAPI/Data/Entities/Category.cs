using System.ComponentModel.DataAnnotations;

namespace BloggCommunityAPI.Data.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required,MaxLength(50)]
        public string Name { get; set; }=string.Empty;

        public string? Description { get; set; }

        public List<BlogPost> BlogPosts { get; set; } = new();
    }
}
