using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MusicManager.API.Common.Repositories;
using MusicManager.API.Data;
using MusicManager.API.Data.Models;

namespace MusicManager.API.Features.Musicians
{
    public class MusicianService : DeletableEntityRepository<Musician, Guid>
    {
        public MusicianService(MusicManagerDbContext db) : base(db)
        {
        }

        public async override Task<IList<Musician>> AllAsync()
        {
            return await DbSet.Where(x => !x.IsDeleted).Include(x => x.Band).ToListAsync();
        }

        public override async Task<Musician> ByIdAsync(Guid id)
        {
            return await DbSet.Include(x => x.Band).FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }
    }
}
