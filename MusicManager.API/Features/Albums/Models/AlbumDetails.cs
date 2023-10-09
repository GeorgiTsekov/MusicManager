using MusicManager.API.Common.Models;
using MusicManager.API.Features.Bands.Models;
using MusicManager.API.Features.Songs.Models;

namespace MusicManager.API.Features.Albums.Models
{
    public class AlbumDetails : BaseDeletableModel<int>
    {
        public string Name { get; set; }
        public string Descritpion { get; set; }
        public int BandId { get; set; }
        public virtual ICollection<SongDetails> Songs { get; set; }
    }
}
