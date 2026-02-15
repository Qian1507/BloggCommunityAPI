using BloggCommunityAPI.Data.Entities;

namespace BloggCommunityAPI.Core.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
