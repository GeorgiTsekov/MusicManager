using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MusicManager.API.Data.Models;
using MusicManager.API.Features.Users.Models;
using MusicManager.API.Utils;

namespace MusicManager.API.Features.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public IdentityController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route(nameof(Register))]
        public async Task<IActionResult> Register([FromBody] RegisterManagerRequestDto registerManagerRequestDto)
        {
            var identityUser = new User
            {
                UserName = registerManagerRequestDto.UserName,
                Email = registerManagerRequestDto.Email,
                CreatedBy = registerManagerRequestDto.UserName,
                CreatedOn = DateTime.Now
            };

            var identityResult = await userManager.CreateAsync(identityUser, registerManagerRequestDto.Password);

            if (!identityResult.Succeeded)
            {
                return BadRequest("Something went wrong with registration!");
            }

            var roles = new List<string>();

            if (registerManagerRequestDto.RoleId != MMConstants.ADMIN_ID)
            {
                roles.Add(MMConstants.GUEST);
                await userManager.AddToRolesAsync(identityUser, roles);
            }
            else
            {
                roles.Add(MMConstants.ADMIN);
                await userManager.AddToRolesAsync(identityUser, roles);
            }

            return Ok($"Manager: {identityUser.UserName} was registred as: {roles.FirstOrDefault()}!");
        }

        [HttpPost]
        [Route(nameof(Login))]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await this.userManager.FindByEmailAsync(loginRequestDto.UserName);

            if (user == null)
            {
                return BadRequest("UserName or Password are incorrect!");
            }

            var checkPasswordResult = await this.userManager.CheckPasswordAsync(user, loginRequestDto.Password);

            if (!checkPasswordResult)
            {
                return BadRequest("UserName or Password are incorrect!");
            }

            var roles = await this.userManager.GetRolesAsync(user);

            if (roles == null)
            {
                return BadRequest("UserName or Password are incorrect!");
            }

            var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());

            var response = new LoginResponseDto
            {
                JwtToken = jwtToken
            };

            return Ok(response);

        }

        [HttpGet]
        [Route(nameof(User))]
        public async Task<ActionResult<UserDetailsServiceModel>> UserDetails()
        {
            var email = this.tokenRepository.GetCurrentUserEmail();

            if (email == null)
            {
                return BadRequest();
            }

            var user = await this.tokenRepository.UserDetails(email);

            if (user == null)
            {
                return BadRequest();
            }

            return user;
        }
    }
}
