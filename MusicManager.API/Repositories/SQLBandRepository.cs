using Microsoft.EntityFrameworkCore;
using MusicManager.API.Data;
using MusicManager.API.Models.Domain;

namespace MusicManager.API.Repositories
{
    public class SQLBandRepository : IBandRepository
    {
        private readonly MusicManagerDbContext dbContext;

        public SQLBandRepository(MusicManagerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Band> CreateAsync(Band band)
        {
            await dbContext.Bands.AddAsync(band);
            await dbContext.SaveChangesAsync();
            return band;
        }

        public async Task<Band?> DeleteAsync(int id)
        {
            var band = await dbContext.Bands.FirstOrDefaultAsync(x => x.Id == id);

            if (band == null)
            {
                return null;
            }

            dbContext.Bands.Remove(band); 
            await dbContext.SaveChangesAsync();

            return band;
        }

        public async Task<List<Band>> GetAllAsync()
        {
            return await dbContext.Bands.ToListAsync();
        }

        public async Task<Band?> GetByIdAsync(int id)
        {
            return await dbContext.Bands.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Band?> UpdateAsync(int id, Band updatedBand)
        {
            var band = await dbContext.Bands.FirstOrDefaultAsync(x => x.Id == id);

            if (band == null)
            {
                return null;
            }

            band.Name = updatedBand.Name;
            band.Style = updatedBand.Style;

            await dbContext.SaveChangesAsync();
            return band;
        }
    }
}
