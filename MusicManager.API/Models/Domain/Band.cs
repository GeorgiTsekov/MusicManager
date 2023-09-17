using MusicManager.API.Common.Models;
using MusicManager.API.Models.Enums;

namespace MusicManager.API.Models.Domain
{
    public class Band : BaseDeletableModel<int>
    {
        public Band()
        {
            this.Musicians = new HashSet<Musician>();
            this.Albums = new HashSet<Album>();
        }

        public string Name { get; set; }
        public Style Style { get; set; }
        public double Money { get; set; }
        public virtual IEnumerable<Musician> Musicians { get; set; }
        public virtual IEnumerable<Album> Albums { get; set; }
    }
}
