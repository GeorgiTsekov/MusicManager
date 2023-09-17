using MusicManager.API.Common.Models;
using MusicManager.API.Models.Enums;

namespace MusicManager.API.Models.Domain
{
    public class Song : BaseDeletableModel<int>
    {
        public string Name { get; set; }
        public Training Training { get; set; }
        public string Sound { get; set; }
        public int AlbumId { get; set; }
        public virtual Album Album { get; set; }
    }
}
