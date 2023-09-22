using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MusicManager.API.Models.Domain;

namespace MusicManager.API.Data
{
    public class MusicManagerDbContext : IdentityDbContext
    {
        public MusicManagerDbContext(DbContextOptions<MusicManagerDbContext> options) : base(options)
        {
        }

        public DbSet<Musician> Musicians { get; set; }
        public DbSet<Band> Bands { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Manager> Managers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "5888d7e8-213b-46c5-a8f6-beca89e5a68a";
            var writerRoleId = "5038b910-7a29-4163-ba89-c8a49a53e4d4";


            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },
                new IdentityRole
                {
                    Id = writerRoleId,
                    ConcurrencyStamp = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
