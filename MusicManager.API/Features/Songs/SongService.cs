using Microsoft.EntityFrameworkCore;
using MusicManager.API.BaseRepositories;
using MusicManager.API.Data;
using MusicManager.API.Data.Models;
using MusicManager.API.Features.Users;

namespace MusicManager.API.Features.Songs
{
    public class SongService : DeletableEntityRepository<Song, int>
    {
        public SongService(MusicManagerDbContext db, IUserService userService) : base(db, userService)
        {
        }

        public async override Task<IList<Song>> AllAsync()
        {
            return await DbSet.Where(x => !x.IsDeleted).Include(x => x.Album).ToListAsync();
        }

        public override async Task<Song> ByIdAsync(int id)
        {
            return await DbSet.Include(x => x.Album).FirstOrDefaultAsync(x => x.Id.Equals(id) && !x.IsDeleted);
        }
    }
}
