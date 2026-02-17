using BloggCommunityAPI.Data.Entities;

namespace BloggCommunityAPI.Data.Interfaces
{
    public interface ICommentRepo
    {
        
       

        Task CreateAsync(Comment comment);
        void Delete(Comment comment);
        Task<bool> SaveChangesAsync();
    }
}
