using MusicManager.API.Data.Enums;
using MusicManager.API.Infrastructures.Models;
using System.ComponentModel.DataAnnotations;

namespace MusicManager.API.Data.Models
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
