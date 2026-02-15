using BloggCommunityAPI.Data.DTOs;

namespace BloggCommunityAPI.Core.Interfaces
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentResponseDto>> GetCommentsByPostIdAsync(int postId);
        Task<CommentResponseDto?> CreateCommentAsync(int userId, CommentCreateDto dto);
        Task<bool> DeleteCommentAsync(int commentId, int userId);
    }
}
