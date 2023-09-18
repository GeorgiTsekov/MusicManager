using Microsoft.EntityFrameworkCore;
using MusicManager.API.Common.Repositories;
using MusicManager.API.Data;
using MusicManager.API.Models.Domain;

namespace MusicManager.API.Repositories
{
    public class BandRepository : DeletableEntityRepository<Band, int>
    {
        public BandRepository(MusicManagerDbContext db) : base(db)
        {
        }
    }
}
