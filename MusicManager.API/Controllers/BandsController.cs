using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicManager.API.Models.Domain;
using MusicManager.API.Models.DTO;
using MusicManager.API.Repositories;

namespace MusicManager.API.Controllers
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

            var bandDtos = mapper.Map<List<BandDto>>(bands);

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

            return Ok(band);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateBandRequestDto createBandRequestDto)
        {
            var band = mapper.Map<Band>(createBandRequestDto);

            band = await bandRepository.CreateAsync(band);

            var bandDto = mapper.Map<BandDto>(band);

            return CreatedAtAction(nameof(GetById), new { id = bandDto.Id }, bandDto);
        }

        [HttpPut]
        [Route("{id:int}")]
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
