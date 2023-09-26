using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicManager.API.Common.CustomActionFilters;
using MusicManager.API.Data.Models;
using MusicManager.API.Features.Albums;
using MusicManager.API.Features.Songs.Models;
using MusicManager.API.Utils;

namespace MusicManager.API.Features.Songs
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly SongService songService;
        private readonly IMapper mapper;
        private readonly AlbumService albumService;

        public SongsController(SongService songService, IMapper mapper, AlbumService albumService)
        {
            this.songService = songService;
            this.mapper = mapper;
            this.albumService = albumService;
        }

        [HttpGet]
        [Authorize]
        [Route("Mine")]
        public async Task<IActionResult> GetAllMineAsync()
        {
            var models = await songService.AllMineAsync();

            var modelDtos = mapper.Map<List<SongDetails>>(models);

            return Ok(modelDtos);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var models = await songService.AllAsync();

            var modelsDtos = mapper.Map<List<SongDetails>>(models);

            return Ok(modelsDtos);
        }

        [HttpGet]
        [Route(MMConstants.Id)]
        public async Task<IActionResult> GetById(int id)
        {
            var model = await songService.ByIdAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            var modelDto = mapper.Map<SongDetails>(model);

            return Ok(modelDto);
        }

        [HttpPost]
        [ValidateModel]
        [Authorize]
        public async Task<IActionResult> CreateAsync(CreateSongRequestModel createModelRequestDto)
        {
            var user = this.albumService.UserService.GetCurrentUserDetails().Result;
            if (user == null)
            {
                return BadRequest("Not authorized!");
            }

            var album = albumService.ByIdAsync(createModelRequestDto.AlbumId).Result;
            if (album == null)
            {
                return BadRequest($"There is not band with id: {createModelRequestDto.AlbumId}");
            }

            if (album.CreatedBy != user.Email)
            {
                return BadRequest("Not authorized!");
            }

            var model = mapper.Map<Song>(createModelRequestDto);
            model.CreatedBy = user.Email;
            model.AlbumId = album.Id;
            album.Songs.Add(model);
            await songService.CreateAsync(model);

            var modelDto = mapper.Map<SongModel>(model);

            return Ok(modelDto);
        }

        [HttpPut]
        [ValidateModel]
        [Authorize]
        [Route(MMConstants.Id)]
        public async Task<IActionResult> UpdateAsync(int id, UpdateSongRequestModel updateModelRequestDto)
        {
            var model = await songService.ByIdAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            var user = this.albumService.UserService.GetCurrentUserDetails().Result;
            if (user == null)
            {
                return BadRequest("Not authorized!");
            }

            model.CreatedBy = user.Email;

            model.Name = updateModelRequestDto.Name;

            songService.Update(model);

            return Ok(mapper.Map<SongModel>(model));
        }

        [HttpDelete]
        [Authorize]
        [Route(MMConstants.Id)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var model = await songService.ByIdAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            var user = this.albumService.UserService.GetCurrentUserDetails().Result;
            if (user == null)
            {
                return BadRequest("Not authorized!");
            }

            model.CreatedBy = user.Email;

            songService.Delete(model);

            return Ok(mapper.Map<SongModel>(model));
        }
    }
}
