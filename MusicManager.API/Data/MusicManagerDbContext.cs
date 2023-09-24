using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MusicManager.API.Data.Models;
using MusicManager.API.Utils;

namespace MusicManager.API.Data
{
    public class MusicManagerDbContext : IdentityDbContext
    {

        public MusicManagerDbContext(DbContextOptions<MusicManagerDbContext> options)
            : base(options)
        {
        }

        public DbSet<Musician> Musicians { get; set; }
        public DbSet<Band> Bands { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Album> Albums { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = MMConstants.GUEST_ID,
                    ConcurrencyStamp = MMConstants.GUEST_ID,
                    Name = MMConstants.GUEST,
                    NormalizedName = MMConstants.GUEST

                },
                new IdentityRole
                {
                    Id = MMConstants.ADMIN_ID,
                    ConcurrencyStamp = MMConstants.ADMIN_ID,
                    Name = MMConstants.ADMIN,
                    NormalizedName = MMConstants.ADMIN
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
