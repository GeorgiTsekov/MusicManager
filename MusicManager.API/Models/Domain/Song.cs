
using MusicManager.API.Models.Enums;

namespace MusicManager.API.Models.Domain
{
    public class Song
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Training Training { get; set; }
        public string Sound { get; set; }
        public Guid AlbumId { get; set; }
        public virtual Album Album { get; set; }
    }
}
