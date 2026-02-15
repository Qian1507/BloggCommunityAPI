using BloggCommunityAPI.Data.DTOs;
using BloggCommunityAPI.Data.Entities;

namespace BloggCommunityAPI.Core.Interfaces
{
    public interface IUserService
    {
        Task<User?> RegisterAsync(RegisterDto registerDto);
        Task<string?>LoginAsync(LoginDto loginDto);

        Task<bool> DeleteUserAsync(int id);

        Task<User?> GetByIdAsync(int id);         
        Task<IEnumerable<User>> GetAllUsersAsync();

        Task<bool> UpdateUserAsync(int id, string userName, string email);

    }
}
