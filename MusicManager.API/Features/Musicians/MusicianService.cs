using Microsoft.EntityFrameworkCore;
using MusicManager.API.BaseRepositories;
using MusicManager.API.Data;
using MusicManager.API.Features.Users;
using MusicManager.Data.Enums;
using MusicManager.Data.Models;

namespace MusicManager.API.Features.Musicians
{
    public class MusicianService : DeletableEntityRepository<Musician, Guid>
    {
        public MusicianService(MusicManagerDbContext db, IUserService userService) : base(db, userService)
        {
        }

        public async override Task<Musician> CreateAsync(Musician entity)
        {
            var band = await DbContext.Bands.FirstOrDefaultAsync(b => b.Id == entity.BandId);
            var musiciansCount = band.Musicians.Count;
            var dailyRent = (int)entity.DailyRent;
            entity.MusicalInstrumentType = Enum.Parse<MusicalInstrumentType>((musiciansCount + 1).ToString());

            var random = new Random();
            entity.Ambition = random.Next(0, (dailyRent / 2) + 1);
            entity.Talant = random.Next(0, (dailyRent / 2) + 1 - entity.Ambition);
            entity.Compatibility = dailyRent - (entity.Talant + entity.Ambition);

            return await base.CreateAsync(entity);
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
