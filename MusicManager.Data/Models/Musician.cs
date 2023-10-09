using MusicManager.Data.Enums;
using MusicManager.Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace MusicManager.Data.Models
{
    public class Musician : BaseDeletableModel<Guid>
    {
        public Musician()
        {
            Picture = "https://cdn.britannica.com/27/150327-050-CDB88DF6/Bono.jpg";
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Picture { get; set; }

        public int Ambition { get; set; }

        public int Talant { get; set; }

        public int Compatibility { get; set; }

        public DailyRent DailyRent { get; set; }

        public MusicalInstrumentType MusicalInstrumentType { get; set; }

        public int BandId { get; set; }

        [Required]
        public virtual Band Band { get; set; }
    }
}
