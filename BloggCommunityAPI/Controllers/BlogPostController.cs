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
        private readonly IUserService _userService;

        public BlogPostController(IBlogPostService blogPostService, IUserService userService)
        {
            _blogPostService = blogPostService;
            _userService = userService;
        }






        // GET: api/BlogPost/Getall
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _blogPostService.GetAllPostsAsync();
            return Ok(posts);
        }


        // GET: api/BlogPost/by-id/{id}
       [HttpGet("By-Id/{id}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> GetById(int id)
        {
            var post = await _blogPostService.GetPostByIdAsync(id);
            if (post == null) return NotFound();
            return Ok(post);
        }



        

        // GET: api/BlogPost/Filter/{categoryId}
        [HttpGet("Filter/{categoryId}")]
        public async Task<IActionResult> GetByCategory(int categoryId)
        {
            var posts = await _blogPostService.GetPostsByCategoryIdAsync(categoryId);
            return Ok(posts);
        }



      
        // GET: api/BlogPost/search?title=xxx
        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] string title)
        {
            if (string.IsNullOrWhiteSpace(title)) return BadRequest("Search title cannot be empty");
            var posts = await _blogPostService.SearchPostsByTitleAsync(title);
            return Ok(posts);
        }

        

        // POST: api/BlogPost/create
        [HttpPost("Create")]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] PostCreateDto dto)
        {
            var userId = GetCurrentUserId();
            if(userId == null) return Unauthorized();

            var user = await _userService.GetByIdAsync(userId!.Value);
            if (user == null) return Unauthorized("Account no longer exists.");

            var response = await _blogPostService.CreatePostAsync(userId.Value, dto);
            if (response == null) return BadRequest("Category not found or failed to create post");

           return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
           
        }

        

        // PUT: api/BlogPost/update/{id}
        [HttpPut("Update/{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] PostUpdateDto dto)
        {

            var userId = GetCurrentUserId()  ;
            if(userId ==null) return Unauthorized();

            var user = await _userService.GetByIdAsync(userId.Value);
            if (user == null) return Unauthorized();

            var post = await _blogPostService.GetPostByIdAsync(id);
            if (post == null) return NotFound($"Post with ID {id} not found.");

            
            if (post.UserId != userId.Value) return Forbid();

            var success = await _blogPostService.UpdatePostAsync(id, userId.Value, dto);

            if (!success) return BadRequest("Update failed.");
            return NoContent();
        }



        

        // DELETE: api/BlogPost/delete/{id}
        [HttpDelete("Delete/{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetCurrentUserId() ;
            if(userId == null) return Unauthorized();

            var post = await _blogPostService.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound($"Post with ID {id} not found.");
            }
            if (post.UserId != userId.Value)
            {
                return Forbid(); 
            }
            var success = await _blogPostService.DeletePostAsync(id, userId.Value);

            if (!success) return BadRequest("Could not delete post."); 
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

