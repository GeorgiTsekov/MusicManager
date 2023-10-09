using MusicManager.Infrastructure.Models;

namespace MusicManager.API.Features.Songs.Models
{
    public class SongModel : BaseDeletableModel<int>
    {
        public string Name { get; set; }
        public int TrainingLevel { get; set; }
        public string Sound { get; set; }
    }
}
