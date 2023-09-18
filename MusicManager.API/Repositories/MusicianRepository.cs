using Microsoft.EntityFrameworkCore;
using MusicManager.API.Common.Repositories;
using MusicManager.API.Data;
using MusicManager.API.Models.Domain;

namespace MusicManager.API.Repositories
{
    public class MusicianRepository : DeletableEntityRepository<Musician, Guid>
    {
        public MusicianRepository(MusicManagerDbContext db) : base(db)
        {
        }

        public async override Task<IList<Musician>> AllAsync()
        {
            return await base.DbSet.Where(x => !x.IsDeleted).Include(x => x.Band).ToListAsync();
        }

        public override async Task<Musician> ByIdAsync(Guid id)
        {
            return await base.DbSet.Include(x => x.Band).FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }
    }
}
