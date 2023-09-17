using MusicManager.API.Common.Models;
using MusicManager.API.Models.Enums;

namespace MusicManager.API.Models.Domain
{
    public class Musician : BaseDeletableModel<int>
    {
        public string Name { get; set; }
        public string Picture { get; set; }
        public MusicalInstrument MusicalInstrument { get; set; }
        public int Ambition { get; set; }
        public int Talant { get; set; }
        public int Charisma { get; set; }
        public int ComposingSkills { get; set; }
        public int Mood { get; set; }
        public int Fame { get; set; }
        public int BandId { get; set; }
        public virtual Band Band { get; set; }
    }
}
