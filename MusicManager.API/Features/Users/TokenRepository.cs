using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MusicManager.API.Data;
using MusicManager.API.Features.Users.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MusicManager.API.Features.Users
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration configuration;
        private readonly MusicManagerDbContext data;
        private readonly ClaimsPrincipal user;

        public TokenRepository(IConfiguration configuration, MusicManagerDbContext data, IHttpContextAccessor httpContextAccessor)
        {
            this.configuration = configuration;
            this.data = data;
            this.user = httpContextAccessor.HttpContext?.User;
        }

        public string CreateJWTToken(IdentityUser user, List<string> roles)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Email, user.Email));

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<UserDetailsServiceModel> UserDetails(string email)
        {
            var user = this.data
                .Users
                .Where(u => u.Email == email)
                .Select(t => new UserDetailsServiceModel
                {
                    Id = t.Id,
                    UserName = t.UserName,
                    Email = t.Email
                })
                .FirstOrDefaultAsync();

            return await user;
        }

        public string GetCurrentUserEmail()
        {
            return this.user?.FindFirst(ClaimTypes.Email)?.Value;
        }
    }
}
