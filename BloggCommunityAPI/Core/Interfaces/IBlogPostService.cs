using BloggCommunityAPI.Data.DTOs;
using BloggCommunityAPI.Data.Entities;
namespace BloggCommunityAPI.Core.Interfaces
{
    public interface IBlogPostService
    {

        Task<BlogPost?> CreatePostAsync(int userId, PostCreateDto dto);
        Task<bool> UpdatePostAsync(int postId, int userId, PostUpdateDto dto);
        Task<bool> DeletePostAsync(int postId, int userId);

        Task<BlogPost?> GetPostByIdAsync(int id);
        Task<IEnumerable<BlogPost>> GetAllPostsAsync();
        Task<IEnumerable<BlogPost>> GetPostsByUserIdAsync(int userId);
        Task<IEnumerable<BlogPost>> GetPostsByCategoryIdAsync(int categoryId);
        Task<IEnumerable<BlogPost>> SearchPostsByTitleAsync(string title);

    }
}
