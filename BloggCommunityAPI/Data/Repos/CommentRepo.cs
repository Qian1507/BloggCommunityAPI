using BloggCommunityAPI.Data.Entities;
using BloggCommunityAPI.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BloggCommunityAPI.Data.Repos
{
    public class CommentRepo:ICommentRepo
    {
        private readonly BlogDbContext _context;

        public CommentRepo(BlogDbContext context)
        {
            _context = context;
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comments
                .Include(c => c.User)
                .Include(c => c.BlogPost)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Comment>> GetByPostIdAsync(int postId)
        {
            return await _context.Comments
                .Include(c => c.User)
                .Where(c => c.BlogPostId == postId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }
       
        public async Task CreateAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
        }

        public void Delete(Comment comment)
        {
            _context.Comments.Remove(comment);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
