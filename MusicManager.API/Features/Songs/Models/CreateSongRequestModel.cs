using MusicManager.API.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace MusicManager.API.Features.Songs.Models
{
    public class CreateSongRequestModel
    {
        [Required]
        [MinLength(2, ErrorMessage = $"{nameof(Name)} shoult be betwenn 3 and 20 characters!")]
        [MaxLength(20)]
        public string Name { get; set; }

        [EnumDataType(typeof(SongType))]
        public SongType Style { get; set; }

        public int AlbumId { get; set; }
    }
}