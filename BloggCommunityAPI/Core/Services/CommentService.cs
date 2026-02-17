using BloggCommunityAPI.Core.Interfaces;
using BloggCommunityAPI.Data.DTOs;
using BloggCommunityAPI.Data.Entities;
using BloggCommunityAPI.Data.Interfaces;
using BloggCommunityAPI.Data.Repos;

namespace BloggCommunityAPI.Core.Services
{
    public class CommentService:ICommentService
    {
        private readonly ICommentRepo _commentRepo;
        private readonly IBlogPostRepo _postRepo;

        public CommentService(ICommentRepo commentRepo,IBlogPostRepo postRepo)
        {
            _commentRepo = commentRepo;
            _postRepo = postRepo;
        }
        //public async Task<IEnumerable<CommentResponseDto>> GetCommentsByPostIdAsync(int postId)
        //{
        //    var comments = await _commentRepo.GetByPostIdAsync(postId);
        //    return comments.Select(c => new CommentResponseDto
        //    {
        //        Id = c.Id,
        //        Text = c.Text,
        //        UserName = c.User?.UserName ?? "Anonymous",
        //        CreatedAt = c.CreatedAt,
        //        UserId = c.UserId,
        //        BlogPostId = c.BlogPostId,
        //    });
        //}
        public async Task<CommentResponseDto?> CreateCommentAsync(int userId, CommentCreateDto dto)
        {
            var post = await _postRepo.GetByIdAsync(dto.PostId);

          
            if (post == null) return null;

          //Användare kan inte kommentera sina egna inlägg
            if (post.UserId == userId)
            {
                return null; 
            }
            
            var now = DateTime.UtcNow;
            var comment = new Comment
            {
                Text = dto.Text,
                BlogPostId = dto.PostId,
                UserId = userId,
                CreatedAt=now,
            };

            await _commentRepo.CreateAsync(comment);
            await _commentRepo.SaveChangesAsync();

            return new CommentResponseDto
            {
                Id = comment.Id,
                Text = comment.Text,
                UserName = "Me", 
                CreatedAt = now,
            };
        }
        //public async Task<bool> DeleteCommentAsync(int commentId, int userId)
        //{
        //    var comment = await _commentRepo.GetByIdAsync(commentId);
     
        //    if (comment == null || comment.UserId != userId) return false;

        //    _commentRepo.Delete(comment);
        //    return await _commentRepo.SaveChangesAsync();
        //}

    }
}
