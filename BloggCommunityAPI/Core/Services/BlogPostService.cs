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

        public async Task<PostResponseDto?> CreatePostAsync(int userId, PostCreateDto dto)
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

            post.Category = category;
            return MapToResponseDto(post);
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

        public async Task<PostResponseDto?> GetPostByIdAsync(int id)
        {
            var post = await _blogPostRepo.GetByIdAsync(id);
            return post == null ? null :MapToResponseDto(post);
           
        }

        public async Task<IEnumerable<PostResponseDto>> GetAllPostsAsync()
        {
            var posts= await _blogPostRepo.GetAllPostsAsync();
            return posts.Select(MapToResponseDto);
        }

        public async Task<IEnumerable<PostResponseDto>> GetPostsByUserIdAsync(int userId)
        {
            var posts= await _blogPostRepo.GetByUserIdAsync(userId);
            return posts.Select(MapToResponseDto);
        }

        public async Task<IEnumerable<PostResponseDto>> GetPostsByCategoryIdAsync(int categoryId)
        {
            var posts= await _blogPostRepo.GetByCategoryIdAsync(categoryId);
            return posts.Select(MapToResponseDto);
        }

        public async Task<IEnumerable<PostResponseDto>> SearchPostsByTitleAsync(string title)
        {
            var posts= await _blogPostRepo.SearchByTitleAsync(title);
            return posts.Select(MapToResponseDto);
        }



        // Helper method to keep code DRY (Don't Repeat Yourself)
        private PostResponseDto MapToResponseDto(BlogPost post)
        {
            return new PostResponseDto
            {
                Id = post.Id,
                Title = post.Title,
                Text = post.Text,
                CreatedAt = post.CreatedAt,
                CategoryId = post.CategoryId,
                CategoryName = post.Category?.Name, // Works thanks to your Repo .Include()
                UserId = post.UserId,
                UserName = post.User?.UserName
            };
        }
    }
}
