
using MusicManager.API.Infrastructures.Models;

namespace MusicManager.API.Features.Albums.Models
{
    public class AlbumModel : BaseDeletableModel<int>
    {
        public string Name { get; set; }
        public string Descritpion { get; set; }
        public string BandName { get; set; }
    }
}
