using MusicManager.API.Common.Models;
using MusicManager.API.Models.Enums;
using MusicManager.API.Utils;

namespace MusicManager.API.Models.Domain
{
    public class Band : BaseDeletableModel<int>
    {
        public Band()
        {
            this.Musicians = new HashSet<Musician>();
            this.Albums = new HashSet<Album>();
            this.Energy = MMConstants.DEFAULT_ENERGY;
            this.CreatedOn = DateTime.Now;
        }

        public string Name { get; set; }
        public Style Style { get; set; }
        public int Energy { get; private set; }
        public virtual ICollection<Musician> Musicians { get; set; }
        public virtual ICollection<Album> Albums { get; set; }
    }
}
