using MusicManager.Data.Enums;
using MusicManager.Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace MusicManager.Data.Models
{
    public class Song : BaseDeletableModel<int>
    {
        public Song()
        {
        }

        [Required]
        public string Name { get; set; }

        public int TrainingLevel { get; set; }

        public SongType Style { get; set; }

        public string Sound { get; set; }

        public int AlbumId { get; set; }

        [Required]
        public virtual Album Album { get; set; }
    }
}
