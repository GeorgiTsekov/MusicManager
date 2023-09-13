using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicManager.API.Data;
using MusicManager.API.Models.Domain;
using MusicManager.API.Models.DTO;

namespace MusicManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BandsController : ControllerBase
    {
        private readonly MusicManagerDbContext dbContext;

        public BandsController(MusicManagerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bands = await dbContext.Bands.ToListAsync();

            var bandDtos = new List<BandDto>();
            foreach (var band in bands)
            {
                bandDtos.Add(new BandDto()
                {
                    Id = band.Id,
                    Name = band.Name,
                    Style = band.Style,
                    Money = band.Money
                });
            }

            return Ok(bandDtos);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var band = await dbContext.Bands.FirstOrDefaultAsync(x => x.Id == id);

            if (band == null)
            {
                return NotFound();
            }

            return Ok(band);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBandRequestDto createBandRequestDto)
        {
            var band = new Band
            {
                Name = createBandRequestDto.Name,
                Style = createBandRequestDto.Style,
                Money = createBandRequestDto.Money
            };

            await dbContext.Bands.AddAsync(band);
            await dbContext.SaveChangesAsync();

            var bandDto = new BandDto
            {
                Id = band.Id,
                Name = band.Name,
                Style = band.Style,
                Money = band.Money
            };

            return CreatedAtAction(nameof(GetById), new { id = bandDto.Id }, bandDto);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateBandRequestDto updateBandRequestDto)
        {
            var band = await dbContext.Bands.FirstOrDefaultAsync(x => x.Id == id);

            if (band == null)
            {
                return NotFound();
            }

            band.Name = updateBandRequestDto.Name;
            band.Style = updateBandRequestDto.Style;

            await dbContext.SaveChangesAsync();

            var bandDto = new BandDto
            {
                Id = band.Id,
                Name = band.Name,
                Money = band.Money,
                Style = band.Style
            };

            return Ok(bandDto);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var band = await dbContext.Bands.FirstOrDefaultAsync(x => x.Id == id);

            if (band == null)
            {
                return NotFound();
            }

            dbContext.Bands.Remove(band);
            await dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
