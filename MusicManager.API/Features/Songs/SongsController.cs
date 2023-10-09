using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicManager.API.Features.Albums;
using MusicManager.API.Features.Songs.Models;
using MusicManager.Data.Models;
using MusicManager.Infrastructure.CustomActionFilters;
using MusicManager.Infrastructure.Utils;

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
        [Route("All/Mine")]
        public async Task<IActionResult> GetAllMineAsync()
        {
            var models = await songService.AllMineAsync();

            var modelDtos = mapper.Map<List<SongDetails>>(models);

            return Ok(modelDtos);
        }

        [HttpGet]
        [Route("All")]
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
        [Route("Create")]
        public async Task<IActionResult> CreateAsync(CreateSongRequestModel createModelRequestDto)
        {
            var user = this.albumService.UserService.GetCurrentUserDetails().Result;
            if (user == null)
            {
                return BadRequest("Not authorized!");
            }

            var album = await albumService.ByIdAsync(createModelRequestDto.AlbumId);
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
            model.Style = createModelRequestDto.Style;
            model.Sound = "new sound";
            await songService.CreateAsync(model);
            album.Songs.Add(model);

            var modelDto = mapper.Map<SongModel>(model);

            return Ok(modelDto);
        }

        [HttpPut]
        [ValidateModel]
        [Authorize]
        [Route($"Update/{MMConstants.Id}")]
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
        [Route($"Delete/{MMConstants.Id}")]
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
