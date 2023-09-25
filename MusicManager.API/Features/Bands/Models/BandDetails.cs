using MusicManager.API.Common.Models;
using MusicManager.API.Data.Enums;
using MusicManager.API.Features.Albums.Models;
using MusicManager.API.Features.Musicians.Models;

namespace MusicManager.API.Features.Bands.Models
{
    public class BandDetails : BaseDeletableModel<int>
    {
        public string Name { get; set; }
        public Style Style { get; set; }
        public int Energy { private get; set; }
        public virtual ICollection<MusicianModel> Musicians { get; set; }
        public virtual ICollection<AlbumModel> Albums { get; set; }
    }
}
