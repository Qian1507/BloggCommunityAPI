using BloggCommunityAPI.Data.DTOs;
using BloggCommunityAPI.Data.Entities;
using BloggCommunityAPI.Data.Interfaces;

namespace BloggCommunityAPI.Core.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<CategoryDto?> GetByIdAsync(int id);

    }
}
