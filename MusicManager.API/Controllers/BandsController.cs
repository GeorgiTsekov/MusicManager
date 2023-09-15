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
        private readonly IBandRepository bandRepository;
        private readonly IMapper mapper;

        public BandsController(IBandRepository bandRepository, IMapper mapper)
        {
            this.bandRepository = bandRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bands = await bandRepository.GetAllAsync();

            var bandDtos = mapper.Map<List<BandDto>>(bands);

            return Ok(bandDtos);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var band = await bandRepository.GetByIdAsync(id);

            if (band == null)
            {
                return NotFound();
            }

            return Ok(band);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBandRequestDto createBandRequestDto)
        {
            var band = mapper.Map<Band>(createBandRequestDto);

            band = await bandRepository.CreateAsync(band);

            var bandDto = mapper.Map<BandDto>(band);

            return CreatedAtAction(nameof(GetById), new { id = bandDto.Id }, bandDto);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateBandRequestDto updateBandRequestDto)
        {
            var band = mapper.Map<Band>(updateBandRequestDto);

            band = await bandRepository.UpdateAsync(id, band);

            if (band == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<BandDto>(band));
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var band = await bandRepository.DeleteAsync(id);

            if (band == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<BandDto>(band));
        }
    }
}
