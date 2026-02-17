using BloggCommunityAPI.Core.Interfaces;
using BloggCommunityAPI.Data.DTOs;
using BloggCommunityAPI.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloggCommunityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }



        // POST api/User/register

        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status200OK)]             
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var user = await _userService.RegisterAsync(registerDto);
            if (user == null) return BadRequest("Username or Email already exists.");

            return Ok(new { message = "Registration successful", userId = user.Id });
        }



        // POST api/User/login

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]             
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var token = await _userService.LoginAsync(loginDto);
            if (token == null) return Unauthorized("Invalid email or password.");

            return Ok(new { Token = token });
        }



        // DELETE api/User/delete
        [HttpDelete("Delete")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete()
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == null) return Unauthorized();

            var user = await _userService.GetByIdAsync(currentUserId.Value);
            if (user == null)
            {

                return NotFound("Your account has been deleted and cannot be updated.");
            }

            var success = await _userService.DeleteUserAsync(currentUserId.Value);
            if (!success) return NotFound();

            return NoContent();
        }


        // PUT api/User/update

        [HttpPut("Update")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update([FromBody] UserUpdateDto dto)
        {
            var userId = GetCurrentUserId();
  
            if (userId == null) return Unauthorized();

            var user = await _userService.GetByIdAsync(userId.Value);
            if (user == null)
            {
                
                return NotFound("Your account has been deleted and cannot be updated.");
            }

            var success = await _userService.UpdateUserAsync(userId.Value, dto);
            if (!success) return BadRequest("Username or Email already exists.");

            return NoContent();
        }

        //Hlep method
        private int? GetCurrentUserId()
        {
            var idClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            return (idClaim != null && int.TryParse(idClaim.Value, out int id)) ? id : null;
        }
    }
}
