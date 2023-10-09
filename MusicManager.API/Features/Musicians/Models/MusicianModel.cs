using MusicManager.API.Common.Models;
using MusicManager.API.Data.Enums;

namespace MusicManager.API.Features.Musicians.Models
{
    public class MusicianModel : BaseDeletableModel<Guid>
    {
        public string Name { get; set; }
        public string Picture { get; set; }
        public int Ambition { get; set; }
        public int Talant { get; set; }
        public int Compatibility { get; set; }
        public DailyRent DailyRent { get; set; }
        public MusicalInstrumentType MusicalInstrumentType { get; set; }
        public int BandId { get; set; }
    }
}
