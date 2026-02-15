using System.ComponentModel.DataAnnotations;

namespace BloggCommunityAPI.Data.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string UserName { get; set; } = string.Empty;
        [Required, MaxLength(100)]
        public string Email { get; set; } =string.Empty;
        [Required]
        public string PasswordHash { get; set; }=string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<BlogPost> BlogPosts { get; set; } = new();

        public List<Comment> Comments { get; set; } = new();

    }
}
