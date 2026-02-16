using BloggCommunityAPI.Core.Interfaces;
using BloggCommunityAPI.Data.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BloggCommunityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }


        // GET: api/Comment/GetBypPostId
        [HttpGet("GetByPostId/{postId}")]
        public async Task<IActionResult> GetByPost(int postId)
        {
            var comments = await _commentService.GetCommentsByPostIdAsync(postId);
            return Ok(comments);
        }

        // POST: api/Comment/Create
        [HttpPost("Create")]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CommentCreateDto dto)
        {
            var userId = GetCurrentUserId();
            if (userId == null) return Unauthorized();

            var result = await _commentService.CreateCommentAsync(userId.Value, dto);

            if (result == null)
            {
                // This covers: Post not found OR user trying to comment on their own post
                return BadRequest("Invalid request: Post not found or you cannot comment on your own post.");
            }

            return Ok(result);
        }

        // DELETE: api/Comment/Delete
        [HttpDelete("Delete/{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetCurrentUserId();
            if (userId == null) return Unauthorized();

            var success = await _commentService.DeleteCommentAsync(id, userId.Value);

            if (!success) return Forbid(); // User is not the author or comment doesn't exist

            return NoContent();
        }

        private int? GetCurrentUserId()
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (idClaim != null && int.TryParse(idClaim.Value, out int id))
            {
                return id;
            }
            return null;
        }
    }


}

