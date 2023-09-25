using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicManager.API.Common.CustomActionFilters;
using MusicManager.API.Data.Models;
using MusicManager.API.Features.Bands.Models;
using MusicManager.API.Features.Users;
using MusicManager.API.Utils;

namespace MusicManager.API.Features.Bands
{
    [Route("api/[controller]")]
    [ApiController]
    public class BandsController : ControllerBase
    {
        private readonly BandService bandService;
        private readonly IMapper mapper;
        private readonly IUserService userService;

        public BandsController(BandService bandService, IMapper mapper, IUserService userService)
        {
            this.bandService = bandService;
            this.mapper = mapper;
            this.userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var models = await bandService.AllAsync();

            var modelDtos = mapper.Map<List<BandDetails>>(models);

            return Ok(modelDtos);
        }

        [HttpGet]
        [Route(MMConstants.Id)]
        public async Task<IActionResult> GetById(int id)
        {
            var model = await bandService.ByIdAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            var modelDto = mapper.Map<BandDetails>(model);

            return Ok(modelDto);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAsync(CreateBandRequestModel createModelRequestDto)
        {
            var model = mapper.Map<Band>(createModelRequestDto);
            var user = this.userService.GetCurrentUserDetails().Result;
            if (user == null)
            {
                return BadRequest("Not authorized!");
            }

            model.CreatedBy = user.Email;
            model.UserId = user.Id;
            model = await bandService.CreateAsync(model);

            var modelDto = mapper.Map<BandModel>(model);

            return Ok(modelDto);
        }

        [HttpPut]
        [ValidateModel]
        [Route(MMConstants.Id)]
        [Authorize]
        public async Task<IActionResult> UpdateAsync(int id, UpdateBandRequestModel updateModelRequestDto)
        {
            var model = await bandService.ByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            var user = this.userService.GetCurrentUserDetails().Result;
            if (user.Email != model.CreatedBy)
            {
                return BadRequest("Not authorized!");
            }

            model.ModifiedBy = user.Email;
            model.Name = updateModelRequestDto.Name;
            model.Style = updateModelRequestDto.Style;

            bandService.Update(model);

            return Ok(mapper.Map<BandModel>(model));
        }

        [HttpDelete]
        [Route(MMConstants.Id)]
        [Authorize]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var model = await bandService.ByIdAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            var user = this.userService.GetCurrentUserDetails().Result;
            if (user.Email != model.CreatedBy)
            {
                return BadRequest("Not authorized!");
            }

            model.DeletedBy = user.Email;

            bandService.Delete(model);

            return Ok(mapper.Map<BandModel>(model));
        }
    }
}
