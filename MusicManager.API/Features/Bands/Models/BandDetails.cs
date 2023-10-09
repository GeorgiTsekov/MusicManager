using MusicManager.API.Features.Albums.Models;
using MusicManager.API.Features.Musicians.Models;
using MusicManager.Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace MusicManager.API.Features.Bands.Models
{
    public class BandDetails : BaseDeletableModel<int>
    {
        [Required]
        public string Name { get; set; }

        public string Style { get; set; }

        public int Energy { private get; set; }

        public virtual ICollection<MusicianModel> Musicians { get; set; }

        public virtual ICollection<AlbumDetails> Albums { get; set; }
    }
}
