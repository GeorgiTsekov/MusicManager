using Microsoft.AspNetCore.Mvc;
using MusicManager.API.Data;

namespace MusicManager.API.Features.Songs
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly MusicManagerDbContext dbContext;

        public SongsController(MusicManagerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
