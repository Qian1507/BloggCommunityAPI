using System.ComponentModel.DataAnnotations;

namespace BloggCommunityAPI.Data.Entities
{
    public class BlogPost
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Text {  get; set; }= string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        [Required]
        public int CategoryId { get; set; }    
        public Category Category { get; set; }

        public List<Comment> Comments { get; set; } = new();

    }
}
