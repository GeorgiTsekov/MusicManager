using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicManager.API.Data;
using MusicManager.API.Models.Domain;

namespace MusicManager.API.Controllers
{
    // https://localhost:7227/api/albums
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly MusicManagerDbContext dbContext;

        public AlbumsController(MusicManagerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var albums = dbContext.Albums.ToList();
            return Ok(albums);
        }
    }
}
