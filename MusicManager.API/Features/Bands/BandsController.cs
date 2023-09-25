using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicManager.API.Common.CustomActionFilters;
using MusicManager.API.Data.Models;
using MusicManager.API.Features.Bands.Models;
using MusicManager.API.Features.Users;

namespace MusicManager.API.Features.Bands
{
    [Route("api/[controller]")]
    [ApiController]
    public class BandsController : ControllerBase
    {
        private readonly BandService bandRepository;
        private readonly IMapper mapper;
        private readonly IUserService userService;

        public BandsController(BandService bandRepository, IMapper mapper, IUserService userService)
        {
            this.bandRepository = bandRepository;
            this.mapper = mapper;
            this.userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var bands = await bandRepository.AllAsync();

            var bandDtos = mapper.Map<List<BandDetails>>(bands);

            return Ok(bandDtos);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var band = await bandRepository.ByIdAsync(id);

            if (band == null)
            {
                return NotFound();
            }

            var bandDto = mapper.Map<BandDetails>(band);

            return Ok(bandDto);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAsync([FromBody] CreateBandRequestModel createBandRequestDto)
        {
            var band = mapper.Map<Band>(createBandRequestDto);
            var user = this.userService.GetCurrentUserDetails().Result;
            if (user == null)
            {
                return BadRequest("Not authorized!");
            }

            band.CreatedBy = user.Email;
            band.UserId = user.Id;
            band = await bandRepository.CreateAsync(band);

            var bandDto = mapper.Map<BandModel>(band);

            return Ok(bandDto);
        }

        [HttpPut]
        [ValidateModel]
        [Route("{id:int}")]
        [Authorize]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] UpdateBandRequestModel updateBandRequestDto)
        {
            var band = await bandRepository.ByIdAsync(id);
            if (band == null)
            {
                return NotFound();
            }

            var user = this.userService.GetCurrentUserDetails().Result;
            if (user.Email != band.CreatedBy)
            {
                return BadRequest("Not authorized!");
            }

            band.ModifiedBy = user.Email;
            band.Name = updateBandRequestDto.Name;
            band.Style = updateBandRequestDto.Style;

            bandRepository.Update(band);

            return Ok(mapper.Map<BandModel>(band));
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var band = await bandRepository.ByIdAsync(id);

            if (band == null)
            {
                return NotFound();
            }

            var user = this.userService.GetCurrentUserDetails().Result;
            if (user.Email != band.CreatedBy)
            {
                return BadRequest("Not authorized!");
            }

            band.DeletedBy = user.Email;

            bandRepository.Delete(band);

            return Ok(mapper.Map<BandModel>(band));
        }
    }
}
