using BloggCommunityAPI.Data.DTOs;

namespace BloggCommunityAPI.Core.Interfaces
{
    public interface ICommentService
    {
        
        Task<CommentResponseDto?> CreateCommentAsync(int userId, CommentCreateDto dto);
        
    }
}
