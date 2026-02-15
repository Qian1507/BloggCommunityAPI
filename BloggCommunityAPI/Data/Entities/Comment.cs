using System.ComponentModel.DataAnnotations;

namespace BloggCommunityAPI.Data.Entities
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [Required,MaxLength(500)]
        public string Text { get; set; }=string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Required]
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        [Required]
        public int BlogPostId { get; set; }
        public BlogPost BlogPost{get; set;}=null!;
    }
}
