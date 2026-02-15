using BloggCommunityAPI.Data.Entities;
using BloggCommunityAPI.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BloggCommunityAPI.Data.Repos
{
    public class BlogPostRepo:IBlogPostRepo
    {
        private readonly BlogDbContext _context;

        public BlogPostRepo(BlogDbContext context)
        {
            _context = context;
        }

        public async Task<BlogPost?> GetByIdAsync(int id)
        {
            return await _context.BlogPosts
                .Include(p => p.User)
                .Include(p => p.Category)
                .Include(p => p.Comments)
                    .ThenInclude(c => c.User) 
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<BlogPost>> GetAllPostsAsync()
        {
            return await _context.BlogPosts
                .Include(p => p.User)
                .Include(p => p.Category)
                .OrderByDescending(p => p.CreatedAt) 
                .ToListAsync();
        }

        public async Task<IEnumerable<BlogPost>> GetByUserIdAsync(int userId)
        {
            return await _context.BlogPosts
                .Include(p => p.User)
                .Include(p => p.Category)
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<BlogPost>> GetByCategoryIdAsync(int categoryId)
        {
            return await _context.BlogPosts
                .Include(p => p.User)
                .Include(p => p.Category)
                .Where(p => p.CategoryId == categoryId)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<BlogPost>> SearchByTitleAsync(string title)
        {
            return await _context.BlogPosts
                .Include(p => p.User)
                .Include(p => p.Category)
                .Where(p => p.Title.Contains(title))
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task CreateAsync(BlogPost post)
        {
            await _context.BlogPosts.AddAsync(post);
        }

        public void Update(BlogPost post)
        {
            _context.BlogPosts.Update(post);
        }

        public void Delete(BlogPost post)
        {
            _context.BlogPosts.Remove(post);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }



}

