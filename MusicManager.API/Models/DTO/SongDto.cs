
using MusicManager.API.Models.Enums;

namespace MusicManager.API.Models.DTO
{
    public class SongDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Training Training { get; set; }
        public string Sound { get; set; }
        public int AlbumId { get; set; }
        public virtual AlbumDto Album { get; set; }
    }
}
