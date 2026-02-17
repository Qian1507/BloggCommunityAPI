using BloggCommunityAPI.Core.Interfaces;
using BloggCommunityAPI.Data.DTOs;
using BloggCommunityAPI.Data.Entities;
using BloggCommunityAPI.Data.Interfaces;

namespace BloggCommunityAPI.Core.Services
{
    public class CategoryService:ICategoryService
    {
        private readonly ICategoryRepo _categoryRepo;

        public CategoryService(ICategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var categories = await _categoryRepo.GetAllAsync();
            return categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name
            });
        }

        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            var category = await _categoryRepo.GetByIdAsync(id);
            if (category == null) return null;

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }

     
    }
}
