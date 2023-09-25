using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicManager.API.Common.CustomActionFilters;
using MusicManager.API.Data.Models;
using MusicManager.API.Features.Bands;
using MusicManager.API.Features.Musicians.Models;
using MusicManager.API.Features.Users;
using MusicManager.API.Utils;

namespace MusicManager.API.Features.Musicians
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusiciansController : ControllerBase
    {
        private readonly MusicianService musicianService;
        private readonly IMapper mapper;
        private readonly IUserService userService;
        private readonly BandService bandService;

        public MusiciansController(MusicianService musicianService, IMapper mapper, IUserService userService, BandService bandService)
        {
            this.musicianService = musicianService;
            this.mapper = mapper;
            this.userService = userService;
            this.bandService = bandService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var models = await musicianService.AllAsync();

            var modelsDtos = mapper.Map<List<MusicianModel>>(models);

            return Ok(modelsDtos);
        }

        [HttpGet]
        [Route(MMConstants.Id)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var model = await musicianService.ByIdAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            var modelDto = mapper.Map<MusicianModel>(model);

            return Ok(modelDto);
        }

        [HttpPost]
        [ValidateModel]
        [Authorize]
        public async Task<IActionResult> CreateAsync(CreateMusicianRequestModel createModelRequestDto)
        {
            var user = this.userService.GetCurrentUserDetails().Result;
            if (user == null)
            {
                return BadRequest("Not authorized!");
            }

            var band = bandService.ByIdAsync(createModelRequestDto.BandId).Result;
            if (band == null)
            {
                return BadRequest($"There is not band with id: {createModelRequestDto.BandId}");
            }

            if (band.CreatedBy != user.Email)
            {
                return BadRequest("Not authorized!");
            }

            var model = mapper.Map<Musician>(createModelRequestDto);
            model.CreatedBy = user.Email;
            model.BandId = band.Id;
            band.Musicians.Add(model);
            await musicianService.CreateAsync(model);

            var modelDto = mapper.Map<MusicianModel>(model);

            return Ok(modelDto);
        }

        [HttpPut]
        [ValidateModel]
        [Authorize]
        [Route(MMConstants.Id)]
        public async Task<IActionResult> UpdateAsync(Guid id, UpdateMusicianRequestModel updateModelRequestDto)
        {
            var model = await musicianService.ByIdAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            var user = this.userService.GetCurrentUserDetails().Result;
            if (user == null)
            {
                return BadRequest("Not authorized!");
            }

            model.CreatedBy = user.Email;

            model.Name = updateModelRequestDto.Name;
            model.Clothing = updateModelRequestDto.Clothing;

            musicianService.Update(model);

            return Ok(mapper.Map<MusicianModel>(model));
        }

        [HttpDelete]
        [Authorize]
        [Route(MMConstants.Id)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var model = await musicianService.ByIdAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            var user = this.userService.GetCurrentUserDetails().Result;
            if (user == null)
            {
                return BadRequest("Not authorized!");
            }

            model.CreatedBy = user.Email;

            musicianService.Delete(model);

            return Ok(mapper.Map<MusicianModel>(model));
        }
    }
}
