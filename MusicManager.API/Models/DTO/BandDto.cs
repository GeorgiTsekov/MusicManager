using MusicManager.API.Models.Enums;

namespace MusicManager.API.Models.DTO
{
    public class BandDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Style Style { get; set; }
        public double Money { get; set; }
        public virtual ICollection<MusicianDto> Musicians { get; set; }
        public virtual ICollection<AlbumDto> Albums { get; set; }
    }
}
