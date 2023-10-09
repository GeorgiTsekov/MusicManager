using Microsoft.EntityFrameworkCore;
using MusicManager.API.BaseRepositories;
using MusicManager.API.Data;
using MusicManager.API.Features.Users;
using MusicManager.Data.Models;

namespace MusicManager.API.Features.Bands
{
    public class BandService : DeletableEntityRepository<Band, int>
    {
        public BandService(MusicManagerDbContext db, IUserService userService) : base(db, userService)
        {
        }

        public async override Task<IList<Band>> AllAsync()
        {
            var entities = await DbSet.Include(x => x.Musicians).Include(x => x.Albums).ThenInclude(a => a.Songs).ToListAsync();
            entities = entities.Where(x => !x.IsDeleted).ToList();
            return entities;
        }

        public override async Task<Band> ByIdAsync(int id)
        {
            return await DbSet.Include(b => b.Musicians).Include(b => b.Albums).ThenInclude(a => a.Songs).FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }
    }
}
