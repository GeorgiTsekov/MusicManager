using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicManager.API.Common.Repositories;
using MusicManager.API.Models.Domain;
using MusicManager.API.Models.DTO;

namespace MusicManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly UserManager<IdentityUser> manager;
        private readonly ITokenRepository tokenRepository;

        public ManagerController(UserManager<IdentityUser> manager, ITokenRepository tokenRepository)
        {
            this.manager = manager;
            this.tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterManagerRequestDto registerManagerRequestDto)
        {
            var identityUser = new Manager
            {
                UserName = registerManagerRequestDto.UserName,
                Email = registerManagerRequestDto.UserName
            };

            var identityResult = await manager.CreateAsync(identityUser, registerManagerRequestDto.Password);

            if (identityResult.Succeeded)
            {
                if (registerManagerRequestDto.Roles != null && registerManagerRequestDto.Roles.Any())
                {
                    identityResult = await manager.AddToRolesAsync(identityUser, registerManagerRequestDto.Roles);

                    if (identityResult.Succeeded)
                    {
                        return Ok("Manager was registred!");
                    }
                }
            }

            return BadRequest("Something went wrong with registration!");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await manager.FindByEmailAsync(loginRequestDto.UserName);

            if (user != null)
            {
                var checkPasswordResult = await manager.CheckPasswordAsync(user, loginRequestDto.Password);

                if (checkPasswordResult)
                {
                    var roles = await manager.GetRolesAsync(user);

                    if (roles != null)
                    {
                        var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());

                        var response = new LoginResponseDto
                        {
                            JwtToken = jwtToken
                        };

                        return Ok(response);
                    }
                }
            }

            return BadRequest("UserName or Password are incorrect!");
        }
    }
}
