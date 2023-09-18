using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicManager.API.Common.Repositories;
using MusicManager.API.Data;
using MusicManager.API.Models.Domain;
using MusicManager.API.Models.DTO;
using MusicManager.API.Repositories;

namespace MusicManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusiciansController : ControllerBase
    {
        private readonly MusicianRepository musicianRepository;
        private readonly IMapper mapper;

        public MusiciansController(MusicianRepository musicianRepository, IMapper mapper)
        {
            this.musicianRepository = musicianRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var musicians = await musicianRepository.AllAsync();

            var musicianDtos = mapper.Map<List<MusicianDto>>(musicians);

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

            var musicianDto = mapper.Map<MusicianDto>(musician);

            return Ok(musicianDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateMusicianRequestDto createBandRequestDto)
        {
            var musician = mapper.Map<Musician>(createBandRequestDto);
            await musicianRepository.CreateAsync(musician);

            var musicianDto = mapper.Map<MusicianDto>(musician);

            return Ok(musicianDto);
        }

        // TODO: 
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateMusicianRequestDto updateBandRequestDto)
        {
            var musician = await musicianRepository.ByIdAsync(id);

            if (musician == null)
            {
                return NotFound();
            }

            //musician.Name = updateBandRequestDto.Name;
            //musician.Style = updateBandRequestDto.Style;

            //musicianRepository.Update(musician);

            return Ok(mapper.Map<MusicianDto>(musician));
        }

        // TODO:
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var musician = await musicianRepository.ByIdAsync(id);

            if (musician == null)
            {
                return NotFound();
            }

            musicianRepository.Delete(musician);

            return Ok(mapper.Map<MusicianDto>(musician));
        }
    }
}
