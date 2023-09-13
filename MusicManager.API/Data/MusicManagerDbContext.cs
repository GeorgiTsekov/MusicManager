using Microsoft.EntityFrameworkCore;
using MusicManager.API.Models.Domain;

namespace MusicManager.API.Data
{
    public class MusicManagerDbContext : DbContext
    {
        public MusicManagerDbContext(DbContextOptions<MusicManagerDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Musician> Musicians { get; set; }
        public DbSet<Band> Bands { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Album> Albums { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
