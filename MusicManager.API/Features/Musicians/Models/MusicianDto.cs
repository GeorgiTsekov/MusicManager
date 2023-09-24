using MusicManager.API.Common.Models;
using MusicManager.API.Data.Enums;
using MusicManager.API.Features.Bands.Models;

namespace MusicManager.API.Features.Musicians.Models
{
    public class MusicianDto : BaseDeletableModel<Guid>
    {
        public string Name { get; set; }
        public string Picture { get; set; }
        public int Ambition { get; set; }
        public int Talant { get; set; }
        public int Charisma { get; set; }
        public int ComposingSkills { get; set; }
        public int Mood { get; set; }
        public int Fame { get; set; }
        public MusicalInstrumentType MusicalInstrumentType { get; set; }
        public Clothing Clothing { get; set; }
        public int BandId { get; set; }
        public BandDto Band { get; set; }
    }
}
