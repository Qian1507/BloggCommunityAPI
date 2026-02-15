using BloggCommunityAPI.Data.Entities;

namespace BloggCommunityAPI.Data.Interfaces
{
    public interface ICategoryRepo
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        Task CreateAsync(Category category);

        void Delete(Category category);
        void Update(Category category);

        Task<bool> SaveChangesAsync();
    }
}
