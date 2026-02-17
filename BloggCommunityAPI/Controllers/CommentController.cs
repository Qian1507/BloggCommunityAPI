using BloggCommunityAPI.Core.Interfaces;
using BloggCommunityAPI.Core.Services;
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
        private readonly IUserService _userService;

        public CommentController(ICommentService commentService, IUserService userService)
        {
            _commentService = commentService;
            _userService = userService;
        }



        [HttpPost("Create")]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CommentCreateDto dto)
        {
            var userId = GetCurrentUserId();
            if (userId == null) return Unauthorized();

            var user = await _userService.GetByIdAsync(userId.Value);
            if (user == null) return Unauthorized("Account inactive.");


            var result = await _commentService.CreateCommentAsync(userId.Value, dto);

            if (result == null)
            {
                
                return BadRequest("Invalid request: Post not found or you cannot comment on your own post.");
            }

            return Ok(result);
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

