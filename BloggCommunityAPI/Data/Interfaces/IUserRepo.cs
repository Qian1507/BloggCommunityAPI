using BloggCommunityAPI.Data.Entities;

namespace BloggCommunityAPI.Data.Interfaces
{
    public interface IUserRepo
    {
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByUsernameAsync(string username);
       
        Task CreateAsync(User user);
        void Update(User user);
        void Delete(User user);
        Task<bool> SaveChangesAsync();
        Task<bool> UserExistsAsync(string email, string username);
    }
}
