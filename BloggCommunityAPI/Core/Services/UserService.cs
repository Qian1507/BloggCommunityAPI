using BloggCommunityAPI.Core.Interfaces;
using BloggCommunityAPI.Data.DTOs;
using BloggCommunityAPI.Data.Entities;
using BloggCommunityAPI.Data.Interfaces;

namespace BloggCommunityAPI.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly ITokenService _tokenService;

        public UserService(IUserRepo userRepo, ITokenService tokenService)
        {
            _userRepo = userRepo;
            _tokenService = tokenService;
        }
        public async Task<User?> RegisterAsync(RegisterDto registerDto)
        {

            if(await _userRepo.UserExistsAsync(registerDto.Email, registerDto.Username))
                return null;


            var user = new User
            {
                UserName = registerDto.Username,
                Email = registerDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password)
            };

            await _userRepo.CreateAsync(user);
            await _userRepo.SaveChangesAsync();

            return user;
        }
        public async Task<string?> LoginAsync(LoginDto loginDto)
        {
            var user = await _userRepo.GetByEmailAsync(loginDto.Email);
            if (user == null) return null;

            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
                return null;
            return _tokenService.CreateToken(user);

        }
        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            if (user == null) return false;

            _userRepo.Delete(user);
            return await _userRepo.SaveChangesAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _userRepo.GetByIdAsync(id);
        }

       
        public async Task<bool> UpdateUserAsync(int id, UserUpdateDto dto)
        {
            var user = await _userRepo.GetByIdAsync(id);
            if (user == null) return false;

            var email= dto.Email;
            var userName= dto.UserName;

            var exists = await _userRepo.UserExistsAsync(email, userName);
            if (exists && (user.Email != email || user.UserName != userName))
                return false;

            user.UserName = dto.UserName;
            user.Email = dto.Email;

            _userRepo.Update(user);
            return await _userRepo.SaveChangesAsync();
        }

    }

}
