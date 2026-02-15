using BloggCommunityAPI.Data.Entities;

namespace BloggCommunityAPI.Data.Interfaces
{
    public interface IBlogPostRepo
    {
        Task<BlogPost?> GetByIdAsync(int id);
        Task<IEnumerable<BlogPost>> GetAllPostsAsync();
        Task<IEnumerable<BlogPost>> GetByUserIdAsync(int userId);
        Task<IEnumerable<BlogPost>> GetByCategoryIdAsync(int categoryId);
        Task<IEnumerable<BlogPost>> SearchByTitleAsync(string title);

        Task CreateAsync(BlogPost post);
        void Update(BlogPost post);
        void Delete(BlogPost post);
        Task<bool> SaveChangesAsync();


    }
}
