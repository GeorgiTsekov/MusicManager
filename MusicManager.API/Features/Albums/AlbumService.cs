using Microsoft.EntityFrameworkCore;
using MusicManager.API.Common.Repositories;
using MusicManager.API.Data;
using MusicManager.API.Data.Models;
using MusicManager.API.Features.Users;

namespace MusicManager.API.Features.Albums
{
    public class AlbumService : DeletableEntityRepository<Album, int>
    {
        public AlbumService(MusicManagerDbContext db, IUserService userService) : base(db, userService)
        {
        }

        public async override Task<IList<Album>> AllAsync()
        {
            var entities = await DbSet.Include(x => x.Band).Include(x => x.Songs).ToListAsync();
            entities = entities.Where(x => !x.IsDeleted).ToList();
            return entities;
        }

        public override async Task<Album> ByIdAsync(int id)
        {
            return await DbSet.Include(x => x.Band).Include(x => x.Songs).FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }
    }
}
