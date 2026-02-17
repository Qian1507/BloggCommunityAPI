using BloggCommunityAPI.Data.Entities;

namespace BloggCommunityAPI.Data.Interfaces
{
    public interface ICategoryRepo
    {
        Task<IEnumerable<Category>> GetAllAsync();

        Task<Category?> GetByIdAsync(int id);

        Task<bool> SaveChangesAsync();
    }
}
