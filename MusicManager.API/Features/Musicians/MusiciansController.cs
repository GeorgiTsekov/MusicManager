using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicManager.API.Features.Bands;
using MusicManager.API.Features.Musicians.Models;
using MusicManager.Data.Models;
using MusicManager.Infrastructure.CustomActionFilters;
using MusicManager.Infrastructure.Utils;

namespace MusicManager.API.Features.Musicians
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusiciansController : ControllerBase
    {
        private readonly MusicianService musicianService;
        private readonly IMapper mapper;
        private readonly BandService bandService;

        public MusiciansController(MusicianService musicianService, IMapper mapper, BandService bandService)
        {
            this.musicianService = musicianService;
            this.mapper = mapper;
            this.bandService = bandService;
        }

        [HttpGet]
        [Authorize]
        [Route("All/Mine")]
        public async Task<IActionResult> GetAllMineAsync()
        {
            var models = await musicianService.AllMineAsync();

            var modelDtos = mapper.Map<List<MusicianModel>>(models);

            return Ok(modelDtos);
        }

        [HttpGet]
        [Route("All")]
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
        [Route("Create")]
        public async Task<IActionResult> CreateAsync(CreateMusicianRequestModel createModelRequestDto)
        {
            var user = this.musicianService.UserService.GetCurrentUserDetails().Result;
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

            if (band.Musicians.Count >= 6)
            {
                return BadRequest("Too many musicians!");
            }

            await musicianService.CreateAsync(model);
            band.Musicians.Add(model);

            var modelDto = mapper.Map<MusicianModel>(model);

            return Ok(modelDto);
        }

        [HttpPut]
        [ValidateModel]
        [Authorize]
        [Route($"Update/{MMConstants.Id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, UpdateMusicianRequestModel updateModelRequestDto)
        {
            var model = await musicianService.ByIdAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            var user = this.musicianService.UserService.GetCurrentUserDetails().Result;
            if (user == null)
            {
                return BadRequest("Not authorized!");
            }

            model.CreatedBy = user.UserName;
            model.Name = updateModelRequestDto.Name;

            musicianService.Update(model);

            return Ok(mapper.Map<MusicianModel>(model));
        }

        [HttpDelete]
        [Authorize]
        [Route($"Delete/{MMConstants.Id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var model = await musicianService.ByIdAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            var user = this.musicianService.UserService.GetCurrentUserDetails().Result;
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
