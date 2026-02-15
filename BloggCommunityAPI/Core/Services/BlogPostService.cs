using BloggCommunityAPI.Core.Interfaces;
using BloggCommunityAPI.Data.DTOs;
using BloggCommunityAPI.Data.Entities;
using BloggCommunityAPI.Data.Interfaces;

namespace BloggCommunityAPI.Core.Services
{
    public class BlogPostService:IBlogPostService
    {
        
        private readonly IBlogPostRepo _blogPostRepo;
        private readonly ICategoryRepo _categoryRepo; 

        public BlogPostService(IBlogPostRepo blogPostRepo, ICategoryRepo categoryRepo)
        {
            _blogPostRepo = blogPostRepo;
            _categoryRepo = categoryRepo;
        }

        public async Task<BlogPost?> CreatePostAsync(int userId, PostCreateDto dto)
        {
            var category = await _categoryRepo.GetByIdAsync(dto.CategoryId);
            if (category == null) return null;

            var post = new BlogPost
            {
                Title = dto.Title,
                Text = dto.Text,
                UserId = userId,
                CategoryId = dto.CategoryId,
                CreatedAt = DateTime.UtcNow
            };

            await _blogPostRepo.CreateAsync(post);
            var saved = await _blogPostRepo.SaveChangesAsync();
            if (!saved) return null;

            return post;
        }

        public async Task<bool> UpdatePostAsync(int postId, int userId, PostUpdateDto dto)
        {
            var post = await _blogPostRepo.GetByIdAsync(postId);
            if (post == null) return false;
            if (post.UserId != userId) return false;

            post.Title = dto.Title;
            post.Text = dto.Text;
            post.CategoryId = dto.CategoryId;
            post.UpdatedAt = DateTime.UtcNow;

            _blogPostRepo.Update(post);
            return await _blogPostRepo.SaveChangesAsync();
        }

        public async Task<bool> DeletePostAsync(int postId, int userId)
        {
            var post = await _blogPostRepo.GetByIdAsync(postId);
            if (post == null) return false;
            if (post.UserId != userId) return false;

            _blogPostRepo.Delete(post);
            return await _blogPostRepo.SaveChangesAsync();
        }

        public async Task<BlogPost?> GetPostByIdAsync(int id)
        {
            return await _blogPostRepo.GetByIdAsync(id);
        }

        public async Task<IEnumerable<BlogPost>> GetAllPostsAsync()
        {
            return await _blogPostRepo.GetAllPostsAsync();
        }

        public async Task<IEnumerable<BlogPost>> GetPostsByUserIdAsync(int userId)
        {
            return await _blogPostRepo.GetByUserIdAsync(userId);
        }

        public async Task<IEnumerable<BlogPost>> GetPostsByCategoryIdAsync(int categoryId)
        {
            return await _blogPostRepo.GetByCategoryIdAsync(categoryId);
        }

        public async Task<IEnumerable<BlogPost>> SearchPostsByTitleAsync(string title)
        {
            return await _blogPostRepo.SearchByTitleAsync(title);
        }

    }
}
