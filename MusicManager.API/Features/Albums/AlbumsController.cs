using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicManager.API.Features.Albums.Models;
using MusicManager.API.Features.Bands;
using MusicManager.Data.Models;
using MusicManager.Infrastructure.CustomActionFilters;
using MusicManager.Infrastructure.Utils;

namespace MusicManager.API.Features.Albums
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly AlbumService albumsService;
        private readonly IMapper mapper;
        private readonly BandService bandService;

        public AlbumsController(AlbumService albumsService, IMapper mapper, BandService bandService)
        {
            this.albumsService = albumsService;
            this.mapper = mapper;
            this.bandService = bandService;
        }

        [HttpGet]
        [Authorize]
        [Route("All/Mine")]
        public async Task<IActionResult> GetAllMineAsync()
        {
            var models = await albumsService.AllMineAsync();

            var modelDtos = mapper.Map<List<AlbumDetails>>(models);

            return Ok(modelDtos);
        }

        [HttpGet]
        [Route($"All")]
        public async Task<IActionResult> GetAllAsync()
        {
            var models = await albumsService.AllAsync();

            var modelsDtos = mapper.Map<List<AlbumDetails>>(models);

            return Ok(modelsDtos);
        }

        [HttpGet]
        [Route(MMConstants.Id)]
        public async Task<IActionResult> GetById(int id)
        {
            var model = await albumsService.ByIdAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            var modelDto = mapper.Map<AlbumDetails>(model);

            return Ok(modelDto);
        }

        [HttpPost]
        [ValidateModel]
        [Authorize]
        [Route("Create")]
        public async Task<IActionResult> CreateAsync(CreateAlbumRequestModel createModelRequest)
        {
            var user = this.albumsService.UserService.GetCurrentUserDetails().Result;
            if (user == null)
            {
                return BadRequest("Not authorized!");
            }

            var band = bandService.ByIdAsync(createModelRequest.BandId).Result;
            if (band == null)
            {
                return BadRequest($"There is not band with id: {createModelRequest.BandId}");
            }

            if (band.CreatedBy != user.Email)
            {
                return BadRequest("Not authorized!");
            }

            var model = mapper.Map<Album>(createModelRequest);
            model.CreatedBy = user.Email;
            model.BandId = band.Id;
            band.Albums.Add(model);
            await albumsService.CreateAsync(model);

            var modelDto = mapper.Map<AlbumModel>(model);

            return Ok(modelDto);
        }

        [HttpPut]
        [ValidateModel]
        [Authorize]
        [Route($"Update/{MMConstants.Id}")]
        public async Task<IActionResult> UpdateAsync(int id, UpdateAlbumRequestModel updateModelRequest)
        {
            var model = await albumsService.ByIdAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            var user = this.albumsService.UserService.GetCurrentUserDetails().Result;
            if (user == null)
            {
                return BadRequest("Not authorized!");
            }

            model.CreatedBy = user.Email;

            model.Name = updateModelRequest.Name;
            model.Descritpion = updateModelRequest.Descritpion;

            albumsService.Update(model);

            return Ok(mapper.Map<AlbumModel>(model));
        }

        [HttpDelete]
        [Authorize]
        [Route($"Delete/{MMConstants.Id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var model = await albumsService.ByIdAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            var user = this.albumsService.UserService.GetCurrentUserDetails().Result;
            if (user == null)
            {
                return BadRequest("Not authorized!");
            }

            model.CreatedBy = user.Email;

            albumsService.Delete(model);

            return Ok(mapper.Map<AlbumModel>(model));
        }
    }
}
