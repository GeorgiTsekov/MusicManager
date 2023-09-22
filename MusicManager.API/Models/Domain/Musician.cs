using MusicManager.API.Common.Models;
using MusicManager.API.Models.Enums;
using MusicManager.API.Utils;

namespace MusicManager.API.Models.Domain
{
    public class Musician : BaseDeletableModel<Guid>
    {
        public Musician()
        {
            this.CreatedOn = DateTime.Now;
            this.Picture = "https://cdn.britannica.com/27/150327-050-CDB88DF6/Bono.jpg";
            this.Ambition = new Random().Next(MMConstants.MIN_TALANT, MMConstants.MAX_TALANT);
            this.Talant = new Random().Next(MMConstants.MIN_TALANT, MMConstants.MAX_TALANT);
            this.Charisma = new Random().Next(MMConstants.MIN_TALANT, MMConstants.MAX_TALANT);
        }

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
        public virtual Band Band { get; set; }
    }
}
