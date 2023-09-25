using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicManager.API.Common.CustomActionFilters;
using MusicManager.API.Common.Models;
using MusicManager.API.Data.Models;
using MusicManager.API.Features.Bands;
using MusicManager.API.Features.Musicians.Models;
using MusicManager.API.Features.Users;

namespace MusicManager.API.Features.Musicians
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusiciansController : ControllerBase
    {
        private readonly MusicianService musicianRepository;
        private readonly IMapper mapper;
        private readonly IUserService userService;
        private readonly BandService bandService;

        public MusiciansController(MusicianService musicianRepository, IMapper mapper, IUserService userService, BandService bandService)
        {
            this.musicianRepository = musicianRepository;
            this.mapper = mapper;
            this.userService = userService;
            this.bandService = bandService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var musicians = await musicianRepository.AllAsync();

            var musicianDtos = mapper.Map<List<MusicianModel>>(musicians);

            return Ok(musicianDtos);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var musician = await musicianRepository.ByIdAsync(id);

            if (musician == null)
            {
                return NotFound();
            }

            var musicianDto = mapper.Map<MusicianModel>(musician);

            return Ok(musicianDto);
        }

        [HttpPost]
        [ValidateModel]
        [Authorize]
        public async Task<IActionResult> CreateAsync([FromBody] CreateMusicianRequestModel createBandRequestDto)
        {
            var user = this.userService.GetCurrentUserDetails().Result;
            if (user == null)
            {
                return BadRequest("Not authorized!");
            }

            var band = bandService.ByIdAsync(createBandRequestDto.BandId).Result;
            if (band == null)
            {
                return BadRequest($"There is not band with id: {createBandRequestDto.BandId}");
            }

            if (band.CreatedBy != user.Email)
            {
                return BadRequest("Not authorized!");
            }

            var musician = mapper.Map<Musician>(createBandRequestDto);
            musician.CreatedBy = user.Email;
            musician.BandId = band.Id;
            band.Musicians.Add(musician);
            await musicianRepository.CreateAsync(musician);

            var musicianDto = mapper.Map<MusicianModel>(musician);

            return Ok(musicianDto);
        }

        [HttpPut]
        [ValidateModel]
        [Authorize]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateMusicianRequestModel updateBandRequestDto)
        {
            var musician = await musicianRepository.ByIdAsync(id);

            if (musician == null)
            {
                return NotFound();
            }

            var user = this.userService.GetCurrentUserDetails().Result;
            if (user == null)
            {
                return BadRequest("Not authorized!");
            }

            musician.CreatedBy = user.Email;

            musician.Name = updateBandRequestDto.Name;
            musician.Clothing = updateBandRequestDto.Clothing;

            musicianRepository.Update(musician);

            return Ok(mapper.Map<MusicianModel>(musician));
        }

        [HttpDelete]
        [Authorize]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var musician = await musicianRepository.ByIdAsync(id);

            if (musician == null)
            {
                return NotFound();
            }

            var user = this.userService.GetCurrentUserDetails().Result;
            if (user == null)
            {
                return BadRequest("Not authorized!");
            }

            musician.CreatedBy = user.Email;

            musicianRepository.Delete(musician);

            return Ok(mapper.Map<MusicianModel>(musician));
        }
    }
}
