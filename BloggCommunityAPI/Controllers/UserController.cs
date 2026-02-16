using BloggCommunityAPI.Core.Interfaces;
using BloggCommunityAPI.Data.DTOs;
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



        // GET api/User/by-id/{id}

        [HttpGet("by-id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();

            return Ok(new { user.Id, user.UserName, user.Email });
        }



        // GET api/User/all

        [HttpGet("All")]
        [ProducesResponseType(StatusCodes.Status200OK)]              
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users.Select(u => new { u.Id, u.UserName, u.Email }));
        }



        // POST api/User/register

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]             
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var user = await _userService.RegisterAsync(registerDto);
            if (user == null) return BadRequest("Username or Email already exists.");

            return Ok(new { message = "Registration successful", userId = user.Id });
        }



        // POST api/User/login

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]             
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var token = await _userService.LoginAsync(loginDto);
            if (token == null) return Unauthorized("Invalid email or password.");

            return Ok(new { Token = token });
        }



        // DELETE api/User/delete/{id}
        [HttpDelete("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]    
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _userService.DeleteUserAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }


        // PUT api/User/update/{id}

        [HttpPut("update/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]          
        [ProducesResponseType(StatusCodes.Status400BadRequest)]         
        [ProducesResponseType(StatusCodes.Status404NotFound)]           
        public async Task<IActionResult> Update(int id, UserUpdateDto dto)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();

            var success = await _userService.UpdateUserAsync(id, dto.UserName, dto.Email);
            if (!success) return BadRequest("Username or Email already exists.");

            return NoContent();
        }


    }
}
