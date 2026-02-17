using BloggCommunityAPI.Core.Interfaces;
using BloggCommunityAPI.Data.Entities;
using BloggCommunityAPI.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BloggCommunityAPI.Data.Repos
{
    public class CategoryRepo:ICategoryRepo
    {
        private readonly BlogDbContext _context;

        public CategoryRepo(BlogDbContext context)
        {
            _context = context;
        }

     
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id);
        }


        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}



