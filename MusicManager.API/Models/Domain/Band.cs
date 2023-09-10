using MusicManager.API.Models.Enums;

namespace MusicManager.API.Models.Domain
{
    public class Band
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Style Style { get; set; }
        public double Money { get; set; }
        public virtual ICollection<Musician> Musicians { get; set; }
        public virtual ICollection<Album> Albums { get; set; }
    }
}
