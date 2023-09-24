using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicManager.API.Common.CustomActionFilters;
using MusicManager.API.Data.Models;
using MusicManager.API.Features.Bands.Models;

namespace MusicManager.API.Features.Bands
{
    [Route("api/[controller]")]
    [ApiController]
    public class BandsController : ControllerBase
    {
        private readonly BandRepository bandRepository;
        private readonly IMapper mapper;


        public BandsController(BandRepository bandRepository, IMapper mapper)
        {
            this.bandRepository = bandRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var bands = await bandRepository.AllAsync();

            var bandDtos = mapper.Map<List<BandSingleDto>>(bands);

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

            var bandDto = mapper.Map<BandSingleDto>(band);

            return Ok(bandDto);
        }

        [HttpPost]
        [ValidateModel]
        [Authorize]
        public async Task<IActionResult> CreateAsync([FromBody] CreateBandRequestDto createBandRequestDto)
        {
            var band = mapper.Map<Band>(createBandRequestDto);

            band = await bandRepository.CreateAsync(band);

            var bandDto = mapper.Map<BandDto>(band);

            return CreatedAtAction(nameof(GetById), new { id = bandDto.Id }, bandDto);
        }

        [HttpPut]
        [ValidateModel]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin,Creator")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] UpdateBandRequestDto updateBandRequestDto)
        {
            var band = await bandRepository.ByIdAsync(id);

            if (band == null)
            {
                return NotFound();
            }

            band.Name = updateBandRequestDto.Name;
            band.Style = updateBandRequestDto.Style;

            bandRepository.Update(band);

            return Ok(mapper.Map<BandDto>(band));
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var band = await bandRepository.ByIdAsync(id);

            if (band == null)
            {
                return NotFound();
            }

            bandRepository.Delete(band);

            return Ok(mapper.Map<BandDto>(band));
        }
    }
}
