using BloggCommunityAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BloggCommunityAPI.Data
{
    public class BlogDbContext : DbContext
    {

        public BlogDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<BlogPost> BlogPosts { get; set; }

        public virtual DbSet<Category> Categories { get; set; } 

        public virtual DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 🔹 User - unique email
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // 🔹 BlogPost -> User (Many to One)
            modelBuilder.Entity<BlogPost>()
                .HasOne(p => p.User)
                .WithMany(u => u.BlogPosts)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // 🔹 BlogPost -> Category (Many to One)
            modelBuilder.Entity<BlogPost>()
                .HasOne(p => p.Category)
                .WithMany(c => c.BlogPosts)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // 🔹 Comment -> User (Many to One)
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // 🔹 Comment -> BlogPost (Many to One)
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.BlogPost)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.BlogPostId)
                .OnDelete(DeleteBehavior.Cascade);
            // 🔹 Seed Categories (As required by assignment)
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Träning" },
                new Category { Id = 2, Name = "Mode" },
                new Category { Id = 3, Name = "Hälsa" }
            );
        }

    }
}
