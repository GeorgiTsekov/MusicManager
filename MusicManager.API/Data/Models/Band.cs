using MusicManager.API.Common.Models;
using MusicManager.API.Utils;
using System.ComponentModel.DataAnnotations;

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

        [Required]
        public string Name { get; set; }

        public string Style { get; set; }

        public int Energy { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public virtual User User { get; set; }

        public virtual ICollection<Musician> Musicians { get; set; }

        public virtual ICollection<Album> Albums { get; set; }
    }
}
