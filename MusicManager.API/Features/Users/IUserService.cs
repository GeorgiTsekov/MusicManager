using Microsoft.AspNetCore.Identity;
using MusicManager.API.Features.Users.Models;

namespace MusicManager.API.Features.Users
{
    public interface IUserService
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
        Task<UserDetailsServiceModel> UserDetails(string id);
        string GetCurrentUserId();
        Task<UserDetailsServiceModel> GetCurrentUserDetails();
    }
}
