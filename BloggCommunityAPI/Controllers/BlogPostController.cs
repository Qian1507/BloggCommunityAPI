using BloggCommunityAPI.Core.Interfaces;
using BloggCommunityAPI.Core.Services;
using BloggCommunityAPI.Data.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BloggCommunityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private readonly IBlogPostService _blogPostService;

        public BlogPostController(IBlogPostService blogPostService)
        {
            _blogPostService = blogPostService;
        }




        // GET: api/BlogPost
        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _blogPostService.GetAllPostsAsync();
            return Ok(posts);
        }

        // GET: api/BlogPost/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var post = await _blogPostService.GetPostByIdAsync(id);
            if (post == null) return NotFound();
            return Ok(post);
        }

        // GET: api/BlogPost/by-user/3
        [HttpGet("by-user/{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            var posts = await _blogPostService.GetPostsByUserIdAsync(userId);
            return Ok(posts);
        }

        // GET: api/BlogPost/by-category/2
        [HttpGet("by-category/{categoryId}")]
        public async Task<IActionResult> GetByCategory(int categoryId)
        {
            var posts = await _blogPostService.GetPostsByCategoryIdAsync(categoryId);
            return Ok(posts);
        }

        // GET: api/BlogPost/search?title=xxx
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string title)
        {
            if (string.IsNullOrWhiteSpace(title)) return BadRequest("Search title cannot be empty");
            var posts = await _blogPostService.SearchPostsByTitleAsync(title);
            return Ok(posts);
        }

   

        // POST: api/BlogPost
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] PostCreateDto dto)
        {
            var userId = GetCurrentUserId();
            if(userId == null) return Unauthorized();

            
            var post = await _blogPostService.CreatePostAsync(userId.Value, dto);
            if (post == null) return BadRequest("Category not found or failed to create post");

            return CreatedAtAction(nameof(GetById), new { id = post.Id }, post);
        }

        // PUT: api/BlogPost/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] PostUpdateDto dto)
        {
            

            var userId = GetCurrentUserId()  ;
            if(userId ==null) return Unauthorized();

            var success = await _blogPostService.UpdatePostAsync(id, userId.Value, dto);

            if (!success) return Forbid(); 
            return NoContent();
        }

        // DELETE: api/BlogPost/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetCurrentUserId() ;
            if(userId == null) return Unauthorized();

            var success = await _blogPostService.DeletePostAsync(id, userId.Value);

            if (!success) return NotFound(); 
            return NoContent();
        }

        //Helper method to extract UserId from JWT safely
        private int? GetCurrentUserId()
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (idClaim == null) return null;

            return int.TryParse(idClaim.Value, out int id) ? id : null;
        }
    }

}

