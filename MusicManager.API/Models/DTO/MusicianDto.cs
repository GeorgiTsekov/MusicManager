using MusicManager.API.Models.Enums;

namespace MusicManager.API.Models.DTO
{
    public class MusicianDto
    {
        public int Id { get; set; }
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
        public virtual BandDto Band { get; set; }
    }
}
