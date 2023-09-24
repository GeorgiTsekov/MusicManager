using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MusicManager.API.Common.Repositories;
using MusicManager.API.Data;
using MusicManager.API.Data.Models;

namespace MusicManager.API.Features.Musicians
{
    public class MusicianRepository : DeletableEntityRepository<Musician, Guid>
    {
        public MusicianRepository(MusicManagerDbContext db) : base(db)
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

        public override async Task<Musician> CreateAsync(Musician entity)
        {
            var band = await Context.Bands.FirstOrDefaultAsync(x => x.Id == entity.BandId);

            if (band == null)
            {
                return null;
            }

            band.Musicians.Add(entity);
            await DbSet.AddAsync(entity);
            Context.SaveChanges();
            return entity;
        }
    }
}
