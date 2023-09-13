using Microsoft.AspNetCore.Mvc;
using MusicManager.API.Data;

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
    }
}
