using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicManager.API.Data;
using MusicManager.API.Models.Domain;
using MusicManager.API.Models.Enums;

namespace MusicManager.API.Controllers
{
    // https://localhost:postnumber/api/musicians
    [Route("api/[controller]")]
    [ApiController]
    public class MusiciansController : ControllerBase
    {
        private readonly MusicManagerDbContext dbContext;

        public MusiciansController(MusicManagerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: https://localhost:postnumber/api/musicians
        [HttpGet]
        public IActionResult GetAll()
        {
            var albums = dbContext.Musicians.ToList();
            return Ok(albums);
        }
    }
}
