
using MusicManager.API.Common.Models;
using MusicManager.API.Models.Domain;

namespace MusicManager.API.Models.DTO
{
    public class SongDto : BaseDeletableModel<Guid>
    {
        public string Name { get; set; }
        public int TrainingLevel { get; set; }
        public Dictionary<int, string> Sounds { get; set; }
        public int AlbumId { get; set; }
        public virtual Album Album { get; set; }
    }
}
