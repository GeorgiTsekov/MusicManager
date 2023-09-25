using MusicManager.API.Common.Models;
using MusicManager.API.Data.Enums;
using MusicManager.API.Utils;

namespace MusicManager.API.Data.Models
{
    public class Band : BaseDeletableModel<int>
    {
        public Band()
        {
            Musicians = new HashSet<Musician>();
            Albums = new HashSet<Album>();
            Energy = MMConstants.DEFAULT_ENERGY;
        }

        public string Name { get; set; }
        public Style Style { get; set; }
        public int Energy { get; private set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Musician> Musicians { get; set; }
        public virtual ICollection<Album> Albums { get; set; }
    }
}
