using BloggCommunityAPI.Data.DTOs;
using BloggCommunityAPI.Data.Entities;
namespace BloggCommunityAPI.Core.Interfaces
{
    public interface IBlogPostService
    {

        Task<PostResponseDto?> CreatePostAsync(int userId, PostCreateDto dto);
        Task<bool> UpdatePostAsync(int postId, int userId, PostUpdateDto dto);
        Task<bool> DeletePostAsync(int postId, int userId);

        Task<PostResponseDto?> GetPostByIdAsync(int id);
        Task<IEnumerable<PostResponseDto>> GetAllPostsAsync();
        Task<IEnumerable<PostResponseDto>> GetPostsByUserIdAsync(int userId);
        Task<IEnumerable<PostResponseDto>> GetPostsByCategoryIdAsync(int categoryId);
        Task<IEnumerable<PostResponseDto>> SearchPostsByTitleAsync(string title);

    }
}
