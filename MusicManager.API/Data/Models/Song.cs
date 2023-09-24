using MusicManager.API.Common.Models;

namespace MusicManager.API.Data.Models
{
    public class Song : BaseDeletableModel<int>
    {
        public string Name { get; set; }
        public int TrainingLevel { get; set; }
        public string Sound { get; set; }
        public int AlbumId { get; set; }
        public virtual Album Album { get; set; }
    }
}
