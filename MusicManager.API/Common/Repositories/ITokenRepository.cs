using Microsoft.AspNetCore.Identity;

namespace MusicManager.API.Common.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
