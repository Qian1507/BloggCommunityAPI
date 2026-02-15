using BloggCommunityAPI.Data.Entities;

namespace BloggCommunityAPI.Data.Interfaces
{
    public interface ICommentRepo
    {
        Task<Comment?> GetByIdAsync(int id);
        Task<IEnumerable<Comment>> GetByPostIdAsync(int postId);

        Task CreateAsync(Comment comment);
        void Delete(Comment comment);
        Task<bool> SaveChangesAsync();
    }
}
