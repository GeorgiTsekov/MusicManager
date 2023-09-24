using Microsoft.AspNetCore.Mvc;
using MusicManager.API.Data;

namespace MusicManager.API.Features.Albums
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
    }
}
