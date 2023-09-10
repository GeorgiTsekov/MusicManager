using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicManager.API.Data;
using MusicManager.API.Models.Domain;

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
        public IActionResult GetAll()
        {
            var albums = dbContext.Bands.ToList();
            return Ok(albums);
        }
    }
}
