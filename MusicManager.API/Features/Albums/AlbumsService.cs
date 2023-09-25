using Microsoft.EntityFrameworkCore;
using MusicManager.API.Common.Repositories;
using MusicManager.API.Data;
using MusicManager.API.Data.Models;

namespace MusicManager.API.Features.Albums
{
    public class AlbumsService : DeletableEntityRepository<Album, int>
    {
        public AlbumsService(MusicManagerDbContext db) : base(db)
        {
        }

        public async override Task<IList<Album>> AllAsync()
        {
            return await DbSet.Where(x => !x.IsDeleted).Include(x => x.Band).ToListAsync();
        }

        public override async Task<Album> ByIdAsync(int id)
        {
            return await DbSet.Include(x => x.Band).FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }
    }
}
